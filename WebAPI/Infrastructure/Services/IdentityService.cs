using Microsoft.AspNetCore.Identity;
using WebAPI.Application.Contracts.Common;
using WebAPI.Domain.Entities;

namespace WebAPI.Infrastructure.Services;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public IdentityService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<(bool Succeeded, IEnumerable<FieldError> Errors)> RegisterAsync(string email, string password, string displayName)
    {
        var user = new ApplicationUser { Email = email, UserName = email, DisplayName = displayName };
        var result = await _userManager.CreateAsync(user, password);
        var errors = result.Errors.Select(e => new FieldError { Message = e.Description });
        return (result.Succeeded, errors);
    }

    public async Task<bool> CheckPasswordSignInAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null) return false;

        var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
        return result.Succeeded;
    }

    public async Task<bool> IsEmailConfirmedAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return user?.EmailConfirmed ?? false;
    }

    public async Task<string?> GenerateEmailConfirmationTokenAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return user is null ? null : await _userManager.GenerateEmailConfirmationTokenAsync(user);
    }

    public async Task<string?> GeneratePasswordResetTokenAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return user is null ? null : await _userManager.GeneratePasswordResetTokenAsync(user);
    }

    public async Task<bool> ConfirmEmailAsync(string email, string token)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null) return false;

        var result = await _userManager.ConfirmEmailAsync(user, token);
        return result.Succeeded;
    }

    public async Task<bool> ResetPasswordAsync(string email, string token, string newPassword)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null) return false;

        var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
        return result.Succeeded;
    }

    public Task<ApplicationUser?> GetUserByEmailAsync(string email) => _userManager.FindByEmailAsync(email);
    public Task<ApplicationUser?> GetUserByIdAsync(string id) => _userManager.FindByIdAsync(id);
}
