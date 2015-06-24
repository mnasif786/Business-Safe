using System;
using System.Collections.Generic;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.DataTransferObjects
{
    public class HazardousSubstanceRiskAssessmentDto
    {
        public long CompanyId { get; set; }
        public long Id { get; set; }
        public string Title { get; set; }
        public string Reference { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? NextReviewDate { get; set; }
        public AuditedUserDto CreatedBy { get; set; }
        public RiskAssessmentStatus Status { get; set; }
        public RiskAssessorDto RiskAssessor { get; set; }
        public DateTime? AssessmentDate { get; set; }
        public bool Deleted { get; set; }
        public bool IsInhalationRouteOfEntry { get; set; }
        public bool IsIngestionRouteOfEntry { get; set; }
        public bool IsAbsorptionRouteOfEntry { get; set; }
        public string WorkspaceExposureLimits { get; set; }
        public IList<Tuple<Guid, string>> Employees { get; set; }
        public IList<Tuple<long, string>> NonEmployees { get; set; }
        public HazardousSubstanceDto HazardousSubstance { get; set; }
        public SiteStructureElementDto Site { get; set; }
        public HazardousSubstanceGroupDto Group { get; set; }
        public Quantity? Quantity { get; set; }
        public MatterState? MatterState { get; set; }
        public DustinessOrVolatility? DustinessOrVolatility { get; set; }
        public IEnumerable<HazardousSubstanceRiskAssessmentControlMeasureDto> ControlMeasures { get; set; }
        public bool? HealthSurveillanceRequired { get; set; }
        public IEnumerable<TaskDto> FurtherControlMeasureTasks { get; set; }
        public DateTime? CompletionDueDate { get; set; }
        public List<RiskAssessmentReviewDto> Reviews { get; set; }

        public HazardousSubstanceRiskAssessmentDto()
        {
            Employees = new List<Tuple<Guid, string>>();
            NonEmployees = new List<Tuple<long, string>>();
            ControlMeasures = new List<HazardousSubstanceRiskAssessmentControlMeasureDto>();
            FurtherControlMeasureTasks = new List<TaskDto>();
            Reviews = new List<RiskAssessmentReviewDto>();
        }
    }
}