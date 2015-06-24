using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessSafe.WebSite.CustomValidators
{
    public class MustBeNotEmptyGuidAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) return false;
            try
            {
                return (Guid)value != Guid.Empty;
            }
            catch (InvalidCastException)
            {
                return false;
            }
        }
    }
}