using System.Web.Mvc;
using BusinessSafe.Application.Contracts.GeneralRiskAssessments;
using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Filters;
using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.GeneralRiskAssessments.Controllers
{
    [GeneralRiskAssessmentCurrentTabActionFilter(GeneralRiskAssessmentTabs.ControlMeasures)]
    [GeneralRiskAssessmentContextFilter]
    public class ControlMeasuresController : BaseController
    {
        private readonly IGeneralRiskAssessmentService _riskAssessmentService;
        private readonly IRiskAssessmentHazardService _riskAssessmentHazardService;

        public ControlMeasuresController(IGeneralRiskAssessmentService riskAssessmentService, IRiskAssessmentHazardService riskAssessmentHazardService)
        {
            _riskAssessmentService = riskAssessmentService;
            _riskAssessmentHazardService = riskAssessmentHazardService;
        }

        [PermissionFilter(Permissions.ViewGeneralandHazardousSubstancesRiskAssessments)]
        public ActionResult View(long companyId, long riskAssessmentId)
        {
            IsReadOnly = true;
            return Index(companyId, riskAssessmentId);
        }

        [PermissionFilter(Permissions.ViewGeneralandHazardousSubstancesRiskAssessments)]
        public ActionResult Index(long companyId, long riskAssessmentId)
        {
            var riskAssessment = _riskAssessmentService.GetRiskAssessmentWithHazards(riskAssessmentId, companyId);
            var viewModel = ControlMeasuresViewModel.CreateFrom(riskAssessment);

            return View("Index", viewModel);
        }



        [HttpPost]
        [PermissionFilter(Permissions.EditGeneralandHazardousSubstancesRiskAssessments)]
        public JsonResult AddControlMeasureToRiskAssessmentHazard(SaveControlMeasureViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return ModelStateErrorsAsJson();
            }

            var Id = _riskAssessmentHazardService.AddControlMeasureToRiskAssessmentHazard(new AddControlMeasureRequest
                                                                                              {
                                                                                                  CompanyId = model.CompanyId,
                                                                                                  RiskAssessmentId = model.RiskAssessmentId,
                                                                                                  RiskAssessmentHazardId = model.RiskAssessmentHazardId,
                                                                                                  ControlMeasure = model.ControlMeasure,
                                                                                                  UserId = CurrentUser.UserId
                                                                                              });

            return Json(new { Success = true, Id });
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditGeneralandHazardousSubstancesRiskAssessments)]
        public JsonResult UpdateControlMeasureForRiskAssessmentHazard(SaveControlMeasureViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return ModelStateErrorsAsJson();
            }

            _riskAssessmentHazardService.UpdateControlMeasureForRiskAssessmentHazard(new UpdateControlMeasureRequest
            {
                CompanyId = CurrentUser.CompanyId,
                RiskAssessmentId = model.RiskAssessmentId,
                RiskAssessmentHazardId = model.RiskAssessmentHazardId,
                ControlMeasureId = model.ControlMeasureId,
                ControlMeasure = model.ControlMeasure,
                UserId = CurrentUser.UserId
            });

            return Json(new { Success = true });
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditGeneralandHazardousSubstancesRiskAssessments)]
        public JsonResult RemoveControlMeasureFromRiskAssessmentHazard(long riskAssessmentId, long riskAssessmentHazardId, long controlMeasureId, long companyId)
        {
            _riskAssessmentHazardService.RemoveControlMeasureFromRiskAssessmentHazard(new RemoveControlMeasureRequest
            {
                CompanyId = companyId,
                RiskAssessmentId = riskAssessmentId,
                RiskAssessmentHazardId = riskAssessmentHazardId,
                ControlMeasureId = controlMeasureId,
                UserId = CurrentUser.UserId
            });

            return Json(new { Success = true });
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditGeneralandHazardousSubstancesRiskAssessments)]
        public JsonResult UpdateRiskAssessmentHazardDescription(UpdateHazardDescriptionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return ModelStateErrorsAsJson();
            }

            var request = new UpdateRiskAssessmentHazardDescriptionRequest
            {
                CompanyId = CurrentUser.CompanyId,
                RiskAssessmentId = model.RiskAssessmentId,
                RiskAssessmentHazardId = model.RiskAssessmentHazardId,
                Description = model.Description,
                UserId = CurrentUser.UserId
            };

            _riskAssessmentHazardService.UpdateRiskAssessmentHazardDescription(request);

            return Json(new { Success = true });
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditGeneralandHazardousSubstancesRiskAssessments)]
        public JsonResult UpdateRiskAssessmentHazardTitle(UpdateHazardTitleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return ModelStateErrorsAsJson();
            }

            var request = new UpdateRiskAssessmentHazardTitleRequest
            {
                CompanyId = CurrentUser.CompanyId,
                RiskAssessmentId = model.RiskAssessmentId,
                RiskAssessmentHazardId = model.RiskAssessmentHazardId,
                Title = model.Title,
                UserId = CurrentUser.UserId
            };

            _riskAssessmentHazardService.UpdateRiskAssessmentHazardTitle(request);

            return Json(new { Success = true });
        }
    }
}