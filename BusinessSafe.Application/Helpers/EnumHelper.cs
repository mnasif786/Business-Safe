using System;
using System.ComponentModel;
using System.Reflection;

using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Helpers
{
    public class EnumHelper
    {
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;

            return value.ToString();
        }

        public static int GetEnumValue(string value)
        {
            return (int)((HazardTypeEnum)Enum.Parse(typeof(HazardTypeEnum), value));
        }
    }
}