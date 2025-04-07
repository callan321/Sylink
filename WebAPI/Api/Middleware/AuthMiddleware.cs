using WebAPI.Application.Interfaces.Services;

namespace WebAPI.Api.Middleware;

// Middleware that authenticates the user from the access token or refresh token
public class AuthMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        var tokenService = context.RequestServices.GetRequiredService<ITokenService>();

        // Try to authenticate using the access token
        var principal = await tokenService.AuthenticateAccessTokenAsync(context.Request, context.Response);

        // If access token is valid, set the user
        if (principal is not null)
        {
            context.User = principal;
            await _next(context);
            return;
        }

        // Try to refresh tokens using the refresh token
        var refreshedPrincipal = await tokenService.TryRefreshAsync(context.Request, context.Response);

        // If refresh was successful, set the user
        if (refreshedPrincipal is not null)
        {
            context.User = refreshedPrincipal;
        }

        // Continue with the request regardless — Use [Authorize(policy=?)]
        await _next(context);
    }
}
