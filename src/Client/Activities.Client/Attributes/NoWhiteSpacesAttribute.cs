using System.ComponentModel.DataAnnotations;

namespace Activities.Client.Attributes
{
    public class NoWhiteSpacesAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not string stringValue)
                return new ValidationResult("Provided value is not text value");

            if (string.IsNullOrWhiteSpace(stringValue))
                return new ValidationResult("Value cannot be empty");

            if (stringValue.Any(Char.IsWhiteSpace))
                return new ValidationResult("Value cannot contain white spaces");

            return ValidationResult.Success;
        }
    }
}
