using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models
{
    [Index(nameof(DisplayName), IsUnique = true)]
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; } = default!;
        public ICollection<RefreshToken> RefreshTokens { get; set; } = default!;
    }
}
