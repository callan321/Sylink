namespace WebAPI.Domain.Entities;

public class RefreshToken
{
    public required string Id { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime ExpiryDate { get; set; }
    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = default!;

}
