using System;

namespace BusinessSafe.Application.Request
{
    public class UpdateRiskAssessmentHazardTitleRequest
    {
        public long CompanyId { get; set; }
        public long RiskAssessmentId { get; set; }
        public long RiskAssessmentHazardId { get; set; }
        public string Title { get; set; }
        public Guid UserId { get; set; }
    }
}
