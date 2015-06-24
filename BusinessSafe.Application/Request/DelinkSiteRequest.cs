using BusinessSafe.Application.Request.Attributes;

namespace BusinessSafe.Application.Request
{
    public class DelinkSiteRequest
    {
        [GreaterThanZero("Site Id must be greater than zero")]
        public long SiteId { get; set; }

        [GreaterThanZero("Company Id must be greater than zero")]
        public long CompanyId { get; set; }
    }
}