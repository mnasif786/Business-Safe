using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using BusinessSafe.WebSite.Areas.SqlReports.Factories;
using BusinessSafe.WebSite.Areas.SqlReports.ViewModels;
using BusinessSafe.WebSite.Custom_Exceptions;
using BusinessSafe.WebSite.SqlReportExecutionService;

namespace BusinessSafe.WebSite.Areas.SqlReports.Helpers
{
    public class SqlReportExecutionServiceFacade : ISqlReportExecutionServiceFacade
    {
        private ReportExecutionServiceSoapClient _reportingService;

        private ReportExecutionServiceSoapClient ReportingService
        {
            get
            {
                if (_reportingService != null)
                    return _reportingService;

                _reportingService = new ReportExecutionServiceSoapClient();
                _reportingService.ClientCredentials.Windows.ClientCredential = SqlReportHelper.GetNetworkCredential();
                _reportingService.ClientCredentials.Windows.AllowNtlm = true;
                _reportingService.ClientCredentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Impersonation;

                return _reportingService;
            }
        }

        public DocumentViewModel GetReport(SqlReportHelper.ReportType report, object[] parameterValues, SqlReportHelper.ReportFormatType format)
        {
            var reportDefinition = GetSqlReportDefinition(report);

            SetReportParameters(reportDefinition.Parameters.ToArray(), parameterValues);

            ServerInfoHeader serverInfoHeader;
            ExecutionInfo executionInfo;

            var sqlReportsSuffix = ConfigurationManager.AppSettings["SQLReportsSuffix"];
            var reportDefinitionReportPath = reportDefinition.ReportPath + sqlReportsSuffix;
            reportDefinitionReportPath.Trim();

            var executionHeader = ReportingService.LoadReport(null, reportDefinitionReportPath, null,
                out serverInfoHeader,
                out executionInfo);

            // Attach Report Parameters
            ReportingService.SetExecutionParameters(executionHeader, null, reportDefinition.Parameters.ToArray(),
                null,
                out executionInfo);

            // Render
            byte[] result;
            string extension;
            string mimeType;
            string encoding;
            Warning[] warnings = null;
            string[] streamIds;

            ReportingService.Render(executionHeader, null, SqlReportHelper.GetExportFormatString(format), null,
                out result,
                out extension, out mimeType, out encoding, out warnings, out streamIds);

            var ms = new MemoryStream();

            ms.Write(result, 0, result.Length);
            ms.Position = 0;
            var fileName = reportDefinition.Filename.Replace("/", "_");

            return new DocumentViewModel()
            {
                MimeType = mimeType,
                FileName =
                    !string.IsNullOrEmpty(fileName)
                        ? fileName + "." + extension
                        : Guid.NewGuid() + "." + extension,
                FileStream = ms
            };


        }

        private static SqlReportDefinition GetSqlReportDefinition(SqlReportHelper.ReportType report)
        {
            var reportDefinitions = SqlReportDefinitionFactory.GetSqlReportDefinitions();
            var reportDefinition = reportDefinitions.SingleOrDefault(a => a.Report == report);

            if (reportDefinition == null)
            {
                throw new SqlReportDefinitonNotFoundException(report);
            }
            return reportDefinition;
        }

        private void SetReportParameters(IEnumerable<ParameterValue> reportParameters, object[] parameterValues)
        {
            var index = 0;

            foreach (var param in reportParameters)
            {
                if (parameterValues[index] != null)
                {

                    var parameterValueType = parameterValues[index].GetType();

                    if (parameterValueType == typeof (DateTime))
                        param.Value = ((DateTime) parameterValues[index]).ToLongDateString();
                    else if (parameterValueType == typeof (Int32))
                        param.Value = ((Int32) parameterValues[index]).ToString();
                    else
                        param.Value = parameterValues[index].ToString();
                }

                index += 1;
            }
        }
    }
}