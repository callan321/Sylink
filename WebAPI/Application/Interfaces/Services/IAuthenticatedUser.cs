namespace WebAPI.Application.Interfaces.Services;

public interface IAuthenticatedUser
{
    string Id { get; }
    string Email { get; }
    bool IsEmailConfirmed { get; }
    bool IsAuthenticated { get; }
}
