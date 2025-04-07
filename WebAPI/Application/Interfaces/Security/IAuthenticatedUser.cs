namespace WebAPI.Application.Interfaces.Security;
public interface IAuthenticatedUser
{
    string Id { get; }
    string Email { get; }
    bool IsEmailConfirmed { get; }
    bool IsAuthenticated { get; }
}
