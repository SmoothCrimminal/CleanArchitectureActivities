using System.ComponentModel.DataAnnotations;

namespace Activities.Client.Attributes
{
    public class FutureDateAttribute : ValidationAttribute
    {
        public string Message { get; }

        public FutureDateAttribute(string message)
        {
            Message = message;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not DateTime dateTime)
                return new ValidationResult("Provided value is not DateTime value!");

            if (DateTime.Now.Date > dateTime.Date)
                return new ValidationResult(Message);

            return ValidationResult.Success;
        }
    }
}
