using Scalar.AspNetCore;
using WebAPI.Api.Middleware;
using WebAPI.Startup;

var builder = WebApplication.CreateBuilder(args);

// Register application services
builder.Services.AddAppServices(builder.Configuration);

var app = builder.Build();

// Enable Swagger and Scalar in development
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

// Register custom middleware
app.UseMiddleware<AuthMiddleware>();

// Core middleware pipeline
app.UseHttpsRedirection();
app.UseCors("AllowAngularDev");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
