using System.Web.Mvc;
using BusinessSafe.Application.Contracts.FireRiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Filters;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;

namespace BusinessSafe.WebSite.Areas.FireRiskAssessments.Controllers
{
    [FireRiskAssessmentCurrentTabActionFilter(FireRiskAssessmentTabs.SignificantFindings)]
    [FireRiskAssessmentContextFilter]
    public class SignificantFindingsController : BaseController
    {
        private readonly ISignificantFindingViewModelFactory _viewModelFactory;
        private readonly ISignificantFindingService _significantFindingService;

        public SignificantFindingsController(
            ISignificantFindingViewModelFactory viewModelFactory,
            ISignificantFindingService significantFindingService
            )
        {
            _viewModelFactory = viewModelFactory;
            _significantFindingService = significantFindingService;
        }

        [PermissionFilter(Permissions.EditFireRiskAssessments)]
        public ActionResult Index(long riskAssessmentId, long companyId)
        {
            var model = _viewModelFactory
                .WithRiskAssessmentId(riskAssessmentId)
                .WithCompanyId(companyId)
                .GetViewModel();
            return View("Index", model);
        }

     	[PermissionFilter(Permissions.ViewFireRiskAssessments)]
        public ActionResult View(long companyId, long riskAssessmentId)
        {
            IsReadOnly = true;
            return Index(riskAssessmentId, companyId);
        }
        [PermissionFilter(Permissions.DeleteFireRiskAssessments)]
        [HttpPost]
        public JsonResult MarkSignificantFindingAsDeleted(MarkSignificantFindingAsDeletedViewModel viewModel)
        {
            _significantFindingService.MarkSignificantFindingAsDeleted(new MarkSignificantFindingAsDeletedRequest
                                                         {
                                                             CompanyId = viewModel.CompanyId,
                                                             FireChecklistId = viewModel.FireChecklistId,
                                                             FireQuestionId = viewModel.FireQuestionId,
                                                             UserId = CurrentUser.UserId
                                                         }
                );

            return Json(new { Success = true });
        }
    }
}
