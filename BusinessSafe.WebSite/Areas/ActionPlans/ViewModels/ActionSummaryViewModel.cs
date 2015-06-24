using System;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.WebSite.Areas.ActionPlans.ViewModels
{
    public class ActionSummaryViewModel
    {
        public long Id { get; set; }
        public string Reference { get; set; }
        public string AreaOfNonCompliance { get; set; }
        public string ActionRequired { get; set; }
        public string GuidanceNotes { get; set; }
        public string TargetTimescale { get; set; }
        public Guid? AssignedTo { get; set; }
        public DateTime? DueDate { get; set; }
        public string Status { get; set; }
        public string Title { get; set; }

        public ActionCategory Category { get; set; }

        public string DueDateFormatted
        {
            get { return DueDate.HasValue ? DueDate.Value.ToShortDateString() : string.Empty; }
        }
    }
}