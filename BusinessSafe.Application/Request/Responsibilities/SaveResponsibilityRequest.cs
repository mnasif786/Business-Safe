using System;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Request.Responsibilities
{
    public class SaveResponsibilityRequest
    {
        public long CompanyId { get; set; }
        public long ResponsibilityId { get; set; }
        public long ResponsibilityCategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long SiteId { get; set; }
        public long ResponsibilityReasonId { get; set; }
        public Guid? OwnerId { get; set; }
        public TaskReoccurringType TaskReoccurringType { get; set; }
        public Guid UserId { get; set; }
    }
}