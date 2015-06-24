using System.Web.Mvc;
using BusinessSafe.Application.Contracts.HazardousSubstanceRiskAssessments;
using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Request.HazardousSubstanceRiskAssessments;
using BusinessSafe.WebSite.Areas.Filters;
using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.ViewModels;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;
using BusinessSafe.WebSite.Helpers;
using BusinessSafe.WebSite.ViewModels;

using FluentValidation;

namespace BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Controllers
{
    [HazardousSubstanceRiskAssessmentCurrentTabActionFilter(HazardousSubstancesTabs.Description)]
    public class RiskAssessmentController : BaseController
    {
        private readonly ISearchRiskAssessmentsViewModelFactory _searchRiskAssessmentsViewModelFactory;
        private readonly ICreateRiskAssessmentViewModelFactory _createRiskAssessmentViewModelFactory;
        private readonly IHazardousSubstanceRiskAssessmentService _hazardousSubstanceRiskAssessmentService;
        private const  int PAGE_SIZE = 10;

        public RiskAssessmentController(
            ISearchRiskAssessmentsViewModelFactory searchRiskAssessmentsViewModelFactory,
            ICreateRiskAssessmentViewModelFactory createRiskAssessmentViewModelFactory,
            IHazardousSubstanceRiskAssessmentService hazardousSubstanceRiskAssessmentService, IRiskAssessmentService riskAssessmentService)
        {
            _searchRiskAssessmentsViewModelFactory = searchRiskAssessmentsViewModelFactory;
            _createRiskAssessmentViewModelFactory = createRiskAssessmentViewModelFactory;
            _hazardousSubstanceRiskAssessmentService = hazardousSubstanceRiskAssessmentService;
        }

        [PermissionFilter(Permissions.ViewGeneralandHazardousSubstancesRiskAssessments)]
        public ViewResult Index(HazardousSubstanceRiskAssessmentSearchViewModel model)
        {
            var viewModel =
                _searchRiskAssessmentsViewModelFactory
                    .WithTitle(model.title)
                    .WithCompanyId(CurrentUser.CompanyId)
                    .WithCreatedFrom(model.createdFrom)
                    .WithCreatedTo(model.createdTo)
                    .WithHazardousSubstanceId(model.hazardousSubstanceId)
                    .WithAllowedSiteIds(CurrentUser.GetSitesFilter())
                    .WithCurrentUserId(CurrentUser.UserId)
                    .WithSiteId(model.siteId)
                    .WithSiteGroupId(model.siteGroupId)
                    .WithShowDeleted(model.showDeleted)
                    .WithShowArchived(model.showArchived)
                    .WithPageNumber(model.Page !=default(int) ? model.Page : 1)
                    .WithPageSize(model.Size != default(int) ? model.Size : PAGE_SIZE)
                    .WithOrderBy(model.OrderBy)
                    .GetViewModel(_hazardousSubstanceRiskAssessmentService);

            return View(viewModel);
        }

        [PermissionFilter(Permissions.AddGeneralandHazardousSubstancesRiskAssessments)]
        public ActionResult Create(long companyId, long? newHazardousSubstanceId)
        {
            var hazardousSubstanceSummaryViewModel = _createRiskAssessmentViewModelFactory
                .WithCompanyId(companyId)
                .WithNewHazardousSubstanceId(newHazardousSubstanceId)
                .GetViewModel();

            return View("Create", hazardousSubstanceSummaryViewModel);
        }

        [HttpPost]
        [PermissionFilter(Permissions.AddGeneralandHazardousSubstancesRiskAssessments, Permissions.EditGeneralandHazardousSubstancesRiskAssessments)]
        public ActionResult Create(CreateRiskAssessmentViewModel viewModel)
        {

            if (!ModelState.IsValid)
                return View("Create", _createRiskAssessmentViewModelFactory.WithCompanyId(viewModel.CompanyId).GetViewModel(viewModel));


            long riskAssessmentId;
            try
            {
                riskAssessmentId = _hazardousSubstanceRiskAssessmentService.CreateRiskAssessment(new SaveHazardousSubstanceRiskAssessmentRequest
                {
                    CompanyId = viewModel.CompanyId,
                    Title = viewModel.Title,
                    Reference = viewModel.Reference,
                    UserId = CurrentUser.UserId,
                    HazardousSubstanceId = viewModel.NewHazardousSubstanceId
                });
            }
            catch (ValidationException validationException)
            {
                ModelState.AddValidationErrors(validationException);
                return View("Create", _createRiskAssessmentViewModelFactory.WithCompanyId(viewModel.CompanyId).GetViewModel(viewModel));
            }


            if (viewModel.IsCreateDraft)
                return RedirectToAction("Index", new { companyId = viewModel.CompanyId });

            return RedirectToAction("Index", "Summary", new { riskAssessmentId, viewModel.CompanyId });
        }

        [HttpPost]
        [PermissionFilter(Permissions.AddGeneralandHazardousSubstancesRiskAssessments, Permissions.EditGeneralandHazardousSubstancesRiskAssessments)]
        public JsonResult Copy(CopyRiskAssessmentViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ModelStateErrorsWithKeysAsJson();
            }

            try
            {
                var result = _hazardousSubstanceRiskAssessmentService.CopyRiskAssessment(
                    new CopyRiskAssessmentRequest
                    {
                        CompanyId = CurrentUser.CompanyId,
                        RiskAssessmentToCopyId = viewModel.RiskAssessmentId,
                        Reference = viewModel.Reference,
                        Title = viewModel.Title,
                        UserId = CurrentUser.UserId
                    });


                return Json(new { Success = true, Id = result });
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
                _hazardousSubstanceRiskAssessmentService.CopyForMultipleSites(
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
