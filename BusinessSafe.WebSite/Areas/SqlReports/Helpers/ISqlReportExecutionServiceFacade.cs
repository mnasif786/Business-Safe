using BusinessSafe.WebSite.Areas.SqlReports.ViewModels;

namespace BusinessSafe.WebSite.Areas.SqlReports.Helpers
{
    public interface ISqlReportExecutionServiceFacade
    {
        DocumentViewModel GetReport(SqlReportHelper.ReportType report,
                                    object[] parameterValues, SqlReportHelper.ReportFormatType format);
    }
}