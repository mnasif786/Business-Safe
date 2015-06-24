using System.Web.Mvc;
using BusinessSafe.Application.Contracts.AccidentRecord;
using BusinessSafe.Application.Custom_Exceptions;
using BusinessSafe.WebSite.Areas.Responsibilities.ViewModels;
using BusinessSafe.WebSite.Areas.SqlReports.Helpers;
using BusinessSafe.WebSite.Areas.SqlReports.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Custom_Exceptions;
using BusinessSafe.WebSite.Extensions;
using BusinessSafe.WebSite.Filters;
using System;

namespace BusinessSafe.WebSite.Areas.SqlReports.Controllers
{
    public class AccidentRecordController : BaseController
    {
        private readonly ISqlReportExecutionServiceFacade _sqlReportExecutionServiceFacade;
        private readonly IAccidentRecordService _accidentRecordService;

        public AccidentRecordController(IAccidentRecordService accidentRecordService, ISqlReportExecutionServiceFacade sqlReportExecutionServiceFacade)
        {
            _sqlReportExecutionServiceFacade = sqlReportExecutionServiceFacade;
            _accidentRecordService = accidentRecordService;
        }

        [PermissionFilter(Permissions.ViewAccidentRecords)]
        public FileResult Pdf(long accidentRecordId)
        {
            var companyId = CurrentUser.CompanyId;
            var accidentRecord = _accidentRecordService.GetByIdAndCompanyIdWithSite(accidentRecordId, companyId);

            if (accidentRecord == null)
            {
                throw new AccidentRecordNotFoundException(accidentRecordId,companyId);
            }

            if (accidentRecord.SiteWhereHappened != null)
            {
                if (!CurrentUser.GetSitesFilter().Contains(accidentRecord.SiteWhereHappened.Id))
                {
                    throw new SitePermissionsInvalidForUserException(CurrentUser.UserId, accidentRecordId);
                }
            }

            var document = _sqlReportExecutionServiceFacade.GetReport(SqlReportHelper.ReportType.AccidentRecord, new object[] { accidentRecord.Id, CurrentUser.CompanyId }, SqlReportHelper.ReportFormatType.PDF);
            var filename = accidentRecord.Title.ParseAsFileName();
            return File(document.FileStream, document.MimeType, filename);
        }
    }
}