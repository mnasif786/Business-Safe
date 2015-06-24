using System.Web.Mvc;
using BusinessSafe.Application.Contracts.GeneralRiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Filters;
using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;
using BusinessSafe.WebSite.Helpers;
using BusinessSafe.WebSite.ViewModels;

using FluentValidation;
using Telerik.Web.Mvc;
using Telerik.Web.Mvc.UI;
using CreatingRiskAssessmentSummaryViewModel = BusinessSafe.WebSite.ViewModels.CreatingRiskAssessmentSummaryViewModel;

namespace BusinessSafe.WebSite.Areas.GeneralRiskAssessments.Controllers
{
    [GeneralRiskAssessmentCurrentTabActionFilter(GeneralRiskAssessmentTabs.PremisesInformation)]
    public class RiskAssessmentController : BaseController
    {
        private readonly IGeneralRiskAssessmentService _generalRiskAssessmentService;
        private readonly ISearchRiskAssessmentViewModelFactory _searchRiskAssessmentViewModelFactory;
        private const  int PAGE_SIZE = 10;
        public RiskAssessmentController(
            IGeneralRiskAssessmentService generalRiskAssessmentService,
            ISearchRiskAssessmentViewModelFactory searchRiskAssessmentViewModelFactory)
        {
            _generalRiskAssessmentService = generalRiskAssessmentService;
            _searchRiskAssessmentViewModelFactory = searchRiskAssessmentViewModelFactory;
        }
        [GridAction(EnableCustomBinding = true, GridName = "GeneralRiskAssessmentsGrid")]
        [PermissionFilter(Permissions.ViewGeneralandHazardousSubstancesRiskAssessments)]
        public ActionResult Index(RiskAssessmentSearchViewModel model)
        {
            
           // var sortSettings = new GridSortSettings()

            var viewModel = _searchRiskAssessmentViewModelFactory
                                .WithTitle(model.title)
                                .WithCompanyId(CurrentUser.CompanyId)
                                .WithCreatedFrom(model.createdFrom)
                                .WithCreatedTo(model.createdTo)
                                .WithSiteGroupId(model.siteGroupId)
                                .WithSiteId(model.siteId)
                                .WithShowDeleted(model.showDeleted)
                                .WithShowArchived(model.showArchived)
                                .WithAllowedSiteIds(CurrentUser.GetSitesFilter())
                                .WithCurrentUserId(CurrentUser.UserId)
                                .WithPageNumber(model.Page !=default(int) ? model.Page : 1)
                                .WithPageSize(model.Size != default(int) ? model.Size : PAGE_SIZE)
                                .WithOrderBy(model.OrderBy)
                                .GetViewModel();

            return View("Index", viewModel);
        }

        [PermissionFilter(Permissions.AddGeneralandHazardousSubstancesRiskAssessments)]
        public ActionResult Create(long companyId)
        {
            var generalRiskAssessmentSummaryViewModel = new CreatingRiskAssessmentSummaryViewModel
                                                            {
                                                                CompanyId = companyId
                                                            };
            return View("Create", generalRiskAssessmentSummaryViewModel);
        }

        [HttpPost]
        [PermissionFilter(Permissions.AddGeneralandHazardousSubstancesRiskAssessments, Permissions.EditGeneralandHazardousSubstancesRiskAssessments)]
        public ActionResult Create(CreatingRiskAssessmentSummaryViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", viewModel);
            }

            long riskAssessmentId;
            try
            {
                riskAssessmentId = _generalRiskAssessmentService.CreateRiskAssessment(new CreateRiskAssessmentRequest
                                                                                   {
                                                                                       CompanyId = viewModel.CompanyId,
                                                                                       Title = viewModel.Title,
                                                                                       Reference = viewModel.Reference,
                                                                                       UserId = CurrentUser.UserId
                                                                                   });
            }
            catch (ValidationException validationException)
            {
                ModelState.AddValidationErrors(validationException);
                return View("Create", viewModel);
            }

            return RedirectToAction("Index", "Summary", new { riskAssessmentId, companyId = CurrentUser.CompanyId });

        }
        
        [HttpPost]
        [PermissionFilter(Permissions.AddGeneralandHazardousSubstancesRiskAssessments)]
        public JsonResult Copy(CopyRiskAssessmentViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ModelStateErrorsWithKeysAsJson();
            }

            try
            {
                var result = _generalRiskAssessmentService.CopyRiskAssessment(
                    new CopyRiskAssessmentRequest
                        {
                            CompanyId = CurrentUser.CompanyId,
                            RiskAssessmentToCopyId = viewModel.RiskAssessmentId,
                            Reference = viewModel.Reference,
                            UserId = CurrentUser.UserId,
                            Title = viewModel.Title

                        });
                
                return Json(new {Success = true, Id = result});
            }
            catch (ValidationException validationException)
            {
                ModelState.AddValidationErrors(validationException);
                return ModelStateErrorsWithKeysAsJson();
            }
        }

        [HttpPost]
        [PermissionFilter(Permissions.AddGeneralandHazardousSubstancesRiskAssessments)]
        public JsonResult CopyForMultipleSites(CopyRiskAssessmentForMultipleSitesViewModel viewModel)
        {
            if (viewModel.SiteIds.Count <= 0)
            {
                ModelState.AddModelError(string.Empty, "Please select at least one site");
            }

            if (!ModelState.IsValid)
            {
                return ModelStateErrorsWithKeysAsJson();
            }

            try
            {
                _generalRiskAssessmentService.CopyForMultipleSites(
                    new CopyRiskAssessmentForMultipleSitesRequest
                    {
                        CompanyId = CurrentUser.CompanyId,
                        RiskAssessmentToCopyId = viewModel.RiskAssessmentId,
                        Reference = viewModel.Reference,
                        UserId = CurrentUser.UserId,
                        Title = viewModel.Title,
                        SiteIds = viewModel.SiteIds
                    });

                TempData["Notice"] = "Successfully Copied to Multiple Sites";

                return Json(new { Success = true });
            }
            catch (ValidationException validationException)
            {
                ModelState.AddValidationErrors(validationException);
                return ModelStateErrorsWithKeysAsJson();
            }
        }
    }
}