using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Services;
using WebAPI.Dtos;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        JwtService jwtService,
        EmailService emailService) : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
        private readonly JwtService _jwtService = jwtService;
        private readonly EmailService _emailService = emailService;

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            var user = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.Email,
                DisplayName = model.DisplayName,
                EmailConfirmed = false
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                // Send email confirmation
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                await _emailService.SendEmailConfirmationAsync(user.Email, token);
                return Ok(new ErrorResponseDto
                {
                    Message = "Account created successfully"
                });
            }

            var errorMessages = string.Join("; ", result.Errors.Select(e => e.Description));
            return BadRequest(new ErrorResponseDto
            {
                Message = errorMessages
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return BadRequest(new ErrorResponseDto
                {
                    Message = "Invalid Email or Password"
                });
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (result.Succeeded)
            {
                var refreshToken = await _jwtService.GenerateRefreshToken(user.Id);
                return Ok(new AuthResponseDto
                {
                    AccessToken = _jwtService.GenerateJwtToken(user),
                    RefreshToken = refreshToken.Token,
                    TokenExpiry = refreshToken.ExpiryDate.ToString("yyyy-MM-ddTHH:mm:ssZ")
                });
            }

            return BadRequest(new ErrorResponseDto
            {
                Message = "Invalid Email or Password"
            });
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenDto model)
        {
            var (isValid, userId) = await _jwtService.ValidateRefreshToken(model.RefreshToken);
            if (!isValid || userId == null)
            {
                return BadRequest(new ErrorResponseDto
                {
                    Message = "Invalid Refresh Token"
                });
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest(new ErrorResponseDto
                {
                    Message = "Invalid Refresh Token"
                });
            }

            await _jwtService.RevokeRefreshToken(model.RefreshToken);
            var refreshToken = await _jwtService.GenerateRefreshToken(user.Id);
            return Ok(new AuthResponseDto
            {
                AccessToken = _jwtService.GenerateJwtToken(user),
                RefreshToken = refreshToken.Token,
                TokenExpiry = refreshToken.ExpiryDate.ToString("yyyy-MM-ddTHH:mm:ssZ")
            });

        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new ErrorResponseDto
            {
                Message = "Logged out successfully"
            });
        }

        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail(VerifyEmailDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return BadRequest(new ErrorResponseDto
                {
                    Message = "Invalid Email or User not found"
                });
            }
            var result = await _userManager.ConfirmEmailAsync(user, model.Token);
            if (result.Succeeded)
            {
                return Ok(new ErrorResponseDto
                {
                    Message = "Email verified successfully"
                });
            }
            return BadRequest(new ErrorResponseDto
            {
                Message = "Invalid Token"
            });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return BadRequest(new ErrorResponseDto
                {
                    Message = "Invalid Email or User not found"
                });
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            if (!(user.Email == null))
            {
                await _emailService.SendPasswordResetAsync(user.Email, token);

            }
            else { return BadRequest(new ErrorResponseDto { Message = "Invalid Email or User not found" }); }

            return Ok(new ErrorResponseDto
            {
                Message = "Password reset link sent to your email"
            });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return BadRequest(new ErrorResponseDto
                {
                    Message = "Invalid Email or User not found"
                });
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
            if (result.Succeeded)
            {
                return Ok(new ErrorResponseDto
                {
                    Message = "Password reset successfully"
                });
            }
            return BadRequest(new ErrorResponseDto
            {
                Message = "Invalid Token"
            });
        }
    }
}
