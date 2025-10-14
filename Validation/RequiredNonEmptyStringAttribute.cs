using System.ComponentModel.DataAnnotations;

namespace SupplyChainTransparency.Validation
{
    public class RequiredNonEmptyStringAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult(ErrorMessage ?? "The field cannot be empty or whitespace.");
            }
            return ValidationResult.Success;
        }
    }
}


