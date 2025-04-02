using System.ComponentModel.DataAnnotations;

namespace WebAPI.Dtos
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "Display Name is required")]
        [StringLength(16, MinimumLength = 3, ErrorMessage = "Display Name must be between 3 and 16 characters")]
        [RegularExpression(@"^[a-zA-Z0-9 ]+$", ErrorMessage = "Display Name can only contain letters, numbers, and spaces")]
        public required string DisplayName { get; set; }

    }

    public class LoginDto
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public required string Password { get; set; }
    }

    public class AuthResponseDto
    {
        public required string Message { get; set; }
        public required string TokenExpiry { get; set; }
    }

    public class ErrorResponseDto
    {
        public required string Message { get; set; }
        public List<string>? Errors { get; set; }
        public string? ErrorCode { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }

    public class RefreshTokenDto
    {
        [Required]
        public required string RefreshToken { get; set; }
    }

    public class VerifyEmailDto
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Token { get; set; }
    }

    public class ForgotPasswordDto
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
    }

    public class ResetPasswordDto
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Token { get; set; }

        [Required]
        public required string NewPassword { get; set; }
    }
}
