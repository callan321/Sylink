using System.Security.Claims;
using WebAPI.Application.Contracts.Dtos;
using WebAPI.Domain.Entities;

namespace WebAPI.Application.Interfaces.Services;

public interface IJwtService
{
    AccessTokenDto GenerateAccessToken(ApplicationUser user);
    Task<RefreshToken> GenerateRefreshToken(string userId);
    Task<(bool IsValid, string? UserId)> ValidateRefreshTokenAsync(string token);

    Task<ClaimsPrincipal?> GetUserClaimsFromAccessTokenAsync(HttpRequest request);
    Task DeleteRefreshToken(string token);
}
