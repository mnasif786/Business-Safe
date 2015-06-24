using System.Web.Mvc;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Contracts;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;

namespace BusinessSafe.WebSite.Areas.Sites.Controllers
{
    public class SitesStructureController : BaseController
    {
        private readonly ISiteStructureViewModelFactory _siteStructureViewModelFactory;

        public SitesStructureController(ISiteStructureViewModelFactory siteStructureViewModelFactory)
        {
            _siteStructureViewModelFactory = siteStructureViewModelFactory;
        }

        [PermissionFilter(Permissions.ViewSiteDetails)]
        public ViewResult Index()
        {
            var viewModel = _siteStructureViewModelFactory
                .WithClientId(CurrentUser.CompanyId)
                .DisplaySiteDetails()
                .GetViewModel();

            return View("Index", viewModel);
        }
        
        [PermissionFilter(Permissions.ViewSiteDetails)]
        public ViewResult GetAllSites()
        {
            var viewModel = _siteStructureViewModelFactory
                .WithClientId(CurrentUser.CompanyId)
                .DisplaySiteDetails()
                .ShowClosedSites(true)
                .GetViewModel();

            return View("Index", viewModel);
        }

        [PermissionFilter(Permissions.ViewSiteDetails)]
        public ViewResult SiteOrganisationalChart(long siteOrganistionalChartId)
        {
            var viewModel = _siteStructureViewModelFactory
                .WithClientId(CurrentUser.CompanyId)
                .GetViewModel();

            return View("IndexSiteOrganisationalChart", viewModel);
        }
    }
}