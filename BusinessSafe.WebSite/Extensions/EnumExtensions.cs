using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;

using BusinessSafe.WebSite.Helpers;

namespace BusinessSafe.WebSite.Extensions
{
    public static class EnumExtensions
    {
        public static SelectList ToSelectList(this Enum enumeration)
        {
            var list = (Enum.GetValues(enumeration.GetType()).Cast<Enum>().Select(d => new
            {
                Value =
            (int)
            Enum.Parse(
                enumeration.GetType(),
                Enum.GetName(
                    enumeration.GetType(),
                    d)),
                Text =
            EnumHelper.GetEnumDescription
            (d)
            })).ToList();

            return new SelectList(list, "Value", "Text");
        }

        public static SelectList ToSelectList(this Enum enumeration, Enum selectedValue)
        {
            var list = (Enum.GetValues(enumeration.GetType()).Cast<Enum>().Select(d => new
            {
                Value =
            (int)
            Enum.Parse(
                enumeration.GetType(),
                Enum.GetName(
                    enumeration.GetType(),
                    d)),
                Text =
            EnumHelper.GetEnumDescription
            (d)
            })).ToList();

            return new SelectList(list, "Value", "Text", selectedValue);
        }
    }
}