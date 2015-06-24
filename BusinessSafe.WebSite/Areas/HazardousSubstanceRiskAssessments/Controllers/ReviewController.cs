using System.Web.Mvc;
using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.WebSite.Areas.Filters;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.ViewModels;
using BusinessSafe.WebSite.Factories;
using BusinessSafe.WebSite.Filters;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Factories;
using BusinessSafe.WebSite.Models;

namespace BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Controllers
{
    [HazardousSubstanceRiskAssessmentCurrentTabActionFilter(HazardousSubstancesTabs.Review)]
    [HazardousSubstanceRiskAssessmentContextFilter]
    public class ReviewController : BaseController
    {
        private readonly IRiskAssessmentService _riskAssessmentService;
        private readonly ICompleteReviewViewModelFactory _completeReviewViewModelFactory;
        private readonly IHazardousSubstanceRiskAssessmentReviewsViewModelFactory _hazardousSubstanceRiskAssessmentReviewsViewModelFactory;

        public ReviewController(
            IHazardousSubstanceRiskAssessmentReviewsViewModelFactory hazardousSubstanceRiskAssessmentReviewsViewModelFactory, 
            IRiskAssessmentService riskAssessmentService,
            ICompleteReviewViewModelFactory completeReviewViewModelFactory)
        {
            _hazardousSubstanceRiskAssessmentReviewsViewModelFactory = hazardousSubstanceRiskAssessmentReviewsViewModelFactory;
            _riskAssessmentService = riskAssessmentService;
            _completeReviewViewModelFactory = completeReviewViewModelFactory;
        }

        [PermissionFilter(Permissions.ViewGeneralandHazardousSubstancesRiskAssessments)]
        public ActionResult View(long riskAssessmentId, long companyId)
        {
            IsReadOnly = true;

            return Index(companyId, riskAssessmentId);
        }

        [PermissionFilter(Permissions.ViewGeneralandHazardousSubstancesRiskAssessments)]
        public ActionResult Index(long companyId, long riskAssessmentId)
        {
            var model = _hazardousSubstanceRiskAssessmentReviewsViewModelFactory
                .WithCompanyId(companyId)
                .WithRiskAssessmentId(riskAssessmentId)
                .WithUser(CurrentUser)
                .GetViewModel();

            return View("Index", model);
        }

        [PermissionFilter(Permissions.EditGeneralandHazardousSubstancesRiskAssessments)]
        public PartialViewResult Complete(long companyId, long riskAssessmentId, long riskAssessmentReviewId)
        {
            bool hasUncompletedTasks = _riskAssessmentService.HasUncompletedTasks(companyId, riskAssessmentId);

            var viewModel = _completeReviewViewModelFactory
                .WithCompanyId(companyId)
                .WithReviewId(riskAssessmentReviewId)
                .WithHasUncompletedTasks(hasUncompletedTasks)
                .WithRiskAssessmentType(RiskAssessmentType.HSRA)
                .GetViewModel();

            return PartialView("_CompleteRiskAssessmentReview", viewModel);
        }
    }
}