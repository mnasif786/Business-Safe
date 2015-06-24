using System;
using System.Web.Mvc;

using BusinessSafe.Application.Contracts.Checklists;
using BusinessSafe.Application.Contracts.PersonalRiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;
using BusinessSafe.WebSite.Helpers;
using FluentValidation;

namespace BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Controllers
{
    public class ChecklistManagerCommandsController : BaseController
    {
        private readonly IEmployeeChecklistService _employeeChecklistService;
        private readonly IPersonalRiskAssessmentService _personalRiskAssessmentService;

        public ChecklistManagerCommandsController(IEmployeeChecklistService employeeChecklistService, IPersonalRiskAssessmentService personalRiskAssessmentService)
        {
            _employeeChecklistService = employeeChecklistService;
            _personalRiskAssessmentService = personalRiskAssessmentService;
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditPersonalRiskAssessments)]
        public JsonResult ToggleFurtherActionRequired(Guid id, bool isRequired)
        {
            _employeeChecklistService.ToggleFurtherActionRequired(id, isRequired, CurrentUser.UserId);
            return Json(new { Success = true });
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditPersonalRiskAssessments)]
        public JsonResult UpdateEmployeeChecklistFurtherRequired(Guid employeeChecklistId, bool isFurtherActionRequired)
        {
            var result = Json(new {});

            try
            {
                _employeeChecklistService.ToggleFurtherActionRequired(employeeChecklistId,isFurtherActionRequired,CurrentUser.UserId);
                if (isFurtherActionRequired)
                {
                    var riskAssessmentId = _personalRiskAssessmentService.CreateRiskAssessmentFromChecklist(employeeChecklistId, CurrentUser.UserId);
                    result = Json(new
                                      {
                                          Success = true, riskAssessmentId, companyId = CurrentUser.CompanyId
                                      });
                }
                else
                {
                    result = Json(new
                                      {
                                          Success = true, riskAssessmentId = 0, companyId = CurrentUser.CompanyId
                                      });
                }


            }
            catch (ValidationException validationException)
            {
                ModelState.AddValidationErrors(validationException);
                result = ModelStateErrorsAsJson();
            }


            return result;
        }
    }
}
