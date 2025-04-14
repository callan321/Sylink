using System.ComponentModel.DataAnnotations;
using WebAPI.Application.Interfaces.Security;

namespace WebAPI.Application.Validation
{
    public class UniqueEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var identityService = validationContext.GetService(typeof(IIdentityService)) as IIdentityService;

            if (value is not string email || string.IsNullOrWhiteSpace(email))
                return ValidationResult.Success;

            var user = identityService?.GetUserByEmailAsync(email).GetAwaiter().GetResult();

            return user is null
                ? ValidationResult.Success
                : new ValidationResult($"Email '{email}' is already taken.");
        }
    }
}
