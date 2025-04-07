using WebAPI.Application.Contracts.Common;
using WebAPI.Application.Contracts.Requests;
using WebAPI.Application.Contracts.Responses;

namespace WebAPI.Application.Interfaces.Security;

public interface IAuthService
{
    Task<OperationResult<object>> RegisterAsync(RegisterRequest request);
    Task<OperationResult<object>> LoginAsync(LoginRequest request, HttpResponse response);
    Task<OperationResult<object>> LogoutAsync(HttpRequest request, HttpResponse response);
    Task<OperationResult<object>> ConfirmEmailAsync(VerifyEmailRequest request);
    Task<OperationResult<object>> ForgotPasswordAsync(ForgotPasswordRequest request);
    Task<OperationResult<object>> ResetPasswordAsync(ResetPasswordRequest request);

}
