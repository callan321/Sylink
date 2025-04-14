using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebAPI.Application.Filters;
using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Application.Interfaces.Security;
using WebAPI.Application.Interfaces.Services;
using WebAPI.Application.Services;
using WebAPI.Domain.Entities;
using WebAPI.Infrastructure.Data;
using WebAPI.Infrastructure.Data.Repositories;
using WebAPI.Infrastructure.Services;

namespace WebAPI.Startup;

public static class ConfigureServices
{
    /// <summary>
    /// Registers all application-level services.
    /// </summary>
    public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddApi()
                .AddCorsPolicy()
                .AddOpenApi()
                .AddDatabase(config)
                .AddIdentityConfig()
                .AddAuth()
                .AddCustomServices();

        return services;
    }

    /// <summary>
    /// Adds controller support for Web API endpoints.
    /// </summary>
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add<ValidationFilter>();
        });
        return services;
    }

    /// <summary>
    /// Adds a CORS policy for frontend development.
    /// </summary>
    public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAngularDev", policy =>
            {
                policy.WithOrigins("https://localhost:4200")
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials();
            });
        });

        return services;
    }



    /// <summary>
    /// Registers the EF Core in-memory database.
    /// </summary>
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseInMemoryDatabase("AppDb");
        });

        return services;
    }

    /// <summary>
    /// Configures ASP.NET Core Identity with custom rules.
    /// </summary>
    public static IServiceCollection AddIdentityConfig(this IServiceCollection services)
    {
        services.AddIdentityApiEndpoints<ApplicationUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 3;
            options.Password.RequiredUniqueChars = 0;
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.User.RequireUniqueEmail = true;
        });

        return services;
    }

    /// <summary>
    /// Adds built-in authentication and authorization services.
    /// </summary>
    public static IServiceCollection AddAuth(this IServiceCollection services)
    {
        services.AddAuthentication();
        services.AddAuthorization(options =>
        {
            options.AddPolicy("VerifiedUser", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("email_confirmed", "true");
            });
        });
        services.AddHttpContextAccessor();
        return services;
    }

    /// <summary>
    /// Registers application-specific services like JWT and email handling.
    /// </summary>
    public static IServiceCollection AddCustomServices(this IServiceCollection services)
    {

        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICookieService, CookieService>();
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IApplicationUserService, ApplicationUserService>();
        services.AddScoped<IUserClaimsProvider, UserClaimsProvider>();
        services.AddScoped<IAuthenticatedUser, AuthenticatedUser>();
        return services;
    }
}
