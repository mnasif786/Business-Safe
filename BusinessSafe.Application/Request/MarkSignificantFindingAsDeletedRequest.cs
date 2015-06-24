using System;

namespace BusinessSafe.Application.Request
{
    public class MarkSignificantFindingAsDeletedRequest
    {
        public long CompanyId { get; set; }
        public long FireChecklistId { get; set; }
        public long FireQuestionId { get; set; }
        public Guid UserId { get; set; }
    }
}