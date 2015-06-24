using System;
using StructureMap;

namespace BusinessSafe.AcceptanceTests.StepHelpers
{
    public static class StringHelper
    {
        public static DateTime? GetDateFromString(this string dateString)
        {
            if (String.IsNullOrEmpty(dateString))
                return null;

            var isToday = dateString.Contains("today");

            if (isToday)
            {
                var dateStringWithoutToday = dateString.Replace("today", string.Empty).Trim();
                bool addDays = dateStringWithoutToday.Contains("+");
                bool removeDays = dateStringWithoutToday.Contains("-");

                if (addDays)
                {
                    var numberOfDaysOffsetString = dateStringWithoutToday.Replace("+", string.Empty);
                    int numberOfDaysOffset = GetNumberOfDaysOffset(numberOfDaysOffsetString);
                    return GetCurrentDateTime().AddDays(numberOfDaysOffset);
                }
                
                if (removeDays)
                {
                    var numberOfDaysOffsetString = dateStringWithoutToday.Replace("-", string.Empty);
                    int numberOfDaysOffset = GetNumberOfDaysOffset(numberOfDaysOffsetString);
                    return GetCurrentDateTime().AddDays(numberOfDaysOffset * -1);
                }

                return GetCurrentDateTime();
            }

            throw new ArgumentException("Wrong format is used for date, correct format: today + 1, today - 10");
        }

        private static DateTime GetCurrentDateTime()
        {
            return ObjectFactory.GetInstance<ICurrentTime>().GetCurrentDateTime();            
        }

        private static int GetNumberOfDaysOffset(string numberOfDaysOffsetString)
        {
            int numberOfDaysOffset;
            if (Int32.TryParse(numberOfDaysOffsetString, out numberOfDaysOffset))
            {
                return numberOfDaysOffset;
            }

            throw new ArgumentException("Wrong format is used in the integer part of string, correct format: today + 1, today - 10");
        }
    }

    public interface ICurrentTime
    {
        DateTime GetCurrentDateTime();
    }

    public class CurrentTime : ICurrentTime
    {
        public DateTime GetCurrentDateTime()
        {
            return TimeZone.CurrentTimeZone.ToLocalTime(DateTime.Now);
        }
    }
}
