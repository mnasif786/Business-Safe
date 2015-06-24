using BusinessSafe.Application.Request.Attributes;

namespace BusinessSafe.WebSite.Areas.Sites.ViewModels
{
    public class DelinkSiteViewModel
    {
        [GreaterThanZero("Site Id must be greater than zero")]
        public long SiteId { get; set; }
        [GreaterThanZero("Company Id must be greater than zero")]
        public long ClientId { get; set; }
    }
}