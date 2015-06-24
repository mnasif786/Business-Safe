using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System;

namespace BusinessSafe.WebSite.CustomValidators
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class ClassLevelRequiredAttribute : RequiredAttribute
    {
        private object _typeId = new object();
        public string RequiredProperty { get; set; }

        public override bool IsValid(object value)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(value);
            var confirmValue = properties.Find(RequiredProperty, true).GetValue(value);
            var returnValue = base.IsValid(confirmValue);
            return returnValue;
        }

        public override object TypeId
        {
            get { return _typeId; }
        }
    }
}