using BusinessSafe.Application.Request.Attributes;

namespace BusinessSafe.WebSite.Areas.Sites.ViewModels
{
    public class DeleteSiteGroupViewModel
    {
        [GreaterThanZero("Site Group Id must be greater than zero")]
        public long GroupId { get; set; }
        [GreaterThanZero("Company Id must be greater than zero")]
        public long ClientId { get; set; }
    }
}