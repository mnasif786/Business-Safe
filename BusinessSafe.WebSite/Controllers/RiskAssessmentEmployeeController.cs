using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.GeneralRiskAssessments;
using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Filters;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Controllers
{
    public class RiskAssessmentEmployeeController : BaseController
    {
        private readonly IRiskAssessmentLookupService _riskAssessmentLookupService;
        private readonly IRiskAssessmentAttachmentService _riskAssessmentAttachmentService;

        public RiskAssessmentEmployeeController(IRiskAssessmentLookupService riskAssessmentLookupService, IRiskAssessmentAttachmentService riskAssessmentAttachmentService)
        {
            _riskAssessmentLookupService = riskAssessmentLookupService;
            _riskAssessmentAttachmentService = riskAssessmentAttachmentService;
        }

        [HttpGet]
        public JsonResult GetEmployees(string filter, long companyId, long riskAssessmentId, int pageLimit)
        {
            var employees =
                _riskAssessmentLookupService.SearchForEmployeesNotAttachedToRiskAssessment(new EmployeesNotAttachedToRiskAssessmentSearchRequest
                                                                                               ()
                                                                                               {
                                                                                                   CompanyId = companyId,
                                                                                                   SearchTerm = filter,
                                                                                                   PageLimit = pageLimit,
                                                                                                   RiskAssessmentId = riskAssessmentId
                                                                                               });
            var result = employees.Select(AutoCompleteViewModel.ForEmployee).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditGeneralandHazardousSubstancesRiskAssessments)]
        public JsonResult AttachEmployeeToRiskAssessment(Guid employeeId, long riskAssessmentId, long companyId)
        {
            _riskAssessmentAttachmentService.AttachEmployeeToRiskAssessment(new AttachEmployeeRequest
            {
                EmployeeId = employeeId,
                RiskAssessmentId = riskAssessmentId,
                CompanyId = companyId,
                UserId = CurrentUser.UserId
            });
            return Json(new { Success = true });
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditGeneralandHazardousSubstancesRiskAssessments)]
        public JsonResult DetachEmployeeFromRiskAssessment(List<Guid> employeeIds, long riskAssessmentId, long companyId)
        {
            _riskAssessmentAttachmentService.DetachEmployeeFromRiskAssessment(new DetachEmployeeRequest
            {
                CompanyId = companyId,
                EmployeeIds = employeeIds,
                RiskAssessmentId = riskAssessmentId,
                UserId = CurrentUser.UserId
            });
            return Json(new { Success = true });
        }
    }
}