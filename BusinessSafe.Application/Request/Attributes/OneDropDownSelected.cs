using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessSafe.Application.Request.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class OneDropDownSelected : ValidationAttribute
    {
        public string CommaSeperatedProperties { get; private set; }

        public OneDropDownSelected(string commaSeperatedProperties): base("Only One Option Is Allowed")
        {
            if (!string.IsNullOrEmpty(commaSeperatedProperties))
                CommaSeperatedProperties = commaSeperatedProperties;
        }

        protected override ValidationResult IsValid (object value, ValidationContext validationContext)
        {
            if (value != null && int.Parse(value.ToString()) != 0)
            {
                string[] strProperties = CommaSeperatedProperties.Split(new char[] { ',' });
                
                    foreach (string strProperty in strProperties)
                    {
                        var curProperty = validationContext.ObjectInstance.GetType().GetProperty(strProperty);
                        var curPropertyValue = curProperty.GetValue(validationContext.ObjectInstance, null);

                        if (curPropertyValue != null)
                        {
                            return new ValidationResult("Only One Option Is Allowed");
                        }
                    }                
            }
            return ValidationResult.Success;
        }
    }
}
