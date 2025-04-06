namespace WebAPI.Application.Interfaces.Services;

public interface IEmailService
{
    Task SendEmailConfirmationAsync(string email, string token);
    Task SendPasswordResetAsync(string email, string token);
}
