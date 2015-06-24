using System.ComponentModel.DataAnnotations;

namespace BusinessSafe.WebSite.CustomValidators
{
    public class MustNotBeNullAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return value != null;
        }
    }
}