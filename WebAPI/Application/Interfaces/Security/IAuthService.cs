using WebAPI.Application.Contracts.Common;
using WebAPI.Application.Contracts.Requests;


namespace WebAPI.Application.Interfaces.Security;

public interface IAuthService
{
    Task<OperationResult<Unit>> RegisterAsync(RegisterRequest request);
    Task<OperationResult<Unit>> LoginAsync(LoginRequest request, HttpResponse response);
    Task<OperationResult<Unit>> LogoutAsync(HttpRequest request, HttpResponse response);
    Task<OperationResult<Unit>> ConfirmEmailAsync(VerifyEmailRequest request);
    Task<OperationResult<Unit>> ForgotPasswordAsync(ForgotPasswordRequest request);
    Task<OperationResult<Unit>> ResetPasswordAsync(ResetPasswordRequest request);

}
