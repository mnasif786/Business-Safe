using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.ViewModels
{
    public class EditSummaryViewModel
    {
        public long RiskAssessmentId { get; set; }
        public long CompanyId { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required]
        public string Reference { get; set; }
        [Required(ErrorMessage = "Date of Assessment should be selected")]
        public DateTime? DateOfAssessment { get; set; }
        public string HazardousSubstance { get; set; }
        [Required(ErrorMessage = "The Hazardous Substance is required")]
        public long HazardousSubstanceId { get; set; }

        public long? SiteId { get; set; }
        public string Site { get; set; }
        public long? RiskAssessorId { get; set; }
        public string RiskAssessor { get; set; }

        public IEnumerable<AutoCompleteViewModel> RiskAssessmentAssessors { get; set; }
        public IEnumerable<AutoCompleteViewModel> HazardousSubstances { get; set; }
        public IEnumerable<AutoCompleteViewModel> Sites { get; set; }
        
        public string GetDateOfAssessment()
        {
            return DateOfAssessment.HasValue ? DateOfAssessment.Value.ToShortDateString() : string.Empty;
        }
    }
}