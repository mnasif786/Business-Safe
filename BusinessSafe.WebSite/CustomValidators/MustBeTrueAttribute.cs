using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessSafe.WebSite.CustomValidators
{
    public class MustBeTrueAttribute: ValidationAttribute 
    { 
        public override bool IsValid(object value) 
        { 
            if (value == null) return false;
            try
            {
                return Convert.ToBoolean(value);
            }
            catch (InvalidCastException) 
            { 
                return false; 
            } 
        } 
    }
} 