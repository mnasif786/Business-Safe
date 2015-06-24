using System.Collections.Generic;

using BusinessSafe.WebSite.Areas.SqlReports.Helpers;
using BusinessSafe.WebSite.SqlReportExecutionService;

namespace BusinessSafe.WebSite.Areas.SqlReports.ViewModels
{
    public class SqlReportDefinition
    {
        public SqlReportDefinition()
        {
            Parameters = new List<ParameterValue>();
        }

        public string Filename { get; set; }
        public SqlReportHelper.ReportType Report { get; set; }
        public string ReportPath { get; set; }
        public List<ParameterValue> Parameters { get; set; }
    }
}