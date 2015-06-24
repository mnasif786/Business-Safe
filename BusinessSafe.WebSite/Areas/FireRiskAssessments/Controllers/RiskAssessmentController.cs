using System.Web.Mvc;
using BusinessSafe.Application.Contracts.FireRiskAssessments;
using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;
using BusinessSafe.WebSite.Helpers;
using BusinessSafe.WebSite.ViewModels;

using FluentValidation;
using Telerik.Web.Mvc;
using CreatingRiskAssessmentSummaryViewModel = BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels.CreatingRiskAssessmentSummaryViewModel;

namespace BusinessSafe.WebSite.Areas.FireRiskAssessments.Controllers
{
    
    public class RiskAssessmentController : BaseController
    {
        private readonly ISearchRiskAssessmentViewModelFactory _searchRiskAssessmentViewModelFactory;
        private readonly IFireRiskAssessmentService _fireRiskAssessmentService;

        private const int PAGE_SIZE = 10;
        public RiskAssessmentController(
            ISearchRiskAssessmentViewModelFactory searchRiskAssessmentViewModelFactory,
            IFireRiskAssessmentService fireRiskAssessmentService)
        {
            _searchRiskAssessmentViewModelFactory = searchRiskAssessmentViewModelFactory;
            _fireRiskAssessmentService = fireRiskAssessmentService;
        }

        [GridAction(EnableCustomBinding = true, GridName = "FireRiskAssessmentsGrid")]
        [PermissionFilter(Permissions.ViewFireRiskAssessments)]
        public ActionResult Index(RiskAssessmentSearchViewModel model)
        {
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
                                .WithPageNumber(model.Page != default(int) ? model.Page : 1 )
                                .WithPageSize(model.Size != default(int) ? model.Size : PAGE_SIZE )
                                .WithOrderBy(model.OrderBy)
                                .GetViewModel();

            return View("Index", viewModel);
        }

        [PermissionFilter(Permissions.AddFireRiskAssessments)]
        public ActionResult Create()
        {
            var riskAssessmentSummaryViewModel = new CreatingRiskAssessmentSummaryViewModel
            {
                CompanyId = CurrentUser.CompanyId
            };

            return View("Create", riskAssessmentSummaryViewModel);
        }

        [HttpPost]
        [PermissionFilter(Permissions.AddGeneralandHazardousSubstancesRiskAssessments, Permissions.EditFireRiskAssessments)]
        public ActionResult Create(CreatingRiskAssessmentSummaryViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", viewModel);
            }

            try
            {
                var riskAssessmentId = _fireRiskAssessmentService.CreateRiskAssessment(new CreateRiskAssessmentRequest
                {
                    CompanyId = CurrentUser.CompanyId,
                    Title = viewModel.Title,
                    Reference = viewModel.Reference,
                    UserId = CurrentUser.UserId
                });

                return RedirectToAction("Index", "Summary", new { riskAssessmentId = riskAssessmentId, companyId = CurrentUser.CompanyId });
            }
            catch (ValidationException validationException)
            {
                ModelState.AddValidationErrors(validationException);
                return View("Create", viewModel);
            }
        }

        [HttpPost]
        [PermissionFilter(Permissions.AddGeneralandHazardousSubstancesRiskAssessments, Permissions.EditFireRiskAssessments)]
        public JsonResult Copy(CopyRiskAssessmentViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ModelStateErrorsWithKeysAsJson();
            }

            try
            {
                var result = _fireRiskAssessmentService.CopyRiskAssessment(
                    new CopyRiskAssessmentRequest
                        {
                            CompanyId = CurrentUser.CompanyId,
                            RiskAssessmentToCopyId = viewModel.RiskAssessmentId,
                            Reference = viewModel.Reference,
                            Title = viewModel.Title,
                            UserId = CurrentUser.UserId
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
                _fireRiskAssessmentService.CopyForMultipleSites(
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
