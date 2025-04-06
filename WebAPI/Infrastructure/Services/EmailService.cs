using WebAPI.Application.Interfaces.Services;

namespace WebAPI.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly string frontendUrl = "https://localhost:4200";

    public Task SendEmailConfirmationAsync(string email, string token)
    {
        var confirmLink = $"{frontendUrl}/auth/confirm-email?token={Uri.EscapeDataString(token)}&email={Uri.EscapeDataString(email)}";

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n[Email Confirmation]");
        Console.ResetColor();

        Console.WriteLine($"{token}");
        Console.WriteLine($"Email sent to: {email}");
        Console.WriteLine($"Confirmation Link:");

        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(confirmLink);
        Console.ResetColor();

        return Task.CompletedTask;
    }

    public Task SendPasswordResetAsync(string email, string token)
    {
        var resetLink = $"{frontendUrl}/auth/reset-password?token={Uri.EscapeDataString(token)}&email={Uri.EscapeDataString(email)}";

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n[Password Reset]");
        Console.ResetColor();

        Console.WriteLine($"{token}");
        Console.WriteLine($"Email sent to: {email}");
        Console.WriteLine($"Reset Link:");

        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(resetLink);
        Console.ResetColor();

        return Task.CompletedTask;
    }
}
