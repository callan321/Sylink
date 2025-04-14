using System.Security.Claims;
using WebAPI.Application.Contracts.Dtos;
using WebAPI.Application.Interfaces.Security;
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

    public async Task GenerateAndSetTokensAsync(ApplicationUser user, HttpResponse response, bool isPersistent = false)
    {
        var accessTokenDto = _jwtService.GenerateAccessToken(user);
        var refreshTokenEntity = await _jwtService.GenerateRefreshToken(user.Id, isPersistent);

        _cookieService.SetAccessToken(response, accessTokenDto);

        _cookieService.SetRefreshToken(response, new RefreshTokenDto
        {
            Token = refreshTokenEntity.Token,
            Expiry = refreshTokenEntity.ExpiryDate
        });
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
        var refreshTokenString = _cookieService.GetRefreshToken(request);
        if (string.IsNullOrWhiteSpace(refreshTokenString))
            return null;

        var refreshToken = await _jwtService.ValidateRefreshTokenAsync(refreshTokenString);
        if (refreshToken == null)
            return null;

        var user = await _identityService.GetUserByIdAsync(refreshToken.UserId);
        if (user is null)
            return null;

        // Rotate the refresh token
        await _jwtService.DeleteRefreshToken(refreshTokenString);
        var newRefreshToken = await _jwtService.GenerateRefreshToken(user.Id, refreshToken.IsPersistent);

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
