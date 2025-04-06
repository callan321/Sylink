using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Application.Interfaces.Services;
using WebAPI.Domain.Entities;

namespace WebAPI.Infrastructure.Services;

public class JwtService(IConfiguration config, IRefreshTokenRepository refreshTokenRepo) : IJwtService
{
    private readonly IConfiguration _config = config;
    private readonly IRefreshTokenRepository _refreshTokens = refreshTokenRepo;

    public (string Token, DateTime Expiry) GenerateAccessToken(ApplicationUser user)
    {
        var claims = CreateUserClaims(user);
        var expiry = GetAccessTokenExpiry();
        var key = GetSecurityKey();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expiry,
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
            Issuer = _config["JwtSettings:Issuer"],
            Audience = _config["JwtSettings:Audience"]
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return (tokenHandler.WriteToken(token), expiry);
    }

    public async Task<RefreshToken> GenerateRefreshToken(string userId)
    {
        var refreshToken = new RefreshToken
        {
            Id = Guid.NewGuid().ToString(),
            Token = GenerateSecureRandomString(),
            ExpiryDate = GetRefreshTokenExpiry(),
            UserId = userId
        };

        await _refreshTokens.AddAsync(refreshToken);
        return refreshToken;
    }

    public (bool IsValid, string? UserId) ValidateRefreshToken(string token)
    {
        throw new NotImplementedException("This method is not implemented yet.");
    }

    public Task DeleteRefreshToken(string token)
    {
        return _refreshTokens.DeleteAsync(token);
    }

    public async Task<(bool IsValid, string? UserId)> ValidateRefreshTokenAsync(string token)
    {
        var refreshToken = await _refreshTokens.GetByTokenAsync(token);

        if (refreshToken == null || refreshToken.ExpiryDate < DateTime.UtcNow)
        {
            await _refreshTokens.DeleteAsync(token);
            return (false, null);
        }

        return (true, refreshToken.UserId);
    }

    // -----------------------
    // Private Helpers
    // -----------------------

    private static IEnumerable<Claim> CreateUserClaims(ApplicationUser user)
    {
        return
        [
            new Claim(JwtRegisteredClaimNames.Sub, user.Email ?? string.Empty),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
            new Claim("DisplayName", user.DisplayName ?? string.Empty)
        ];
    }

    private DateTime GetAccessTokenExpiry()
    {
        var minutes = double.Parse(_config["JwtSettings:AccessTokenExpiryMinutes"]
            ?? throw new InvalidOperationException("JwtSettings:AccessTokenExpiryMinutes is missing"));

        return DateTime.UtcNow.AddMinutes(minutes);
    }

    private DateTime GetRefreshTokenExpiry()
    {
        var days = double.Parse(_config["JwtSettings:RefreshTokenExpiryDays"]
            ?? throw new InvalidOperationException("JwtSettings:RefreshTokenExpiryDays is missing"));

        return DateTime.UtcNow.AddDays(days);
    }

    private SymmetricSecurityKey GetSecurityKey()
    {
        var key = _config["JwtSettings:securityKey"]
            ?? throw new InvalidOperationException("JwtSettings:securityKey is missing");

        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
    }

    private static string GenerateSecureRandomString()
    {
        var bytes = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(bytes);
        return Convert.ToBase64String(bytes);
    }


}
