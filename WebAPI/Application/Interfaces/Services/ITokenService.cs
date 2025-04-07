using System.Security.Claims;
using WebAPI.Application.Contracts.Auth;
using WebAPI.Application.Contracts.Common;
using WebAPI.Domain.Entities;

namespace WebAPI.Application.Interfaces.Services;

public interface ITokenService
{
    Task<OperationResult<AuthResponse>> GenerateAndSetTokensAsync(ApplicationUser user, HttpResponse response, string message);
    Task<ClaimsPrincipal?> AuthenticateAccessTokenAsync(HttpRequest request, HttpResponse response);
    Task<ClaimsPrincipal?> TryRefreshAsync(HttpRequest request, HttpResponse response);
    void ClearTokens(HttpResponse response);
}
