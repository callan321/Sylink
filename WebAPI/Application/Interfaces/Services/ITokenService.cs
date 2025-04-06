using WebAPI.Application.Contracts;
using WebAPI.Application.Contracts.Common;
using WebAPI.Domain.Entities;

namespace WebAPI.Application.Interfaces.Services;

public interface ITokenService
{
    Task<OperationResult<AuthResponse>> GenerateAndSetTokensAsync(ApplicationUser user, HttpResponse response, string message);
    Task<OperationResult<AuthResponse>> RefreshAsync(HttpRequest request, HttpResponse response);
    void ClearTokens(HttpResponse response);
}
