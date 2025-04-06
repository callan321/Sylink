namespace WebAPI.Application.Interfaces.Services;

public interface ICookieService
{
    void SetToken(HttpResponse response, string name, string token, DateTime expires);
    void RemoveToken(HttpResponse response, string name);
    string? GetToken(HttpRequest request, string name);
}
