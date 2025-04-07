namespace WebAPI.Application.Contracts.Dtos;


public class AccessTokenDto
{
    public string Token { get; set; } = string.Empty;
    public DateTime Expiry { get; set; }
}

public class RefreshTokenDto
{
    public string Token { get; set; } = string.Empty;
    public DateTime Expiry { get; set; }
}
