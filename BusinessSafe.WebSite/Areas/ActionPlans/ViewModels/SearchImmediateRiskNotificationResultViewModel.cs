using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.WebSite.Areas.ActionPlans.ViewModels
{
    public class SearchImmediateRiskNotificationResultViewModel
    {
        public long Id { get; set; }
        public string Reference { get; set; }
        public string Title { get; set; }
        public string RecommendedImmediateAction { get; set; }
        public string GuidanceNotes { get; set; }
        public string SignificantHazardIdentified { get; set; }
        public string Status { get; set; }
        public Guid? AssignedTo { get; set; }
        public DateTime? DueDate { get; set; }
        public string QuestionStatus { get; set; }

        public string DueDateFormatted
        {
            get
            {
                return (DueDate.HasValue) ? DueDate.Value.ToShortDateString() : string.Empty;
            }
        }

        public bool HasTask { get; set; }
    }
}
