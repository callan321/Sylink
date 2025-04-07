using System.Security.Claims;
using WebAPI.Domain.Entities;

namespace WebAPI.Application.Interfaces.Services;

public interface IUserClaimsProvider
{
    IEnumerable<Claim> GetClaimsForUser(ApplicationUser user);
}