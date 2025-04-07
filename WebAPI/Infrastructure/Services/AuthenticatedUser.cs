using System.Security.Claims;
using WebAPI.Application.Interfaces.Services;

namespace WebAPI.Infrastructure.Services;

public class AuthenticatedUser(IHttpContextAccessor contextAccessor) : IAuthenticatedUser
{
    private readonly ClaimsPrincipal? _user = contextAccessor.HttpContext?.User;

    public string Id =>
        _user?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

    public string Email =>
        _user?.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;

    public bool IsEmailConfirmed =>
        _user?.FindFirst("email_confirmed")?.Value == "true";

    public bool IsAuthenticated =>
        _user?.Identity?.IsAuthenticated ?? false;
}
