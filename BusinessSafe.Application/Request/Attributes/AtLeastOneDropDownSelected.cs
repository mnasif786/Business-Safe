using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessSafe.Application.Request.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class AtLeastOneDropDownSelected : ValidationAttribute
    {
         public string CommaSeperatedProperties { get; private set; }

         public AtLeastOneDropDownSelected(string commaSeperatedProperties)
             : base("At Least One Should Be Selected")
        {
            if (!string.IsNullOrEmpty(commaSeperatedProperties))
                CommaSeperatedProperties = commaSeperatedProperties;
        }

        protected override ValidationResult IsValid (object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                string[] strProperties = CommaSeperatedProperties.Split(new char[] { ',' });
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
            return ValidationResult.Success;
        }
    }
}
