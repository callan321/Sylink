
using WebAPI.Api.Middleware;

namespace WebAPI.Startup;

public static class ConfigureMiddleware
{
    /// <summary>
    /// Registers custom middleware components.
    /// </summary>
    public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<AuthMiddleware>();
        return app;
    }
}
