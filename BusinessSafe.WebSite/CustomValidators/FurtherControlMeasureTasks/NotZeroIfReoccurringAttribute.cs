using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System;

namespace BusinessSafe.WebSite.CustomValidators.FurtherControlMeasureTasks
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class NotZeroIfReoccurringAttribute : ValidationAttribute
    {
        private object _typeId = new object();
        public string RequiredProperty { get; set; }

        public override bool IsValid(object value)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(value);
            var isReoccurring = (bool)properties.Find("IsReoccurring", true).GetValue(value);
            var confirmValue = (int)properties.Find(RequiredProperty, true).GetValue(value);
            return !isReoccurring || confirmValue > 0;
        }

        public override object TypeId
        {
            get { return _typeId; }
        }
    }
}