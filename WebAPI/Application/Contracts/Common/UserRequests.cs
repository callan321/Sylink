namespace WebAPI.Application.Contracts.Common;

public class UpdateUserRequest
{
    public string DisplayName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

