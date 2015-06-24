using System.Linq;
using System.Web.Mvc;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.Documents.Factories;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.DocumentSubTypeService;
using BusinessSafe.WebSite.Extensions;
using BusinessSafe.WebSite.Filters;
using BusinessSafe.WebSite.Helpers;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.Documents.Controllers
{
    public class ReferenceDocumentsLibraryController : BaseController
    {
        private readonly IDocumentLibraryViewModelFactory _viewModelFactory;
        private readonly IDocumentSubTypeService _documentSubTypeService;
        private readonly ICacheHelper _cacheHelper;
        private const int DepartmentId = 2;

        public ReferenceDocumentsLibraryController(IDocumentLibraryViewModelFactory viewModelFactory, IDocumentSubTypeService documentSubTypeService, ICacheHelper cacheHelper)
        {
            _viewModelFactory = viewModelFactory;
            _documentSubTypeService = documentSubTypeService;
            _cacheHelper = cacheHelper;
        }

        [PermissionFilter(Permissions.ViewReferenceLibrary)]
        public ActionResult Index(long companyId, long documentTypeId = 0, long documentSubTypeId = 0, string title = "", string keywords = "")
        {
            var viewModel = _viewModelFactory
                                    .WithCompanyId(companyId)
                                    .WithDocHandlerDocumentTypeGroup(DocHandlerDocumentTypeGroup.ReferenceLibrary)
                                    .WithDocumentTypeId(documentTypeId)
                                    .WithDocumentSubTypeId(documentSubTypeId)
                                    .WithDocumentTitle(title)
                                    .WithKeywords(keywords)
                                    .GetViewModel();

            return View(viewModel);
        }

        [HttpGet]
        [PermissionFilter(Permissions.ViewReferenceLibrary)]
        public JsonResult GetDocumentSubTypesByDocumentTypeId(long documentTypeId)
        {
            const string key = "DocumentSubTypes";
            DocumentSubTypeDto[] documentSubTypes;

            if (!_cacheHelper.Load(key, out documentSubTypes))
            {
                documentSubTypes = _documentSubTypeService.GetByDepartmentId(DepartmentId);
                _cacheHelper.Add(documentSubTypes, key, 60);
            }

            var filteredDocumentSubTypes = documentSubTypes.Where(d => d.DocumentType.Id == documentTypeId).OrderBy(d => d.Title);
            var result = filteredDocumentSubTypes.Select(AutoCompleteViewModel.ForDocumentType).AddDefaultOption();

            return Json(result , JsonRequestBehavior.AllowGet);
        }
    }
}