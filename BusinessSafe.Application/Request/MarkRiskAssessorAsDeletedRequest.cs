using System;

namespace BusinessSafe.Application.Request
{
    public class MarkRiskAssessorAsDeletedRequest
    {
        public long RiskAssessorId { get; set; }
        public long CompanyId { get; set; }
        public Guid UserId { get; set; }
    }
}