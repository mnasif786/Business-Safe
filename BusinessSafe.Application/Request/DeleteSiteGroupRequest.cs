using System;
using BusinessSafe.Application.Request.Attributes;

namespace BusinessSafe.Application.Request
{
    public class DeleteSiteGroupRequest
    {
        [GreaterThanZero("Site Group Id must be greater than zero")]
        public long GroupId { get; set; }

        [GreaterThanZero("Company Id must be greater than zero")]
        public long CompanyId { get; set; }
        public Guid UserId { get; set; }
    }
}