using System;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.GeneralRiskAssessments;
using BusinessSafe.Application.Contracts.PersonalRiskAssessments;
using BusinessSafe.WebSite.Areas.SqlReports.Helpers;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Custom_Exceptions;
using BusinessSafe.WebSite.Extensions;
using BusinessSafe.WebSite.Filters;

namespace BusinessSafe.WebSite.Areas.SqlReports.Controllers
{
    public class PersonalRiskAssessmentsController : BaseController
    {
        private readonly IPersonalRiskAssessmentService _riskAssessmentService;
        private readonly ISqlReportExecutionServiceFacade _sqlReportExecutionService;

        public PersonalRiskAssessmentsController(IPersonalRiskAssessmentService riskAssessmentService, ISqlReportExecutionServiceFacade sqlReportExecutionService)
        {
            _riskAssessmentService = riskAssessmentService;
            _sqlReportExecutionService = sqlReportExecutionService;
        }

        [PermissionFilter(Permissions.ViewGeneralandHazardousSubstancesRiskAssessments)]
        public FileResult Index(long riskAssessmentId)
        {
            var riskAssessment = _riskAssessmentService.GetRiskAssessment(riskAssessmentId, CurrentUser.CompanyId, CurrentUser.UserId);
            
            if (riskAssessment.RiskAssessmentSite != null )
            {
                if (!CurrentUser.GetSitesFilter().Contains(riskAssessment.RiskAssessmentSite.Id))
                    throw new SitePermissionsInvalidForUserException(CurrentUser.UserId, riskAssessmentId);
            }else
            {
                if(riskAssessment.CreatedBy.Id != CurrentUser.UserId)
                    throw new SitePermissionsInvalidForUserException(CurrentUser.UserId, riskAssessmentId);
            }

            var document = _sqlReportExecutionService.GetReport(SqlReportHelper.ReportType.PRA, new object[] { riskAssessmentId }, SqlReportHelper.ReportFormatType.PDF);
            var filename = riskAssessment.Title.ParseAsFileName();

            return File(document.FileStream, document.MimeType, filename);
        }
    }
}