using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Domain.Entities;

// This isn't a pure domain model since it inherits from IdentityUser,
// but it's a practical trade-off to integrate ASP.NET Identity without extra mapping.
// Includes DisplayName (with a uniqueness constraint) and related RefreshTokens.
[Index(nameof(DisplayName), IsUnique = true)]
public class ApplicationUser : IdentityUser
{
    public string DisplayName { get; set; } = default!;
    public ICollection<RefreshToken> RefreshTokens { get; set; } = default!;
}
