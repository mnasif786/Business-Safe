using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.ViewModels
{
    public class CreateRiskAssessmentViewModel
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        public string Reference { get; set; }
        [Required]
        public long CompanyId { get; set; }
        public bool IsCreateDraft { get; set; }
        [Required(ErrorMessage = "Please select a Hazardous Substance to assess")]
        public long NewHazardousSubstanceId { get; set; }
        public IEnumerable<AutoCompleteViewModel> HazardousSubstances { get; set; }
    }
}