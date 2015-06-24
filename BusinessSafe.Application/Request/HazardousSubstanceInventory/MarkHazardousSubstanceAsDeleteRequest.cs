using System;

namespace BusinessSafe.Application.Request.HazardousSubstanceInventory
{
    public class ReinstateHazardousSubstanceRequest
    {
        public long HazardousSubstanceId { get; set; }
        public Guid UserId { get; set; }
        public long CompanyId { get; set; }
    }
}