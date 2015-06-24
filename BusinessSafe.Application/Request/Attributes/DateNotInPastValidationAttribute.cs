using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessSafe.Application.Request.Attributes
{
    public class DateNotInPastValidationAttribute: ValidationAttribute
    {
        private readonly string _propertyName;

        public DateNotInPastValidationAttribute(string propertyName)
        {
            _propertyName = propertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            DateTime date;

            bool success = DateTime.TryParse(value.ToString(), out date);
            if (!success)
                return new ValidationResult("Valid date is required");

            //if (date < System.Data.SqlTypes.SqlDateTime.MinValue || date < DateTime.Now) throws SqlTypeException
            if (date < DateTime.Now)
                return new ValidationResult(string.Format("{0} must be in the future", _propertyName));

            return ValidationResult.Success;
        }
    }
}