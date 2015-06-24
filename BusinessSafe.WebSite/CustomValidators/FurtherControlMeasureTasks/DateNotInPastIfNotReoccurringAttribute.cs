using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BusinessSafe.WebSite.CustomValidators.FurtherControlMeasureTasks
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class DateNotInPastIfNotReoccurringAttribute : ValidationAttribute
    {
        private object _typeId = new object();
        public string RequiredProperty { get; set; }
        public string RequiredPropertyDisplayName { get; set; }

        public override bool IsValid(object value)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(value);
            var taskReoccurringTypeId = (int)properties.Find("TaskReoccurringTypeId", true).GetValue(value);
            var isReoccurring = taskReoccurringTypeId > 0;
            var confirmValue = (string)properties.Find(RequiredProperty, true).GetValue(value);

            if (!isReoccurring)
            {
                DateTime date;

                bool success = DateTime.TryParse(confirmValue, out date);

                if (!success)
                {
                    ErrorMessage = string.Format("{0} requires a valid date", RequiredPropertyDisplayName);
                    return false;
                }
            
                if (date < System.Data.SqlTypes.SqlDateTime.MinValue || date < DateTime.Now)
                {
                    ErrorMessage = string.Format("{0} must be in the future", RequiredPropertyDisplayName);
                    return false;
                }
            }

            return true;
        }

        public override object TypeId
        {
            get { return _typeId; }
        }
    } 
} 