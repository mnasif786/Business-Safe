using System;

namespace BusinessSafe.WebSite.Extensions
{
    public static class DateTimeOffsetExtensions
    {
        public static string ToLocalShortDateString(
            this DateTimeOffset dateTimeOffset)
        {
            var fullString = dateTimeOffset.ToLocalTime().ToString();
            return fullString.Substring(0, 10);
        }
    }
}
