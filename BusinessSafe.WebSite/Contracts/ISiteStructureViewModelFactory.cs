using BusinessSafe.WebSite.Areas.Sites.ViewModels;

namespace BusinessSafe.WebSite.Contracts
{
    public interface ISiteStructureViewModelFactory : IViewModelFactory<SiteStructureViewModel>
    {
        ISiteStructureViewModelFactory WithClientId(long id);
        ISiteStructureViewModelFactory WithSiteDetailsViewModel(SiteDetailsViewModel siteDetailsViewModel);
        ISiteStructureViewModelFactory WithGroupDetailsViewModel(SiteGroupDetailsViewModel siteGroupDetailsViewModel);
        ISiteStructureViewModelFactory HideSiteDetails();
        ISiteStructureViewModelFactory DisplaySiteGroups();
        ISiteStructureViewModelFactory DisplaySiteDetails();
        ISiteStructureViewModelFactory WithValidationError(bool validationError);

        ISiteStructureViewModelFactory ShowClosedSites(bool value);
    }
}