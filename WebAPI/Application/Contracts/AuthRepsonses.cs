namespace WebAPI.Application.Contracts
{
    public class AuthResponse
    {
        public required DateTime TokenExpiry { get; set; }
    }

    public class RegisterResponse
    {
        public required string Email { get; set; }
        public required string DisplayName { get; set; }
    }

}

