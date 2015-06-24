using System;

namespace BusinessSafe.Application.Request.FireRiskAssessments
{
    public class MarkChecklistWithCompleteFailureAttemptRequest
    {
        public long ChecklistId { get; set; }
        public Guid UserId { get; set; }
        public long CompanyId { get; set; }
    }
}