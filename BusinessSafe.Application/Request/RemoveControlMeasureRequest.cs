using System;

namespace BusinessSafe.Application.Request
{
    public class RemoveControlMeasureRequest
    {
        public long CompanyId { get; set; }
        public long RiskAssessmentId { get; set; }
        public long RiskAssessmentHazardId { get; set; }
        public long ControlMeasureId { get; set; }
        public Guid UserId { get; set; }
    }
}