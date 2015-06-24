using System.Web.Mvc;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Filters;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;
using BusinessSafe.Application.Contracts.FireRiskAssessments;

namespace BusinessSafe.WebSite.Areas.FireRiskAssessments.Controllers
{
    [FireRiskAssessmentCurrentTabActionFilter(FireRiskAssessmentTabs.PremisesInformation)]
    [FireRiskAssessmentContextFilter]
    public class PremisesInformationController : BaseController
    {
        private readonly IPremisesInformationViewModelFactory _premisesInformationViewModelFactory;
        private readonly IFireRiskAssessmentService _fireRiskAssessmentService;

        public PremisesInformationController(
            IPremisesInformationViewModelFactory premisesInformationViewModelFactory,
            IFireRiskAssessmentService fireRiskAssessmentService)
        {
            _premisesInformationViewModelFactory = premisesInformationViewModelFactory;
            _fireRiskAssessmentService = fireRiskAssessmentService;
        }

        [PermissionFilter(Permissions.ViewFireRiskAssessments)]
        public ActionResult View(long riskAssessmentId, long companyId)
        {
            IsReadOnly = true;
            return Index(riskAssessmentId, companyId);
        }

        [PermissionFilter(Permissions.ViewFireRiskAssessments)]
        public ActionResult Index(long riskAssessmentId, long companyId)
        {
            var viewModel = _premisesInformationViewModelFactory
                                    .WithRiskAssessmentId(riskAssessmentId)
                                    .WithCompanyId(companyId)
                                    .WithUser(CurrentUser)
                                    .GetViewModel();

            return View("Index", viewModel);
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditFireRiskAssessments)]
        public JsonResult SaveAndNext(PremisesInformationViewModel request)
        {
            UpdateRiskAssessment(request);
            return Json(new { Success = true });
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditFireRiskAssessments)]
        public ActionResult Save(PremisesInformationViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var premisesInformationViewModel = _premisesInformationViewModelFactory
                                                        .WithRiskAssessmentId(viewModel.RiskAssessmentId)
                                                        .WithCompanyId(viewModel.CompanyId)
                                                        .WithUser(CurrentUser)
                                                        .GetViewModel(viewModel);
                return View("Index", premisesInformationViewModel);
            }

            UpdateRiskAssessment(viewModel);

            TempData["Notice"] = "Premises Information Successfully Saved";
            return RedirectToAction("Index", new { riskAssessmentId = viewModel.RiskAssessmentId, companyId = viewModel.CompanyId });
        }

        private void UpdateRiskAssessment(PremisesInformationViewModel viewModel)
        {
            _fireRiskAssessmentService.UpdatePremisesInformation(new UpdateFireRiskAssessmentPremisesInformationRequest
                                                                     {
                                                                         FireRiskAssessmentId =
                                                                             viewModel.RiskAssessmentId,
                                                                         CompanyId = viewModel.CompanyId,
                                                                         PremisesProvidesSleepingAccommodation =
                                                                             viewModel.
                                                                             PremisesProvidesSleepingAccommodation,
                                                                         PremisesProvidesSleepingAccommodationConfirmed
                                                                             =
                                                                             viewModel.
                                                                             PremisesProvidesSleepingAccommodationConfirmed,
                                                                             Location = viewModel.Location,
                                                                             BuildingUse = viewModel.BuildingUse,
                                                                             NumberOfFloors = viewModel.NumberOfFloors,
                                                                             NumberOfPeople = viewModel.NumberOfPeople,
                                                                            CurrentUserId = CurrentUser.UserId,
                                                                            ElectricityEmergencyShutOff = viewModel.ElectricityEmergencyShutOff,
                                                                            GasEmergencyShutOff = viewModel.GasEmergencyShutOff,
                                                                            WaterEmergencyShutOff = viewModel.WaterEmergencyShutOff,
                                                                            OtherEmergencyShutOff = viewModel.OtherEmergencyShutOff

                                                                     });
        }
    }
}
