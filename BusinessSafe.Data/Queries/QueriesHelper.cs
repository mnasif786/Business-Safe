using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessSafe.Data.Queries
{
    public static class QueriesHelper
    {
        internal static DateTime GetPreviousMonthsDate(int frequency)
        {
            DateTime oneMonthAgo = DateTime.Now.AddMonths(-1);
            int daysInPreviousMonth = DateTime.DaysInMonth(oneMonthAgo.Year, oneMonthAgo.Month);

            return frequency <= daysInPreviousMonth ? new DateTime(oneMonthAgo.Year, oneMonthAgo.Month, frequency)
                                              : new DateTime(oneMonthAgo.Year, oneMonthAgo.Month, daysInPreviousMonth); //Last day of the previous month
        }

        internal static DateTime GetNextMonthsDate(int frequency)
        {
            DateTime nextMonth = DateTime.Now.AddMonths(1);
            int daysInNextMonth = DateTime.DaysInMonth(nextMonth.Year, nextMonth.Month);

            return frequency <= daysInNextMonth ? new DateTime(nextMonth.Year, nextMonth.Month, frequency)
                                              : new DateTime(nextMonth.Year, nextMonth.Month, daysInNextMonth); //Last day of the next month
        }

        internal static DateTime SimplifyDate(DateTime date)
        {
            return new DateTime(date.Year,date.Month,date.Day);
        }
    }
}
