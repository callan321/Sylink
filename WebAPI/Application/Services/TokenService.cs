using Microsoft.AspNetCore.Http;
using WebAPI.Application.Contracts;
using WebAPI.Application.Contracts.Common;
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
        var (accessToken, accessExpiry) = _jwtService.GenerateAccessToken(user);
        var refreshToken = await _jwtService.GenerateRefreshToken(user.Id);

        _cookieService.SetToken(response, "access_token", accessToken, accessExpiry);
        _cookieService.SetToken(response, "refresh_token", refreshToken.Token, refreshToken.ExpiryDate);

        return OperationResult<AuthResponse>.Ok(new AuthResponse
        {
            TokenExpiry = accessExpiry
        }, message);
    }

    public async Task<OperationResult<AuthResponse>> RefreshAsync(HttpRequest request, HttpResponse response)
    {
        var token = _cookieService.GetToken(request, "refresh_token");
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
        _cookieService.RemoveToken(response, "access_token");
        _cookieService.RemoveToken(response, "refresh_token");
    }
}
