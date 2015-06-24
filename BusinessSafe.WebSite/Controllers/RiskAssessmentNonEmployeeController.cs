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
    public class RiskAssessmentNonEmployeeController : BaseController
    {
        private readonly IRiskAssessmentLookupService _riskAssessmentLookupService;
        private readonly IRiskAssessmentAttachmentService _riskAssessmentAttachmentService;

        public RiskAssessmentNonEmployeeController(IRiskAssessmentLookupService riskAssessmentLookupService, IRiskAssessmentAttachmentService riskAssessmentAttachmentService)
        {
            _riskAssessmentLookupService = riskAssessmentLookupService;
            _riskAssessmentAttachmentService = riskAssessmentAttachmentService;
        }

        [HttpGet]
        public JsonResult GetNonEmployees(string filter, long companyId, long riskAssessmentId, int pageLimit)
        {
            var nonEmployees = _riskAssessmentLookupService.SearchForNonEmployeesNotAttachedToRiskAssessment(new NonEmployeesNotAttachedToRiskAssessmentSearchRequest
                                                                                                                 {
                                                                                                                    SearchTerm = filter, 
                                                                                                                    CompanyId = companyId, 
                                                                                                                    RiskAssessmentId = riskAssessmentId, 
                                                                                                                    PageLimit = pageLimit
                                                                                                                 });
            var result = nonEmployees.Select(AutoCompleteViewModel.ForNonEmployee).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditGeneralandHazardousSubstancesRiskAssessments)]
        public JsonResult AttachNonEmployeeToRiskAssessment(long nonEmployeeId, long riskAssessmentId, long companyId)
        {
            _riskAssessmentAttachmentService.AttachNonEmployeeToRiskAssessment(new AttachNonEmployeeToRiskAssessmentRequest()
                                                                                   {
                                                                                       NonEmployeeToAttachId = nonEmployeeId,
                                                                                       RiskAssessmentId = riskAssessmentId,
                                                                                       CompanyId = companyId,
                                                                                       UserId = CurrentUser.UserId
                                                                                   });
            return Json(new { Success = true });
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditGeneralandHazardousSubstancesRiskAssessments)]
        public JsonResult DetachNonEmployeeFromRiskAssessment(IEnumerable<string> nonEmployeeIds, long riskAssessmentId, long companyId)
        {
            _riskAssessmentAttachmentService.DetachNonEmployeeFromRiskAssessment(new DetachNonEmployeeFromRiskAssessmentRequest()
                                                                                     {
                                                                                         NonEmployeesToDetachIds = nonEmployeeIds.Select(long.Parse).ToList(),
                                                                                         RiskAssessmentId = riskAssessmentId,
                                                                                         CompanyId = companyId,
                                                                                         UserId = CurrentUser.UserId
                                                                                     });
            return Json(new { Success = true });
        }
    }
}