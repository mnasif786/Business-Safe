using System;

namespace BusinessSafe.Application.Request.Checklists
{
    public class ResendEmployeeChecklistEmailRequest
    {
        public Guid EmployeeChecklistId { get; set; }
        public Guid ResendUserId { get; set; }
        public long RiskAssessmentId { get; set; }
    }
}