using WebAPI.Application.Contracts.Dtos;

namespace WebAPI.Application.Interfaces.Security;

public interface ICookieService
{
    void SetAccessToken(HttpResponse response, AccessTokenDto token);
    string? GetAccessToken(HttpRequest request);
    void RemoveAccessToken(HttpResponse response);
    void SetRefreshToken(HttpResponse response, RefreshTokenDto token);
    string? GetRefreshToken(HttpRequest request);
    void RemoveRefreshToken(HttpResponse response);
}
