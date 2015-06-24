using System.Web.Mvc;
using BusinessSafe.WebSite.Areas.Responsibilities.ViewModels;
using BusinessSafe.WebSite.Areas.SqlReports.Helpers;
using BusinessSafe.WebSite.Areas.SqlReports.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;
using System;

namespace BusinessSafe.WebSite.Areas.SqlReports.Controllers
{
    public class ResponsibilitiesController : BaseController
    {
        private readonly ISqlReportExecutionServiceFacade _sqlReportExecutionServiceFacade;

        public ResponsibilitiesController(ISqlReportExecutionServiceFacade sqlReportExecutionServiceFacade)
        {
            _sqlReportExecutionServiceFacade = sqlReportExecutionServiceFacade;
        }

        [PermissionFilter(Permissions.ViewAccidentRecords)]
        public FileResult Index(string filename, long? responsibilityId, ResponsibilitiesIndexViewModel viewModel)
        {
            //Ignoring filename, it's just to get round an IE issue:
            //http://stackoverflow.com/questions/9609837/file-couldnt-be-downloaded-in-internet-explorer-with-asp-net-mvc

            string createdFrom = null;
            string createdTo = null;

            if(viewModel.CreatedFrom != null)
            {
                createdFrom = DateTime.Parse(viewModel.CreatedFrom).ToString("yyy-MM-dd hh:mm:ss");
            }

            if (viewModel.CreatedTo != null)
            {
                createdTo = DateTime.Parse(viewModel.CreatedTo).ToString("yyy-MM-dd hh:mm:ss");
            }

            DocumentViewModel document = null;

            if (responsibilityId.HasValue)
            {
                document = _sqlReportExecutionServiceFacade.GetReport(SqlReportHelper.ReportType.Responsibilities,
                                                                      new object[]
                                                                          {
                                                                              responsibilityId
                                                                          },
                                                                      SqlReportHelper.ReportFormatType.PDF);
            }
            else
            {
                document = _sqlReportExecutionServiceFacade.GetReport(SqlReportHelper.ReportType.Responsibilities_Index,
                                                                      new object[]
                                                                          {
                                                                              CurrentUser.CompanyId,
                                                                              viewModel.Title,
                                                                              viewModel.CategoryId,
                                                                              viewModel.SiteId,
                                                                              createdFrom,
                                                                              createdTo
                                                                          },
                                                                      SqlReportHelper.ReportFormatType.PDF);
            }

            return File(document.FileStream, document.MimeType, "ResponsibilitiesSearchResult.pdf");
        }
    }
}
