namespace WebAPI.Services
{
    public class EmailService
    {
        private readonly string frontendUrl = "https://localhost:4200";

        public Task SendEmailConfirmationAsync(string email, string token)
        {
            var confirmLink = $"{frontendUrl}/auth/confirm-email?token={Uri.EscapeDataString(token)}&email={Uri.EscapeDataString(email)}";

            Console.WriteLine($"[Email Confirmation]");
            Console.WriteLine($"To: {email}");
            Console.WriteLine($"Confirmation Link:\n{confirmLink}");

            return Task.CompletedTask;
        }

        public Task SendPasswordResetAsync(string email, string token)
        {
            var resetLink = $"{frontendUrl}/auth/reset-password?token={Uri.EscapeDataString(token)}&email={Uri.EscapeDataString(email)}";

            Console.WriteLine($"[Password Reset]");
            Console.WriteLine($"To: {email}");
            Console.WriteLine($"Reset Link:\n{resetLink}");

            return Task.CompletedTask;
        }
    }
}
