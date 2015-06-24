using System.Web.Mvc;
using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.WebSite.Areas.Filters;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Factories;
using BusinessSafe.WebSite.Filters;
using BusinessSafe.WebSite.Models;

namespace BusinessSafe.WebSite.Areas.FireRiskAssessments.Controllers
{
    [FireRiskAssessmentCurrentTabActionFilter(FireRiskAssessmentTabs.Review)]
    [FireRiskAssessmentContextFilter]
    public class ReviewController : BaseController
    {
        private readonly IRiskAssessmentService _riskAssessmentService;
        private readonly ICompleteReviewViewModelFactory _completeReviewViewModelFactory;
        private readonly IFireRiskAssessmentReviewsViewModelFactory _fireRiskAssessmentReviewsViewModelFactory;

        public ReviewController(
            IFireRiskAssessmentReviewsViewModelFactory fireRiskAssessmentReviewsViewModelFactory, 
            IRiskAssessmentService riskAssessmentService, 
            ICompleteReviewViewModelFactory completeReviewViewModelFactory)
        {
            _fireRiskAssessmentReviewsViewModelFactory = fireRiskAssessmentReviewsViewModelFactory;
            _riskAssessmentService = riskAssessmentService;
            _completeReviewViewModelFactory = completeReviewViewModelFactory;
        }

        [PermissionFilter(Permissions.ViewFireRiskAssessments)]
        public ActionResult View(long companyId, long riskAssessmentId)
        {
            IsReadOnly = true;
            return Index(companyId, riskAssessmentId);
        }

        [PermissionFilter(Permissions.ViewFireRiskAssessments)]
        public ActionResult Index(long companyId, long riskAssessmentId)
        {
            var model = _fireRiskAssessmentReviewsViewModelFactory
                                        .WithCompanyId(companyId)
                                        .WithRiskAssessmentId(riskAssessmentId)
                                        .WithUser(CurrentUser)
                                        .GetViewModel();

            return View("Index", model);
        }

        [PermissionFilter(Permissions.EditFireRiskAssessments)]
        public PartialViewResult Complete(long companyId, long riskAssessmentId, long riskAssessmentReviewId)
        {
            bool hasUncompletedTasks = _riskAssessmentService.HasUncompletedTasks(companyId, riskAssessmentId);

            var viewModel = _completeReviewViewModelFactory
                .WithCompanyId(companyId)
                .WithReviewId(riskAssessmentReviewId)
                .WithHasUncompletedTasks(hasUncompletedTasks)
                .WithRiskAssessmentType(RiskAssessmentType.FRA)
                .GetViewModel();

            return PartialView("_CompleteRiskAssessmentReview", viewModel);
        }
    }
}