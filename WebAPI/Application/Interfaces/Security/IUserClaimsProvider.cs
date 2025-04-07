using System.Security.Claims;
using WebAPI.Domain.Entities;

namespace WebAPI.Application.Interfaces.Security;

public interface IUserClaimsProvider
{
    IEnumerable<Claim> GetClaimsForUser(ApplicationUser user);
}