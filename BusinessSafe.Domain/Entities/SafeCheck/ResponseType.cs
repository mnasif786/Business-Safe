using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BusinessSafe.Domain.Entities.SafeCheck
{
    public enum ResponseType
    {
        [Description("Acceptable")]
        Acceptable = 1,
        [Description("Improvement Required")]
        ImprovementRequired = 2,
        [Description("Unacceptable")]
        Unacceptable = 3,
        [Description("Not Applicable")]
        NotApplicable = 4
    }

    public static class EnumExtensions
    {
        public static string ToDescription(this Enum en)
        {
            Type type = en.GetType();

            MemberInfo[] memInfo = type.GetMember(en.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return en.ToString();
    }
}
   

    
}
