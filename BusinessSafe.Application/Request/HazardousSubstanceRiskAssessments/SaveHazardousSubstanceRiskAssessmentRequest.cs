using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessSafe.Application.Request.HazardousSubstanceRiskAssessments
{
    public class SaveHazardousSubstanceRiskAssessmentRequest : ISaveRiskAssessmentSummaryRequest, ICreateRiskAssessmentRequest
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
        public bool IsInhalationRouteOfEntry { get; set; }
        public bool IsIngestionRouteOfEntry { get; set; }
        public bool IsAbsorptionRouteOfEntry { get; set; }
        public string WorkspaceExposureLimits { get; set; }
        public long HazardousSubstanceId { get; set; }
        public long? SiteId { get; set; }
    }
}