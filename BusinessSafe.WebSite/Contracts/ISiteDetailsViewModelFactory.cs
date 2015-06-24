using BusinessSafe.WebSite.Areas.Sites.ViewModels;

namespace BusinessSafe.WebSite.Contracts
{
    public interface ISiteDetailsViewModelFactory : IViewModelFactory<SiteDetailsViewModel>
    {
        ISiteDetailsViewModelFactory WithId(long id);
        ISiteDetailsViewModelFactory WithSiteId(long siteId);
        ISiteDetailsViewModelFactory WithClientId(long id);
    }
}