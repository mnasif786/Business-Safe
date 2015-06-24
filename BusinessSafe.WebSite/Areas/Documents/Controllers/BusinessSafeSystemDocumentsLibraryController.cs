using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.Documents.Factories;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;

namespace BusinessSafe.WebSite.Areas.Documents.Controllers
{
    public class BusinessSafeSystemDocumentsLibraryController : BaseController
    {
        private readonly IDocumentLibraryViewModelFactory _viewModelFactory;

        public BusinessSafeSystemDocumentsLibraryController(IDocumentLibraryViewModelFactory viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }

        [PermissionFilter(Permissions.ViewBusinessSafeSystem)]
        public ActionResult Index(long companyId, long documentTypeId = 0, string title = "", long siteId = 0)
        {
            var viewModel = _viewModelFactory
                                    .WithCompanyId(companyId)
                                    .WithAllowedSites(CurrentUser.GetSitesFilter())
                                    .WithDocHandlerDocumentTypeGroup(DocHandlerDocumentTypeGroup.BusinessSafeSystem)
                                    .WithDocumentTypeId(documentTypeId)
                                    .WithDocumentTitle(title)
                                    .WithSiteId(siteId)
                                    .GetViewModel();

            return View(viewModel);
        }

        [PermissionFilter(Permissions.ViewBusinessSafeSystem)]
        public ActionResult ShowEvaluationReports(long companyId)
        {
            var viewModel = _viewModelFactory
                                    .WithCompanyId(companyId)
                                    .WithAllowedSites(CurrentUser.GetSitesFilter())
                                    .WithDocHandlerDocumentTypeGroup(DocHandlerDocumentTypeGroup.BusinessSafeSystem)
                                    .WithDocumentTypeIds(GetEvaluationReportDocumentTypeIds())
                                    
                                    .GetViewModel();

            return View("Index",viewModel);
        }

        [PermissionFilter(Permissions.ViewBusinessSafeSystem)]
        public ActionResult ShowManagementSystemReports(long companyId)
        {
            var viewModel = _viewModelFactory
                                    .WithCompanyId(companyId)
                                    .WithAllowedSites(CurrentUser.GetSitesFilter())
                                    .WithDocHandlerDocumentTypeGroup(DocHandlerDocumentTypeGroup.BusinessSafeSystem)
                                    .WithDocumentTypeIds(GetManagementSystemDocumentTypeIds())

                                    .GetViewModel();

            return View("Index", viewModel);
        }

        private IList<long> GetEvaluationReportDocumentTypeIds()
        {
            return new List<long>() { 131, 142 };
        }

        private IList<long> GetManagementSystemDocumentTypeIds()
        {
            return new List<long>() { 135, 127 , 137, 136, 139, 140, 141, 128, 138};
        }
    }
}