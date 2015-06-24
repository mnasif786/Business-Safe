using System.Web.Mvc;
using BusinessSafe.WebSite.Areas.Documents.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace BusinessSafe.WebSite.Areas.Responsibilities.ViewModels
{
    public class CompleteResponsibilityTaskViewModel
    {
        public long CompanyId { get; set; }
        public long ResponsibilityId { get; set; }
        public long ResponsibilityTaskId { get; set; }
        public ResponsibilitySummaryViewModel ResponsibilitySummary { get; set; }

        [StringLength(250)]
        public string CompletedComments { get; set; }
        public ViewResponsibilityTaskViewModel ResponsibilityTask { get; set; }
        
    }
}