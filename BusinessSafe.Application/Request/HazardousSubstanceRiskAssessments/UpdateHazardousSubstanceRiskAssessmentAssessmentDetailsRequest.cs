using System;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Request.HazardousSubstanceRiskAssessments
{
    public class UpdateHazardousSubstanceRiskAssessmentAssessmentDetailsRequest
    {
        public long RiskAssessmentId { get; set; }
        public long CompanyId { get; set; }
        public Quantity? Quantity { get; set; }
        public MatterState? MatterState { get; set; }
        public DustinessOrVolatility? DustinessOrVolatility { get; set; }
        public bool HealthSurveillanceRequired { get; set; }
        public Guid CurrentUserId { get; set; }
        public long ControlSystemId { get; set; }
    }
}
