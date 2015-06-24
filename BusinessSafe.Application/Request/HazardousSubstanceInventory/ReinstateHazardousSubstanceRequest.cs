using System;

namespace BusinessSafe.Application.Request.HazardousSubstanceInventory
{
    public class MarkHazardousSubstanceAsDeleteRequest
    {
        public long HazardousSubstanceId { get; set; }
        public Guid UserId { get; set; }
        public long CompanyId { get; set; }
    }
}