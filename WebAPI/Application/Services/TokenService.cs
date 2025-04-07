using System.Security.Claims;
using WebAPI.Application.Contracts.Auth;
using WebAPI.Application.Contracts.Common;
using WebAPI.Application.Contracts.Dtos;
using WebAPI.Application.Interfaces.Services;
using WebAPI.Domain.Entities;

namespace WebAPI.Application.Services;

public class TokenService(
    IJwtService jwtService,
    ICookieService cookieService,
    IIdentityService identityService
) : ITokenService
{
    private readonly IJwtService _jwtService = jwtService;
    private readonly ICookieService _cookieService = cookieService;
    private readonly IIdentityService _identityService = identityService;

    public async Task<OperationResult<AuthResponse>> GenerateAndSetTokensAsync(ApplicationUser user, HttpResponse response, string message)
    {
        var accessToken = _jwtService.GenerateAccessToken(user);
        var refreshToken = await _jwtService.GenerateRefreshToken(user.Id);

        _cookieService.SetAccessToken(response, accessToken);

        _cookieService.SetRefreshToken(response, new RefreshTokenDto
        {
            Token = refreshToken.Token,
            Expiry = refreshToken.ExpiryDate
        });

        return OperationResult<AuthResponse>.Ok(new AuthResponse
        {
            TokenExpiry = accessToken.Expiry
        }, message);
    }

    public async Task<OperationResult<AuthResponse>> RefreshAsync(HttpRequest request, HttpResponse response)
    {
        var token = _cookieService.GetRefreshToken(request);
        if (string.IsNullOrWhiteSpace(token))
            return OperationResult<AuthResponse>.Fail("Refresh token is missing");

        var (isValid, userId) = await _jwtService.ValidateRefreshTokenAsync(token);
        if (!isValid || userId is null)
            return OperationResult<AuthResponse>.Fail("Invalid or expired refresh token");

        var user = await _identityService.GetUserByIdAsync(userId);
        if (user is null)
            return OperationResult<AuthResponse>.Fail("User not found");

        await _jwtService.DeleteRefreshToken(token);
        return await GenerateAndSetTokensAsync(user, response, "Token refreshed");
    }

    public void ClearTokens(HttpResponse response)
    {
        _cookieService.RemoveAccessToken(response);
        _cookieService.RemoveRefreshToken(response);
    }

    public async Task<ClaimsPrincipal?> AuthenticateAccessTokenAsync(HttpRequest request, HttpResponse response)
    {
        var principal = await _jwtService.GetUserClaimsFromAccessTokenAsync(request);

        if (principal is null)
        {
            _cookieService.RemoveAccessToken(response);
            return null;
        }

        return principal;
    }

    public async Task<ClaimsPrincipal?> TryRefreshAsync(HttpRequest request, HttpResponse response)
    {
        var refreshToken = _cookieService.GetRefreshToken(request);
        if (string.IsNullOrWhiteSpace(refreshToken))
            return null;

        var (isValid, userId) = await _jwtService.ValidateRefreshTokenAsync(refreshToken);
        if (!isValid || userId is null)
            return null;

        var user = await _identityService.GetUserByIdAsync(userId);
        if (user is null)
            return null;

        // Rotate the refresh token
        await _jwtService.DeleteRefreshToken(refreshToken);
        var newRefreshToken = await _jwtService.GenerateRefreshToken(user.Id);

        var accessToken = _jwtService.GenerateAccessToken(user);

        _cookieService.SetAccessToken(response, accessToken);
        _cookieService.SetRefreshToken(response, new RefreshTokenDto
        {
            Token = newRefreshToken.Token,
            Expiry = newRefreshToken.ExpiryDate
        });

        return await _jwtService.GetUserClaimsFromAccessTokenAsync(request);
    }
}
