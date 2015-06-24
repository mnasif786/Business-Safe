using System.Web.Mvc;
using BusinessSafe.Application.Contracts.Documents;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.Documents.Controllers
{
    public class CreateAddedDocumentController : BaseController
    {
        private readonly IAddedDocumentsService _addedDocumentsService;

        public CreateAddedDocumentController(IAddedDocumentsService addedDocumentsService)
        {
            _addedDocumentsService = addedDocumentsService;
        }

        [PermissionFilter(Permissions.AddAddedDocuments)]
        public PartialViewResult Index(long companyId)
        {
            return PartialView();
        }

        [HttpPost]
        [PermissionFilter(Permissions.AddAddedDocuments)]
        public JsonResult Index(DocumentsToSaveViewModel documentsToSaveViewModel)
        {
            _addedDocumentsService.Add(documentsToSaveViewModel.CreateDocumentRequests, CurrentUser.UserId, CurrentUser.CompanyId);
            return Json(new { Success = true });
        }
    }
}