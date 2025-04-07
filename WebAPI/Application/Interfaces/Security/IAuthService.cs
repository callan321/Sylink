using WebAPI.Application.Contracts.Common;
using WebAPI.Application.Contracts.Requests;
using WebAPI.Application.Contracts.Responses;


namespace WebAPI.Application.Interfaces.Services;

public interface IAuthService
{
    Task<OperationResult<RegisterResponse>> RegisterAsync(RegisterRequest request);
    Task<OperationResult<AuthResponse>> LoginAsync(LoginRequest request, HttpResponse response);
    Task<OperationResult<object>> LogoutAsync(HttpRequest request, HttpResponse response);
    Task<OperationResult<object>> ConfirmEmailAsync(VerifyEmailRequest request);
    Task<OperationResult<object>> ForgotPasswordAsync(ForgotPasswordRequest request);
    Task<OperationResult<object>> ResetPasswordAsync(ResetPasswordRequest request);
}
