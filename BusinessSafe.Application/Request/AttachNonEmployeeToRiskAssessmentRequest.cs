using System;

namespace BusinessSafe.Application.Request
{
    public class AttachNonEmployeeToRiskAssessmentRequest
    {
        public long CompanyId { get; set; }
        public long NonEmployeeToAttachId { get; set; }
        public long RiskAssessmentId { get; set; }
        public Guid UserId { get; set; }
    }
}