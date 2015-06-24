using System;
using BusinessSafe.Application.Helpers;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.WebSite.Areas.Responsibilities.ViewModels
{
    public class ResponsibilityTasksViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AssignedTo { get; set; }
        public string Site { get; set; }
        public DateTime Created { get; set; }
        public DateTime? DueDate { get; set; }
        public string Status { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsNoLongerRequired { get; set; }
        public bool IsReoccurring { get; set; }
        public TaskReoccurringType TaskReoccurringType { get; set; }
        public DateTime? TaskReoccurringEndDate { get; set; }
        public string CreatedDateFormatted
        {
            get
            {
                return Created.ToShortDateString();
            }
        }

        public string DueDateFormatted
        {
            get
            {
                return DueDate != null ? DueDate.Value.ToShortDateString() : null;
            }
        }


        public string GetReoccurringFrequencyDetails()
        {
            if (IsReoccurring)
            {
                return TaskReoccurringTypeHelper.GetReoccurringFrequencyDetails(TaskReoccurringType, TaskReoccurringEndDate,false);
            }
            return string.Empty;
        }
    }
}