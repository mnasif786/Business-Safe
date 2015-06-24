using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessSafe.WebSite.Areas.ActionPlans.ViewModels
{
    public class SearchActionPlanResultViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Site { get; set; }
        public string VisitBy { get; set; }
        public string Status { get; set; }
        public DateTime SubmittedDate { get; set; }
        public DateTime? VisitDate { get; set; }
        public bool IsDeleted { get; set; }
        public long? EvaluationReportId { get; set; }

        public string SubmittedDateFormatted
        {
            get
            {
                return SubmittedDate.ToShortDateString();
            }
        }

        public string VisitDateFormatted
        {
            get
            {
                return VisitDate.HasValue ? VisitDate.Value.ToShortDateString() : string.Empty;
            }
        }
    }
}
