using System.Web.Mvc;
using BusinessSafe.Application.Contracts.GeneralRiskAssessments;
using BusinessSafe.Application.Contracts.MultiHazardRiskAssessment;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Filters;
using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;
using BusinessSafe.WebSite.Helpers;
using FluentValidation;

namespace BusinessSafe.WebSite.Areas.GeneralRiskAssessments.Controllers
{
    [GeneralRiskAssessmentCurrentTabActionFilter(GeneralRiskAssessmentTabs.PremisesInformation)]
    [GeneralRiskAssessmentContextFilter]
    public class PremisesInformationController : BaseController
    {
        private readonly IGeneralRiskAssessmentService _riskAssessmentService;
        private readonly IMultiHazardRiskAssessmentService _multiHazardRiskAssessmentService;

        public PremisesInformationController(
            IGeneralRiskAssessmentService riskAssessmentService, 
            IMultiHazardRiskAssessmentService multiHazardRiskAssessmentService)
        {
            _riskAssessmentService = riskAssessmentService;
            _multiHazardRiskAssessmentService = multiHazardRiskAssessmentService;
        }

        [PermissionFilter(Permissions.ViewGeneralandHazardousSubstancesRiskAssessments)]
        public ActionResult View(long riskAssessmentId, long companyId)
        {
            IsReadOnly = true;
            return Index(riskAssessmentId, companyId);
        }

        [PermissionFilter(Permissions.ViewGeneralandHazardousSubstancesRiskAssessments)]
        public ActionResult Index(long riskAssessmentId, long companyId)
        {
            var riskAssessment = _riskAssessmentService.GetRiskAssessment(riskAssessmentId, companyId);
            var model = PremisesInformationViewModel.CreateFrom(riskAssessment);
            return View("Index", model);
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditGeneralandHazardousSubstancesRiskAssessments)]
        public JsonResult SaveAndNext(PremisesInformationViewModel request)
        {
            UpdateRiskAssessment(request);

            return Json(new { Success = true });
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditGeneralandHazardousSubstancesRiskAssessments)]
        public ActionResult Save(PremisesInformationViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var premisesInformationViewModel = GetPremisesInformationViewModel(viewModel);
                return View("Index", premisesInformationViewModel);
            }

            try
            {
                UpdateRiskAssessment(viewModel);
            }
            catch (ValidationException validationException)
            {
                ModelState.AddValidationErrors(validationException);
                viewModel = GetPremisesInformationViewModel(viewModel);
                return View("Index", viewModel);
            }

            TempData["Notice"] = "Premises Information Successfully Saved";
            return RedirectToAction("Index", new { riskAssessmentId = viewModel.RiskAssessmentId, companyId = viewModel.CompanyId });
        }

        private void UpdateRiskAssessment(PremisesInformationViewModel request)
        {
            _multiHazardRiskAssessmentService.UpdateRiskAssessmentPremisesInformation(new SaveRiskAssessmentPremisesInformationRequest
                                                            {
                                                                CompanyId = request.CompanyId,
                                                                Id = request.RiskAssessmentId,
                                                                LocationAreaDepartment = request.LocationAreaDepartment,
                                                                TaskProcessDescription = request.TaskProcessDescription,
                                                                UserId = CurrentUser.UserId
                                                            });
        }

        private PremisesInformationViewModel GetPremisesInformationViewModel(PremisesInformationViewModel viewModel)
        {
            var riskAssessment = _riskAssessmentService.GetRiskAssessment(viewModel.RiskAssessmentId, viewModel.CompanyId);
            return PremisesInformationViewModel.ForValidationErrors(viewModel, riskAssessment.NonEmployees);
        }

   }
}
