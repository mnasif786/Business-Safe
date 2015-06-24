using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessSafe.Application.Request
{
    public class UpdatePersonalRiskAssessmentSummaryRequest : ISaveRiskAssessmentSummaryRequest
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }

        [Required(ErrorMessage = "Please enter Risk Assessment Title.")]
        [StringLength(200)]
        public string Title { get; set; }

        [Display(Name = "Ref")]
        [StringLength(50)]
        public string Reference { get; set; }

        public DateTime? AssessmentDate { get; set; }
        public long? RiskAssessorId { get; set; }
        public Guid UserId { get; set; }
        public bool Sensitive { get; set; }
        public long? SiteId { get; set; }
    }
}
