using System.ComponentModel.DataAnnotations;

namespace BusinessSafe.WebSite.Areas.GeneralRiskAssessments.ViewModels
{
    public class MarkRiskAssessmentIsDraftStatusViewModel
    {
        [Required]
        public long CompanyId { get; set; }
        [Required]
        public long RiskAssessmentId { get; set; }
        [Required]
        public bool IsDraft { get; set; }
        
    }
}