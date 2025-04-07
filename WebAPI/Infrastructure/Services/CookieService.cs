using WebAPI.Application.Contracts.Dtos;
using WebAPI.Application.Interfaces.Services;

namespace WebAPI.Infrastructure.Services;

public class CookieService : ICookieService
{
    private const string AccessTokenName = "access_token";
    private const string RefreshTokenName = "refresh_token";

    // -----------------------
    // Public API
    // -----------------------

    public void SetAccessToken(HttpResponse response, AccessTokenDto token) =>
        SetToken(response, AccessTokenName, token.Token, token.Expiry);

    public string? GetAccessToken(HttpRequest request) =>
        GetToken(request, AccessTokenName);

    public void RemoveAccessToken(HttpResponse response) =>
        RemoveToken(response, AccessTokenName);

    public void SetRefreshToken(HttpResponse response, RefreshTokenDto token) =>
        SetToken(response, RefreshTokenName, token.Token, token.Expiry);

    public string? GetRefreshToken(HttpRequest request) =>
        GetToken(request, RefreshTokenName);

    public void RemoveRefreshToken(HttpResponse response) =>
        RemoveToken(response, RefreshTokenName);

    // -----------------------
    // Internal Helpers
    // -----------------------

    private void SetToken(HttpResponse response, string name, string token, DateTime expires)
    {
        response.Cookies.Append(name, token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = expires,
            Path = "/"
        });
    }

    private void RemoveToken(HttpResponse response, string name)
    {
        response.Cookies.Delete(name, new CookieOptions
        {
            Secure = true,
            SameSite = SameSiteMode.None,
            Path = "/"
        });
    }

    private string? GetToken(HttpRequest request, string name)
    {
        return request.Cookies.TryGetValue(name, out var value)
            ? value
            : null;
    }
}
