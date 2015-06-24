using System;

namespace BusinessSafe.Application.Request
{
    public class MarkRiskAssessorAsDeletedAndUndeletedRequest
    {
        public long RiskAssessorId { get; set; }
        public long CompanyId { get; set; }
        public Guid UserId { get; set; }
    }
}