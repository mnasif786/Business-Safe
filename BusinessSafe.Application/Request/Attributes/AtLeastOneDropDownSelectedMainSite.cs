using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessSafe.Application.Request.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class AtLeastOneDropDownSelectedMainSite : ValidationAttribute
    {
        public string CommaSeperatedProperties { get; private set; }

        public AtLeastOneDropDownSelectedMainSite(string commaSeperatedProperties)
            : base("At Least One Should Be Selected")
        {
            if (!string.IsNullOrEmpty(commaSeperatedProperties))
                CommaSeperatedProperties = commaSeperatedProperties;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                string[] ignoreProperty = CommaSeperatedProperties.Split(new char[] { '-' });

                var isMainSite = IsMainSite(validationContext, ignoreProperty);

                if (!isMainSite)
                {
                    string[] strProperties = ignoreProperty[0].Split(new char[] { ',' });
                    bool isOneOptionSelected = false;
                    foreach (string strProperty in strProperties)
                    {
                        var curProperty = validationContext.ObjectInstance.GetType().GetProperty(strProperty);
                        var curPropertyValue = curProperty.GetValue(validationContext.ObjectInstance, null);

                        if (curPropertyValue != null)
                        {
                            isOneOptionSelected = true;
                        }
                    }

                    if (!isOneOptionSelected)
                        return new ValidationResult("At Least One Should Be Selected");
                }
            }
            return ValidationResult.Success;
        }

        private static bool IsMainSite(ValidationContext validationContext, string[] ignoreProperty)
        {            
            if (ignoreProperty.Length > 1)
            {
                var curProperty = validationContext.ObjectInstance.GetType().GetProperty(ignoreProperty[1]);
                var curPropertyValue = curProperty.GetValue(validationContext.ObjectInstance, null);
                return (bool) curPropertyValue;                    
            }
            return false;
        }
    }
}