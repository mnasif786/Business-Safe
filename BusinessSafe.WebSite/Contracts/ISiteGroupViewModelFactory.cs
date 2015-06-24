using BusinessSafe.WebSite.Areas.Sites.ViewModels;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Contracts
{
    public interface ISiteGroupViewModelFactory: IViewModelFactory<SiteGroupDetailsViewModel>
    {
        ISiteGroupViewModelFactory WithSiteGroupId(long siteGroupId);
        ISiteGroupViewModelFactory WithClientId(long clientId);
    }
}