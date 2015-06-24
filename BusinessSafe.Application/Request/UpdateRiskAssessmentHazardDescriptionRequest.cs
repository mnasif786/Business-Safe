using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessSafe.Application.Request
{
    public class UpdateRiskAssessmentHazardDescriptionRequest
    {
        public long CompanyId { get; set; }
        public long RiskAssessmentId { get; set; }
        public long RiskAssessmentHazardId { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
    }
}