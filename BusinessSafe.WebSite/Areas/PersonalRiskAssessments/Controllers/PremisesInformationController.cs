using System.Web.Mvc;
using BusinessSafe.Application.Contracts.MultiHazardRiskAssessment;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Filters;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;

namespace BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Controllers
{
    [PersonalRiskAssessmentCurrentTabActionFilter(PersonalRiskAssessmentTabs.PremisesInformation)]
    [PersonalRiskAssessmentContextFilter]
    public class PremisesInformationController : BaseController
    {
        private readonly IPremisesInformationViewModelFactory _premisesInformationViewModelFactory;
        private readonly IMultiHazardRiskAssessmentService _riskAssessmentService;

        public PremisesInformationController(IPremisesInformationViewModelFactory premisesInformationViewModelFactory, IMultiHazardRiskAssessmentService riskAssessmentService)
        {
            _premisesInformationViewModelFactory = premisesInformationViewModelFactory;
            _riskAssessmentService = riskAssessmentService;
        }

        [PermissionFilter(Permissions.ViewPersonalRiskAssessments)]
        public ActionResult View(long riskAssessmentId, long companyId)
        {
            IsReadOnly = true;
            return Index(riskAssessmentId, companyId);
        }

        [PermissionFilter(Permissions.ViewPersonalRiskAssessments)]
        public ActionResult Index(long riskAssessmentId, long companyId)
        {
            var viewModel = _premisesInformationViewModelFactory
                .WithRiskAssessmentId(riskAssessmentId)
                .WithCompanyId(companyId)
                .WithCurrentUserId(CurrentUser.UserId)
                .GetViewModel();

            return View("Index", viewModel);
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditPersonalRiskAssessments)]
        public JsonResult SaveAndNext(PremisesInformationViewModel request)
        {
            UpdateRiskAssessment(request);

            return Json(new { Success = true });
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditPersonalRiskAssessments)]
        public ActionResult Save(PremisesInformationViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var premisesInformationViewModel = _premisesInformationViewModelFactory
                                                        .WithCompanyId(viewModel.CompanyId)
                                                        .WithCurrentUserId(CurrentUser.UserId)
                                                        .GetViewModel(viewModel);
                return View("Index", premisesInformationViewModel);
            }
            
            UpdateRiskAssessment(viewModel);
           
            TempData["Notice"] = "Premises Information Successfully Saved";
            return RedirectToAction("Index", new { riskAssessmentId = viewModel.RiskAssessmentId, companyId = viewModel.CompanyId });
        }

        private void UpdateRiskAssessment(PremisesInformationViewModel request)
        {
            _riskAssessmentService.UpdateRiskAssessmentPremisesInformation(new SaveRiskAssessmentPremisesInformationRequest
            {
                CompanyId = request.CompanyId,
                Id = request.RiskAssessmentId,
                LocationAreaDepartment = request.LocationAreaDepartment,
                TaskProcessDescription = request.TaskProcessDescription,
                UserId = CurrentUser.UserId
            });
        }
    }
}