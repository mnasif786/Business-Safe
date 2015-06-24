using System.Collections.Generic;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.HazardousSubstanceRiskAssessments;
using BusinessSafe.Application.Request.HazardousSubstanceRiskAssessments;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.Filters;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;

namespace BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Controllers
{
    [HazardousSubstanceRiskAssessmentCurrentTabActionFilter(HazardousSubstancesTabs.Assessment)]
    public class ControlSystemController : BaseController
    {
        private readonly IControlSystemService _controlSystemService;
        private readonly IHazardousSubstanceRiskAssessmentService _hazardousSubstanceRiskAssessmentService;
        private readonly IDictionary<string, string> _controlSystemsDictionary = new Dictionary<string, string>
                                                                       {
                                                                           {"General", "HAZARDOUS SUBSTANCES CONTROL SYSTEM - General Ventilation.docx"},
                                                                           {"Engineering Controls", "HAZARDOUS SUBSTANCES CONTROL SYSTEM - Engineering Control.docx"},
                                                                           {"Containment", "HAZARDOUS SUBSTANCES CONTROL SYSTEM - Containment.docx"},
                                                                           {"Special", "HAZARDOUS SUBSTANCES CONTROL SYSTEM - Special Measures.docx"}
                                                                       };

        public ControlSystemController(IControlSystemService controlSystemService, IHazardousSubstanceRiskAssessmentService hazardousSubstanceRiskAssessmentService)
        {
            _controlSystemService = controlSystemService;
            _hazardousSubstanceRiskAssessmentService = hazardousSubstanceRiskAssessmentService;
        }

        [PermissionFilter(Permissions.ViewGeneralandHazardousSubstancesRiskAssessments)]
        public JsonResult GetControlSystem(long riskAssessmentId, string hazardousSubstanceGroupCode, MatterState? matterState, Quantity? quantity, DustinessOrVolatility? dustinessOrVolatility)
        {
            var controlSystemDto = _controlSystemService.Calculate(hazardousSubstanceGroupCode, matterState, quantity, dustinessOrVolatility);

            _hazardousSubstanceRiskAssessmentService.SaveLastRecommendedControlSystem(new SaveLastRecommendedControlSystemRequest
                                                                                          {
                                                                                              Id = riskAssessmentId,
                                                                                              CompanyId = CurrentUser.CompanyId,
                                                                                              ControlSystemId = controlSystemDto.Id,
                                                                                              UserId = CurrentUser.UserId
                                                                                          });

            var url = Url.Action("LoadControlSystem", new { controlSystem = controlSystemDto.Description });


            return Json(new
            {
                ControlSystemId = controlSystemDto.Id,
                ControlSystem = controlSystemDto.Description,
                Url = url
            }, JsonRequestBehavior.AllowGet);
        }

        public FileStreamResult LoadControlSystem(string controlSystem)
        {
            var fileName = _controlSystemsDictionary[controlSystem];
            var stream = GetType()
                            .Assembly
                            .GetManifestResourceStream("BusinessSafe.WebSite.Documents." + fileName);

            return File(stream, "application/msword");
        }
    }
}