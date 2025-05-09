﻿using System.Security.Claims;
using WebAPI.Application.Contracts.Dtos;
using WebAPI.Domain.Entities;

namespace WebAPI.Application.Interfaces.Security;

public interface IJwtService
{
    AccessTokenDto GenerateAccessToken(ApplicationUser user);
    Task<RefreshToken> GenerateRefreshToken(string userId, bool isPersistent);
    Task<RefreshToken?> ValidateRefreshTokenAsync(string token);

    Task<ClaimsPrincipal?> GetUserClaimsFromAccessTokenAsync(HttpRequest request);
    Task DeleteRefreshToken(string token);
}
