using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BusinessSafe.WebSite.CustomValidators.FurtherControlMeasureTasks
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class RequiredIfReoccurringAttribute : RequiredAttribute
    {
        private object _typeId = new object();
        public string RequiredProperty { get; set; } 

        public override bool IsValid(object value)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(value);
            var isReoccurring = (bool)properties.Find("IsReoccurring", true).GetValue(value);
            var confirmValue = properties.Find(RequiredProperty, true).GetValue(value);

            if (isReoccurring)
            {
                return base.IsValid(confirmValue);
            }

            return true;
        }

        public override object TypeId
        {
            get { return _typeId; }
        }
    } 
} 