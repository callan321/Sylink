using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// ----------------------------
// Service Configuration
// ----------------------------

// CORS policy for Angular development server
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev", policy =>
    {
        policy.WithOrigins("https://localhost:4200")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

// Adds controller support (API endpoints)
builder.Services.AddControllers();

// Adds OpenAPI/Swagger UI for API docs
builder.Services.AddOpenApi();

// In-memory EF Core database for dev/testing
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseInMemoryDatabase("AppDb");
});

// Identity API endpoints
builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Identity configuration (simplified password and lockout rules)
builder.Services.Configure<IdentityOptions>(options =>
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

// Custom services for JWT handling and email
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<EmailService>();

// Adds policy-based authorization support
builder.Services.AddAuthorization();

var app = builder.Build();

// ----------------------------
// HTTP Request Pipeline
// ----------------------------

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

// Enforce HTTPS redirection
app.UseHttpsRedirection();

// Apply CORS policy before authentication
app.UseCors("AllowAngularDev");

// Enable authentication middleware 
app.UseAuthentication();

// Enable authorization checks for [Authorize]
app.UseAuthorization();

// Map attribute-routed controller endpoints
app.MapControllers();

app.Run();
