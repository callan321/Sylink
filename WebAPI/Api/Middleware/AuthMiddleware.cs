using WebAPI.Application.Interfaces.Services;

namespace WebAPI.Api.Middleware;

// Middleware that authenticates the user from the access token
public class AuthMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    // This method is run for every HTTP request that enters the application
    public async Task InvokeAsync(HttpContext context)
    {
        // Resolve the token service from dependency injection
        var tokenService = context.RequestServices.GetRequiredService<ITokenService>();

        // Try to authenticate the user using the access token stored in cookies
        // If the token is invalid, it will be removed (handled inside the service)
        var principal = await tokenService.AuthenticateAccessTokenAsync(context.Request, context.Response);

        // If authentication succeeded, set the user so it's available throughout the request
        if (principal is not null)
        {
            context.User = principal;
        }

        // Call the next middleware in the pipeline (or the actual endpoint)
        await _next(context);
    }
}
