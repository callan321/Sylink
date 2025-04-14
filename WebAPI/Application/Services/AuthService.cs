using WebAPI.Application.Contracts.Common;
using WebAPI.Application.Contracts.Requests;
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

    public async Task<OperationResult<Unit>> RegisterAsync(RegisterRequest request)
    {
        var (succeeded, errors) = await _identityService.RegisterAsync(
            request.Email,
            request.Password,
            request.DisplayName
        );

        if (!succeeded)
            return OperationResult<Unit>.Fail("Registration failed", [.. errors]);

        var token = await _identityService.GenerateEmailConfirmationTokenAsync(request.Email);
        if (string.IsNullOrWhiteSpace(token))
            return OperationResult<Unit>.Fail("Failed to generate email confirmation token.");

        await _emailService.SendEmailConfirmationAsync(request.Email, token);

        return OperationResult<Unit>.Ok(Unit.Value, "Successfully created a new account.");
    }

    public async Task<OperationResult<Unit>> LoginAsync(LoginRequest request, HttpResponse response)
    {
        var user = await _identityService.GetUserByEmailAsync(request.Email);
        if (user is null || !await _identityService.IsEmailConfirmedAsync(request.Email))
            return OperationResult<Unit>.Fail("Invalid credentials or email not confirmed");

        var succeeded = await _identityService.CheckPasswordSignInAsync(request.Email, request.Password);
        if (!succeeded)
            return OperationResult<Unit>.Fail("Invalid credentials");

        await _tokenService.GenerateAndSetTokensAsync(user, response, request.RememberMe);
        return OperationResult<Unit>.Ok(Unit.Value, "Login successful");
    }

    public async Task<OperationResult<Unit>> ConfirmEmailAsync(VerifyEmailRequest request)
    {
        var succeeded = await _identityService.ConfirmEmailAsync(request.Email, request.Token);
        return succeeded
            ? OperationResult<Unit>.Ok(Unit.Value, "Email confirmed")
            : OperationResult<Unit>.Fail("Invalid or expired token");
    }

    public async Task<OperationResult<Unit>> ForgotPasswordAsync(ForgotPasswordRequest request)
    {
        var token = await _identityService.GeneratePasswordResetTokenAsync(request.Email);
        if (string.IsNullOrWhiteSpace(token))
            return OperationResult<Unit>.Fail("User not found");

        await _emailService.SendPasswordResetAsync(request.Email, token);
        return OperationResult<Unit>.Ok(Unit.Value, "Password reset link sent");
    }

    public async Task<OperationResult<Unit>> ResetPasswordAsync(ResetPasswordRequest request)
    {
        var succeeded = await _identityService.ResetPasswordAsync(request.Email, request.Token, request.NewPassword);
        return succeeded
            ? OperationResult<Unit>.Ok(Unit.Value, "Password reset successfully")
            : OperationResult<Unit>.Fail("Failed to reset password");
    }

    public Task<OperationResult<Unit>> LogoutAsync(HttpRequest request, HttpResponse response)
    {
        _tokenService.ClearTokens(response);
        return Task.FromResult(OperationResult<Unit>.Ok(Unit.Value, "Logged out successfully"));
    }
}
