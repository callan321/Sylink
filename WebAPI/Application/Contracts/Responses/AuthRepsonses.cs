namespace WebAPI.Application.Contracts.Responses;
public class RegisterResponse
{
    public required string Email { get; set; }
    public required string DisplayName { get; set; }
}

public class AuthStatusResponse
{
    public string? UserId { get; set; }
    public string? Email { get; set; }
    public string? DisplayName { get; set; }
}

