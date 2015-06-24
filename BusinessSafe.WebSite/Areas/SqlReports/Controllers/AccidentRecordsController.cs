using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.AccidentRecord;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.AccidentReports.ViewModels;
using BusinessSafe.WebSite.Areas.SqlReports.Factories;
using BusinessSafe.WebSite.Areas.SqlReports.Helpers;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;
using Remotion.Linq.Parsing;

namespace BusinessSafe.WebSite.Areas.SqlReports.Controllers
{
    public class AccidentRecordsController : BaseController
    {
        private readonly ISqlReportExecutionServiceFacade _sqlReportExecutionServiceFacade;
        private readonly AccidentRecordsFactory _factory;

        public AccidentRecordsController(ISqlReportExecutionServiceFacade sqlReportExecutionServiceFacade, IAccidentRecordService accidentRecordsService)
        {
            _sqlReportExecutionServiceFacade = sqlReportExecutionServiceFacade;
            _factory = new AccidentRecordsFactory(accidentRecordsService);
        }

        [PermissionFilter(Permissions.ViewAccidentRecords)]
        public FileResult Pdf(AccidentRecordsIndexViewModel viewModel)
        {
           
            long[] accidentRecords = _factory
                                        .WithCompanyId(CurrentUser.CompanyId)
                                        .WithShowDeleted(viewModel.IsShowDeleted)
                                        .WithSiteId(viewModel.SiteId)
                                        .WithStartDate(string.IsNullOrEmpty(viewModel.CreatedFrom)
                                                           ? (DateTime?) null
                                                           : DateTime.Parse(viewModel.CreatedFrom))
                                        .WithEndDate(string.IsNullOrEmpty(viewModel.CreatedTo)
                                                         ? (DateTime?) null
                                                         : DateTime.Parse(viewModel.CreatedTo))
                                        .WithAllowedSiteIds(CurrentUser.GetSitesFilter().ToList())
                                        .GetAccidentRecords();
            
            return CreateFile( accidentRecords );
        }

        private FileResult CreateFile(long[] accidentRecordIds)
        {
            var commaSeperatedValues = string.Join(",", accidentRecordIds);
            
            var document = _sqlReportExecutionServiceFacade.GetReport(SqlReportHelper.ReportType.AccidentRecords,
                                                                    new object[]
                                                                          {
                                                                             commaSeperatedValues
                                                                          },
                                                                    SqlReportHelper.ReportFormatType.PDF);

            return File(document.FileStream, document.MimeType, "AccidentRecords.pdf");
        }
    }
}