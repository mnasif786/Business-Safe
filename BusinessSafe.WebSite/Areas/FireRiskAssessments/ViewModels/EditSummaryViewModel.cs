using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels
{
    public class EditSummaryViewModel
    {
        public long RiskAssessmentId { get; set; }
        public long CompanyId { get; set; }
        
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        
        public string Reference { get; set; }
        
        [Required(ErrorMessage = "Date of Assessment is required")]
        public DateTime? DateOfAssessment { get; set; }
        
        [Required(ErrorMessage = "Person Appointed is required")]
        [MaxLength(100, ErrorMessage = "Person Appointed can not be greater than 100 characters")]
        public string PersonAppointed { get; set; }

        public long? SiteId { get; set; }
        public string Site { get; set; }
        public long? RiskAssessorId { get; set; }
        public string RiskAssessor { get; set; }

        public IEnumerable<AutoCompleteViewModel> RiskAssessmentAssessors { get; set; }
        public IEnumerable<AutoCompleteViewModel> Sites { get; set; }

        public string GetDateOfAssessment()
        {
            return DateOfAssessment.HasValue ? DateOfAssessment.Value.ToShortDateString() : string.Empty;
        }
    }
}