using System.ComponentModel.DataAnnotations;

namespace BusinessSafe.WebSite.ViewModels
{
    public class SaveControlMeasureViewModel
    {
        public long RiskAssessmentId { get; set; }
        public long RiskAssessmentHazardId { get; set; }
        public long ControlMeasureId { get; set; }
        public long CompanyId { get; set; }

        [Required(ErrorMessage = "Please enter a control measure")]
        [StringLength(300, ErrorMessage = "Text cannot be greater than 300 characters")]
        public string ControlMeasure { get; set; }
        
    }
}