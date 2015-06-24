using System;

namespace BusinessSafe.Application.Request
{
    public class AddControlMeasureRequest
    {
        public long CompanyId { get; set; }
        public long RiskAssessmentId { get; set; }
        public long RiskAssessmentHazardId { get; set; }
        public string ControlMeasure { get; set; }
        public Guid UserId { get; set; }
    }
}