using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessSafe.WebSite.ViewModels
{
    public class CreatingRiskAssessmentSummaryViewModel
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        public string Reference { get; set; }
        [Required]
        public long CompanyId { get; set; }
        public bool IsCreateDraft { get; set; }

        public CreatingRiskAssessmentSummaryViewModel()
        {
            Title = String.Empty;
            Reference = string.Empty;
        }

    }
}