using WebAPI.Application.Contracts.Common;
using WebAPI.Application.Contracts.Requests;
using WebAPI.Application.Contracts.Responses;
using WebAPI.Application.Interfaces.Security;
using WebAPI.Application.Interfaces.Services;

namespace WebAPI.Application.Services;

public class AuthService(
    IIdentityService identityService,
    IEmailService emailService,
    ITokenService tokenService
) : IAuthService
{
    private readonly IIdentityService _identityService = identityService;
    private readonly IEmailService _emailService = emailService;
    private readonly ITokenService _tokenService = tokenService;

    public async Task<OperationResult<RegisterResponse>> RegisterAsync(RegisterRequest request)
    {
        var (succeeded, errors) = await _identityService.RegisterAsync(
            request.Email,
            request.Password,
            request.DisplayName
        );

        if (!succeeded)
            return OperationResult<RegisterResponse>.Fail("Registration failed", errors.ToList());

        var token = await _identityService.GenerateEmailConfirmationTokenAsync(request.Email);
        if (string.IsNullOrWhiteSpace(token))
            return OperationResult<RegisterResponse>.Fail("Failed to generate email confirmation token.");

        await _emailService.SendEmailConfirmationAsync(request.Email, token);

        return OperationResult<RegisterResponse>.Ok(new RegisterResponse
        {
            Email = request.Email,
            DisplayName = request.DisplayName
        });
    }

    public async Task<OperationResult<object>> LoginAsync(LoginRequest request, HttpResponse response)
    {
        var user = await _identityService.GetUserByEmailAsync(request.Email);
        if (user is null || !await _identityService.IsEmailConfirmedAsync(request.Email))
            return OperationResult<object>.Fail("Invalid credentials or email not confirmed");

        var succeeded = await _identityService.CheckPasswordSignInAsync(request.Email, request.Password);
        if (!succeeded)
            return OperationResult<object>.Fail("Invalid credentials");

        await _tokenService.GenerateAndSetTokensAsync(user, response);
        return OperationResult<object>.Ok(new { }, "Login successful");
    }



    public async Task<OperationResult<object>> ConfirmEmailAsync(VerifyEmailRequest request)
    {
        var succeeded = await _identityService.ConfirmEmailAsync(request.Email, request.Token);
        return succeeded
            ? OperationResult<object>.Ok(new { }, "Email confirmed")
            : OperationResult<object>.Fail("Invalid or expired token");
    }

    public async Task<OperationResult<object>> ForgotPasswordAsync(ForgotPasswordRequest request)
    {
        var token = await _identityService.GeneratePasswordResetTokenAsync(request.Email);
        if (string.IsNullOrWhiteSpace(token))
            return OperationResult<object>.Fail("User not found");

        await _emailService.SendPasswordResetAsync(request.Email, token);
        return OperationResult<object>.Ok(new { }, "Password reset link sent");
    }
    public async Task<OperationResult<object>> ResetPasswordAsync(ResetPasswordRequest request)
    {
        var succeeded = await _identityService.ResetPasswordAsync(request.Email, request.Token, request.NewPassword);
        return succeeded
            ? OperationResult<object>.Ok(new { }, "Password reset successfully")
            : OperationResult<object>.Fail("Failed to reset password");
    }

    public Task<OperationResult<object>> LogoutAsync(HttpRequest request, HttpResponse response)
    {
        _tokenService.ClearTokens(response);
        return Task.FromResult(OperationResult<object>.Ok(new { }, "Logged out successfully"));
    }
}
