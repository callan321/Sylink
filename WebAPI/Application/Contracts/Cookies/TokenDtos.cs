namespace WebAPI.Application.Contracts.Cookies;


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
