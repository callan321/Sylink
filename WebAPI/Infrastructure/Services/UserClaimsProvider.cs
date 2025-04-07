using System.Security.Claims;
using WebAPI.Application.Interfaces.Services;
using WebAPI.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace WebAPI.Infrastructure.Services;

public class UserClaimsProvider : IUserClaimsProvider
{
    public IEnumerable<Claim> GetClaimsForUser(ApplicationUser user)
    {
        return
        [
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email ?? ""),
            new Claim("email_confirmed", user.EmailConfirmed.ToString().ToLowerInvariant())
        ];
    }
}
