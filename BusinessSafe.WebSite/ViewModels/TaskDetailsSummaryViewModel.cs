using System.Collections.Generic;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.WebSite.ViewModels
{
    public class TaskDetailsSummaryViewModel
    {
        public static TaskDetailsSummaryViewModel CreateFrom(TaskDetailsSummaryDto taskDetailsSummaryDto)
        {
            return new TaskDetailsSummaryViewModel()
                       {
                           PreviousHistory = taskDetailsSummaryDto.PreviousHistory,
                           ReoccurringSchedule = taskDetailsSummaryDto.ReoccurringSchedule
                       };
        }

        public ReoccurringSchedule ReoccurringSchedule { get; set; }
        public IEnumerable<TaskHistoryRecord> PreviousHistory { get; set; }
    }
}