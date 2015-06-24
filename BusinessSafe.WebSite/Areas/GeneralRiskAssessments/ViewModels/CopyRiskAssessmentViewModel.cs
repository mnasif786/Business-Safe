using System.ComponentModel.DataAnnotations;

namespace BusinessSafe.WebSite.Areas.GeneralRiskAssessments.ViewModels
{
    public class CopyRiskAssessmentViewModel
    {
        [Required]
        public long RiskAssessmentId { get; set; }
        public string Reference { get; set; }
        [Required(ErrorMessage = "Please enter a title")]
        public string Title { get; set; }
    }
}