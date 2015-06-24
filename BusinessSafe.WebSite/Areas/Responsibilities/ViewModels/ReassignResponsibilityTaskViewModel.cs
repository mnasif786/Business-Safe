using System;

namespace BusinessSafe.WebSite.Areas.Responsibilities.ViewModels
{
    public class ReassignResponsibilityTaskViewModel
    {
        public long CompanyId { get; set; }
        public long ResponsibilityTaskId { get; set; }
        public ResponsibilitySummaryViewModel ResponsibilitySummary { get; set; }
        public ViewResponsibilityTaskViewModel ResponsibilityTask { get; set; }

        public string ReassignTaskTo { get; set; }
        public Guid ReassignTaskToId { get; set; }
    }
}