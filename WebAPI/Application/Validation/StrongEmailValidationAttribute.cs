using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace WebAPI.Application.Validation
{
    public partial class StrongEmailValidationAttribute : ValidationAttribute
    {
        [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
        private static partial Regex EmailRegex();

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not string email || string.IsNullOrWhiteSpace(email))
                return ValidationResult.Success;

            if (!EmailRegex().IsMatch(email))
                return new ValidationResult("Invalid email format.");

            return ValidationResult.Success;
        }
    }
}
