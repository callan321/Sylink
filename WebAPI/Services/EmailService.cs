namespace WebAPI.Services
{
    public class EmailService
    {
        public Task SendEmailConfirmationAsync(string email, string token)
        {
            Console.WriteLine($"Sending email to {email}");
            Console.WriteLine($"Confirmation token: {token}");
            return Task.CompletedTask;
        }
        public Task SendPasswordResetAsync(string email, string token)
        {
            Console.WriteLine($"Sending email to {email}");
            Console.WriteLine($"Password reset token: {token}");
            return Task.CompletedTask;
        }

    }
}
