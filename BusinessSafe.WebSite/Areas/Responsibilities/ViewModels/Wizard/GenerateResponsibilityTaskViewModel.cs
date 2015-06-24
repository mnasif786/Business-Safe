using System;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.WebSite.Areas.Responsibilities.ViewModels.Wizard
{
    public class GenerateResponsibilityTaskViewModel
    {
        public long SiteId { get; set; }
        public long ResponsibilityId { get; set; }
        public long TaskId { get; set; }
        public TaskReoccurringType Frequency { get; set; }
        public Guid? Owner { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}