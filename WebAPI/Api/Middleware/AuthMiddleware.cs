using WebAPI.Application.Interfaces.Services;

namespace WebAPI.Api.Middleware;

public class AuthMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        var tokenService = context.RequestServices.GetRequiredService<ITokenService>();

        var principal = await tokenService.AuthenticateAccessTokenAsync(context.Request, context.Response);

        if (principal is not null)
        {
            context.User = principal;
        }

        await _next(context);
    }
}
