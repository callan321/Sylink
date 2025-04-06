namespace WebAPI.Startup;

public static class ConfigureMiddleware
{
    /// <summary>
    /// Registers custom middleware components.
    /// </summary>
    public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder app)
    {
        // add something here
        return app;
    }
}
