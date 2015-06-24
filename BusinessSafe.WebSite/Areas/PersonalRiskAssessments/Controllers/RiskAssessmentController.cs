using System.Web.Mvc;
using BusinessSafe.Application.Contracts.PersonalRiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Factories;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;
using BusinessSafe.WebSite.Helpers;
using BusinessSafe.WebSite.ViewModels;

using FluentValidation;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using Telerik.Web.Mvc;
using CreatingRiskAssessmentSummaryViewModel = BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels.CreatingRiskAssessmentSummaryViewModel;

namespace BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Controllers
{
    public class RiskAssessmentController : BaseController
    {
        private readonly ISearchPersonalRiskAssessmentsViewModelFactory _searchRiskAssessmentViewModelFactory;
        private readonly IPersonalRiskAssessmentService _personalRiskAssessmentService;

        public RiskAssessmentController(
            ISearchPersonalRiskAssessmentsViewModelFactory searchRiskAssessmentViewModelFactory,
            IPersonalRiskAssessmentService personalRiskAssessmentService)
        {
            _searchRiskAssessmentViewModelFactory = searchRiskAssessmentViewModelFactory;
            _personalRiskAssessmentService = personalRiskAssessmentService;
        }

        [GridAction(EnableCustomBinding = true, GridName = "PersonalRiskAssessmentsGrid")]
        [PermissionFilter(Permissions.ViewPersonalRiskAssessments)]
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
                .WithRiskAssessmentTemplatingMode(model.isGeneralRiskAssessmentTemplating)
                .WithAllowedSiteIds(CurrentUser.GetSitesFilter())
                .WithCurrentUserId(CurrentUser.UserId)
                .WithPageNumber(model.Page)
                .WithPageSize(model.Size)
                .WithOrderBy(model.OrderBy)
                .GetViewModel();

            return View("Index", viewModel);
        }

        [PermissionFilter(Permissions.AddPersonalRiskAssessments)]
        public ActionResult Create()
        {
            var personalRiskAssessmentSummaryViewModel = new CreatingRiskAssessmentSummaryViewModel
            {
                CompanyId = CurrentUser.CompanyId
            };

            return View("Create", personalRiskAssessmentSummaryViewModel);
        }

        [HttpPost]
        [PermissionFilter(Permissions.AddPersonalRiskAssessments, Permissions.EditPersonalRiskAssessments)]
        public ActionResult Create(CreatingRiskAssessmentSummaryViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", viewModel);
            }

            try
            {
                var riskAssessmentId = _personalRiskAssessmentService.CreateRiskAssessment(new CreatePersonalRiskAssessmentRequest
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
        [PermissionFilter(Permissions.AddPersonalRiskAssessments, Permissions.EditPersonalRiskAssessments)]
        public JsonResult CreateFromChecklist(CreatingRiskAssessmentSummaryViewModel viewModel)
        {
            var result = Json(new { });

            if (ModelState.IsValid)
            {
                try
                {
                    var riskAssessmentId =
                        _personalRiskAssessmentService.CreateRiskAssessmentWithChecklist(
                            new CreateRiskAssessmentRequest
                                {
                                    CompanyId = CurrentUser.CompanyId,
                                    Title = viewModel.Title,
                                    Reference = viewModel.Reference,
                                    UserId = CurrentUser.UserId
                                }, viewModel.ChecklistId);
                    result =
                        Json(
                            new
                                {
                                    Success = true,
                                    riskAssessmentId,
                                    companyId = CurrentUser.CompanyId
                                });
                }
                catch (ValidationException validationException)
                {
                    ModelState.AddValidationErrors(validationException);
                    result = ModelStateErrorsAsJson();
                }
            }
            else
            {
                result = ModelStateErrorsAsJson();
            }

            return result;
        }
    }
}