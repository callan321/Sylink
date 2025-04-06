using WebAPI.Application.Interfaces.Services;

namespace WebAPI.Api.Middleware;

public class AuthMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        var tokenService = context.RequestServices.GetRequiredService<ITokenService>();

        var (isValid, principal) = await tokenService.VerifyAccessTokenAsync(context.Request, context.Response);

        // ✅ Set user if token is valid
        if (isValid && principal != null)
        {
            context.User = principal;
        }

        // ❌ Do NOT return 401 here — let [Authorize] do that
        await _next(context);
    }
}
