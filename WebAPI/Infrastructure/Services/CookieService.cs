using WebAPI.Application.Interfaces.Services;

namespace WebAPI.Infrastructure.Services;

public class CookieService : ICookieService
{
    public void SetToken(HttpResponse response, string name, string token, DateTime expires)
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

    public void RemoveToken(HttpResponse response, string name)
    {
        response.Cookies.Delete(name, new CookieOptions
        {
            Secure = true,
            SameSite = SameSiteMode.None,
            Path = "/"
        });
    }

    public string? GetToken(HttpRequest request, string name)
    {
        return request.Cookies.TryGetValue(name, out var value)
            ? value
            : null;
    }
}
