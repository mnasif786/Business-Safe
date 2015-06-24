using System.Web.Mvc;
using BusinessSafe.Application.Custom_Exceptions;
using BusinessSafe.WebSite.Areas.ActionPlans.Factories;
using BusinessSafe.WebSite.Areas.ActionPlans.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.ClientDocumentService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;
using BusinessSafe.WebSite.Helpers;
using BusinessSafe.WebSite.StreamingClientDocumentService;
using BusinessSafe.WebSite.StreamingDocumentLibraryService;
using Telerik.Web.Mvc;

namespace BusinessSafe.WebSite.Areas.ActionPlans.Controllers
{
    public class ActionPlanController : BaseController
    {
        private readonly IClientDocumentService _clientDocumentService;

        private readonly ISearchActionPlanViewModelFactory _searchActionPlanViewModelFactory;
        //
        // GET: /ActionPlan/ActionPlan/

        public ActionPlanController(ISearchActionPlanViewModelFactory searchActionPlanViewModelFactory,
                                    IClientDocumentService clientDocumentService)
        {
            _searchActionPlanViewModelFactory = searchActionPlanViewModelFactory;
            _clientDocumentService = clientDocumentService;
        }

        [GridAction(EnableCustomBinding = true, GridName = "ActionPlanGrid")]
        [PermissionFilter(Permissions.ViewActionPlan)]
        public ActionResult Index(ActionPlanIndexViewModel model)
        {
            var result = _searchActionPlanViewModelFactory
                .WithAllowedSiteIds(CurrentUser.GetSitesFilter())
                .WithCompanyId(CurrentUser.CompanyId)
                .WithSiteGroupId(model.SiteGroupId)
                .WithSiteId(model.SiteId)
                .WithSubmittedFrom(model.SubmittedFrom)
                .WithSubmittedTo(model.SubmittedTo)
                .WithAllowedSiteIds(CurrentUser.GetSitesFilter())
                .WithPageNumber(model.Page)
                .WithPageSize(model.Size)
                .WithShowArchived(model.IsShowArchived)
                .WithOrderBy(model.OrderBy)
                .GetViewModel();

            return View(result);
        }


        [PermissionFilter(Permissions.ViewActionPlan)]
        public FileContentResult DownloadClientDocument(long clientDocumentId)
        {         
            var document = _clientDocumentService.GetByIdWithContent( clientDocumentId );

            if (document.ClientId.HasValue && document.ClientId != CurrentUser.CompanyId)
                throw new InvalidDocumentForCompanyException(clientDocumentId, CurrentUser.CompanyId);

            var contentType = ContentTypeHelper.GetContentTypeFromExtension(document.Extension);
            
            return File( document.FileBytes, contentType, document.OriginalFilename);
        }
    }
}
