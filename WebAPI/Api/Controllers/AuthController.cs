using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Application.Contracts.Requests;
using WebAPI.Application.Interfaces.Security;
using WebAPI.Application.Interfaces.Services;

namespace WebAPI.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService, IAuthenticatedUser user, IApplicationUserService userService) : ControllerBase
{
    private readonly IAuthService _authService = authService;
    private readonly IAuthenticatedUser _user = user;
    private readonly IApplicationUserService _userService = userService;


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var result = await _authService.RegisterAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await _authService.LoginAsync(request, Response);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPost("confirm-email")]
    public async Task<IActionResult> ConfirmEmail([FromBody] VerifyEmailRequest request)
    {
        var result = await _authService.ConfirmEmailAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
    {
        var result = await _authService.ForgotPasswordAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
    {
        var result = await _authService.ResetPasswordAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var result = await _authService.LogoutAsync(Request, Response);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpGet("status")]
    [Authorize(Policy = "VerifiedUser")]
    public async Task<IActionResult> GetAuthStatus()
    {
        var result = await _userService.GetUserAsync(_user.Id);
        return result.Success ? Ok(result) : BadRequest(result);
    }

}
