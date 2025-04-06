using WebAPI.Application.Contracts.Common;
using WebAPI.Domain.Entities;

public interface IIdentityService
{
    Task<(bool Succeeded, IEnumerable<FieldError> Errors)> RegisterAsync(string email, string password, string displayName);
    Task<bool> CheckPasswordSignInAsync(string email, string password);
    Task<bool> IsEmailConfirmedAsync(string email);
    Task<string?> GenerateEmailConfirmationTokenAsync(string email);
    Task<string?> GeneratePasswordResetTokenAsync(string email);
    Task<bool> ConfirmEmailAsync(string email, string token);
    Task<bool> ResetPasswordAsync(string email, string token, string newPassword);
    Task<ApplicationUser?> GetUserByEmailAsync(string email);
    Task<ApplicationUser?> GetUserByIdAsync(string id);
}
