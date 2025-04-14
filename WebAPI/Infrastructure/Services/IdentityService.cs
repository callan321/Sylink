using Microsoft.AspNetCore.Identity;
using WebAPI.Application.Contracts.Common;
using WebAPI.Domain.Entities;
using WebAPI.Application.Interfaces.Security;

namespace WebAPI.Infrastructure.Services;

public class IdentityService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;

    public async Task<(bool Succeeded, IEnumerable<FieldError> Errors)> RegisterAsync(string email, string password, string displayName)
    {
        var user = new ApplicationUser { Email = email, UserName = email, DisplayName = displayName };
        var result = await _userManager.CreateAsync(user, password);
        var errors = MapIdentityErrors(result.Errors);
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

    private static IEnumerable<FieldError> MapIdentityErrors(IEnumerable<IdentityError> errors)
    {
        return errors.Select(e => new FieldError
        {
            Field = MapFieldFromCode(e.Code),
            Message = e.Description
        });
    }

    private static string MapFieldFromCode(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
            return "general";

        return code.ToLower() switch
        {
            var c when c.Contains("email") => "email",
            var c when c.Contains("password") => "password",
            var c when c.Contains("username") => "displayName",
            _ => "general"
        };
    }
}
