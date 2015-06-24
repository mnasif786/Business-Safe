using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BusinessSafe.Application.Request.Attributes
{
    public class GreaterThanZeroAttribute : ValidationAttribute
    {
        public GreaterThanZeroAttribute(string message)
            : base("{0} Is Required")
        {
            Message = message;
        }

        public string Message { get; set; }

        public override string FormatErrorMessage(string name)
        {
            return Message;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || (long)value <= 0)
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
            return ValidationResult.Success;
        }

    }
}
