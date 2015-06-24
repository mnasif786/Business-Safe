using System;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.Documents;
using BusinessSafe.Application.Request.Documents;
using BusinessSafe.WebSite.Areas.Documents.Factories;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;

namespace BusinessSafe.WebSite.Areas.Documents.Controllers
{
    public class AddedDocumentsLibraryController : BaseController
    {
        private readonly IAddedDocumentsLibraryViewModelFactory _viewModelFactory;
        private readonly IDocumentService _documentService;
        public AddedDocumentsLibraryController(IDocumentService documentService, IAddedDocumentsLibraryViewModelFactory viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
            _documentService = documentService;
        }

        [PermissionFilter(Permissions.ViewAddedDocuments)]
        public ActionResult Index(long documentTypeId = 0, long siteId = 0, long siteGroupId = 0, string title = "")
        {
            var viewModel = _viewModelFactory
                                    .WithCompanyId(CurrentUser.CompanyId)
                                    .WithDocumentTypeId(documentTypeId)
                                    .WithSiteId(siteId)
                                    .WithSiteGroupId(siteGroupId)
                                    .WithDocumentTitle(title)
                                    .WithAllowedSiteIds(CurrentUser.GetSitesFilter())
                                    .GetViewModel();

            return View(viewModel);
        }

        [HttpPost]
        [PermissionFilter(Permissions.DeleteAddedDocuments)]
        public JsonResult MarkDocumentAsDeleted(long companyId, long documentId)
        {
            if (companyId == 0 || documentId == 0)
                throw new ArgumentException("Invalid documentId and companyId");

            _documentService.MarkDocumentAsDeleted(new MarkDocumentAsDeletedRequest() { DocumentId = documentId, CompanyId = companyId, UserId = CurrentUser.UserId });

            return Json(new { Success = true });
        }
    }
}