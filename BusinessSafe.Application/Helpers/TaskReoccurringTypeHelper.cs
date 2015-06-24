using System;
using System.Text;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Helpers
{
    public static class TaskReoccurringTypeHelper
    {
        public static string GetReoccurringFrequencyDetails(TaskReoccurringType taskReoccurringType, DateTime? taskReoccurringEndDate, bool canViewFurtherDetails = true)
        {
            var result = new StringBuilder("Reoccurring Frequency : " + taskReoccurringType.ToString());
            result.AppendLine();
            if (taskReoccurringEndDate.HasValue)
            {
                result.AppendLine("Last End Date : " + taskReoccurringEndDate.Value.ToShortDateString());
            }
            if (canViewFurtherDetails) 
                result.AppendLine("Double click icon for further details");

            return result.ToString();
        }
    }
}