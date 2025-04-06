using System.Security.Claims;
using WebAPI.Domain.Entities;

namespace WebAPI.Application.Interfaces.Services;

public interface IJwtService
{
    (string Token, DateTime Expiry) GenerateAccessToken(ApplicationUser user);
    Task<RefreshToken> GenerateRefreshToken(string userId);
    Task<(bool IsValid, string? UserId)> ValidateRefreshTokenAsync(string token);

    public Task<(bool IsValid, ClaimsPrincipal? Principal)> ValidateAccessTokenAsync(HttpRequest request);
    Task DeleteRefreshToken(string token);
}
