namespace WebAPI.Models
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public string Token { get; set; } = default!;
        public DateTime ExpiryDate { get; set; }
        public bool IsRevoked { get; set; }

        // Relationship 
        public required string UserId { get; set; }
        public ApplicationUser User { get; set; } = default!;

    }
}
