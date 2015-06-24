using System.Collections.Generic;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.DataTransferObjects
{
    //TODO: This does not represent an entity and so should go. PTD.
    public class TaskDetailsSummaryDto
    {
        public static TaskDetailsSummaryDto CreateFrom(Task task)
        {
            return new TaskDetailsSummaryDto()
            {
                ReoccurringSchedule = task.GetReoccurringSchedule(),
                PreviousHistory = task.GetPreviousHistory()
            };
        }

        public IEnumerable<TaskHistoryRecord> PreviousHistory { get; set; }
        public ReoccurringSchedule ReoccurringSchedule { get; set;}
    }
}