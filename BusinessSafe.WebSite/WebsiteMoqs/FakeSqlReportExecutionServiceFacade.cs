using System.IO;
using BusinessSafe.WebSite.Areas.SqlReports.Helpers;
using BusinessSafe.WebSite.Areas.SqlReports.ViewModels;
using BusinessSafe.WebSite.Helpers;

namespace BusinessSafe.WebSite.WebsiteMoqs
{
    public class FakeSqlReportExecutionServiceFacade : ISqlReportExecutionServiceFacade
    {
        public DocumentViewModel GetReport(SqlReportHelper.ReportType report, object[] parameterValues, SqlReportHelper.ReportFormatType format)
        {
            const string fileName = "lorem_ipsum";
            const string fileExtension = ".txt";

            var documentViewModel = new DocumentViewModel
                                        {
                                            FileName = fileName + fileExtension,
                                            FileStream = new MemoryStream(10),
                                            MimeType = ContentTypeHelper.GetContentTypeFromExtension(fileExtension)
                                        };

            return documentViewModel;
        }
    }
}