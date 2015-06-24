using System;

namespace BusinessSafe.Application.Request
{
    public class UpdateControlMeasureRequest
    {
        public long CompanyId { get; set; }
        public long RiskAssessmentId { get; set; }
        public long RiskAssessmentHazardId { get; set; }
        public long ControlMeasureId { get; set; }
        public string ControlMeasure { get; set; }
        public Guid UserId { get; set; }
    }
}