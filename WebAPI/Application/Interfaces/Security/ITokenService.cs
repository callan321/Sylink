using System.Security.Claims;
using WebAPI.Domain.Entities;

namespace WebAPI.Application.Interfaces.Security;

public interface ITokenService
{
    Task GenerateAndSetTokensAsync(ApplicationUser user, HttpResponse response, bool rememberMe);
    Task<ClaimsPrincipal?> AuthenticateAccessTokenAsync(HttpRequest request, HttpResponse response);
    Task<ClaimsPrincipal?> TryRefreshAsync(HttpRequest request, HttpResponse response);
    void ClearTokens(HttpResponse response);
}
