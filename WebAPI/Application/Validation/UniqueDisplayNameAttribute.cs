using System.ComponentModel.DataAnnotations;
using WebAPI.Application.Interfaces.Security;

namespace WebAPI.Application.Validation
{
    public class UniqueDisplayNameAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var identityService = validationContext.GetService(typeof(IIdentityService)) as IIdentityService;

            if (value is not string displayName || string.IsNullOrWhiteSpace(displayName))
                return ValidationResult.Success;

            var user = identityService?.GetUserByDisplayNameAsync(displayName).GetAwaiter().GetResult();

            return user is null
                ? ValidationResult.Success
                : new ValidationResult($"Display name '{displayName}' is already taken.");
        }
    }
}
