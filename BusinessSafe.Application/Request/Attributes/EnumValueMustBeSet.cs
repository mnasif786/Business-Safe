using System.ComponentModel.DataAnnotations;

namespace BusinessSafe.Application.Request.Attributes
{
    public class BooleanRequiredAttribute : RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            bool outParam;
            return value != null && bool.TryParse(value.ToString(), out outParam);
        }
    }

    public class EnumValueMustBeSet : ValidationAttribute 
    {
        public EnumValueMustBeSet(string fieldName)
            : base("{0} Is Required")
        {
            Message = fieldName;
        }

        public string Message { get; set; }

        public override string FormatErrorMessage(string name)
        {
            return Message;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || (int)value == 0)
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
            
            return ValidationResult.Success;
        }

       
    }
}