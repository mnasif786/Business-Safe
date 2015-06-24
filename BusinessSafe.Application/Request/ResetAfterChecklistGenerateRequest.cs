using System;

namespace BusinessSafe.Application.Request
{
    public class ResetAfterChecklistGenerateRequest
    {
        public long PersonalRiskAssessmentId { get; set; }
        public long CompanyId { get; set; }
        public Guid CurrentUserId { get; set; }
    }
}