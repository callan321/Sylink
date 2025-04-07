using System.ComponentModel.DataAnnotations;

namespace WebAPI.Application.Contracts.Requests;

public class RegisterRequest
{
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [MinLength(3, ErrorMessage = "Password must be at least 3 characters long.")]
    public required string Password { get; set; }

    [Required(ErrorMessage = "Display name is required.")]
    [StringLength(16, MinimumLength = 3, ErrorMessage = "Display name must be between 3 and 16 characters long.")]
    [RegularExpression(@"^[a-zA-Z0-9 ]+$", ErrorMessage = "Display name can only contain letters, numbers, and spaces.")]
    public required string DisplayName { get; set; }
}

public class LoginRequest
{
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [MinLength(3, ErrorMessage = "Password must be at least 3 characters long.")]
    public required string Password { get; set; }
    public bool RememberMe { get; set; } = false;
}

public class VerifyEmailRequest
{
    public required string Email { get; set; }

    public required string Token { get; set; }
}

public class ForgotPasswordRequest
{
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public required string Email { get; set; }
}

public class ResetPasswordRequest
{
    public required string Email { get; set; }

    public required string Token { get; set; }

    [Required(ErrorMessage = "New password is required.")]
    [MinLength(3, ErrorMessage = "Password must be at least 3 characters long.")]
    public required string NewPassword { get; set; }
}
