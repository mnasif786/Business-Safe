using System;
using System.Configuration;
using BusinessSafe.WebSite.Areas.SqlReports.Helpers;
using BusinessSafe.WebSite.Helpers;
using StructureMap;

namespace BusinessSafe.WebSite.WebsiteMoqs
{
    public class SqlReportsFactory
    {
        public ISqlReportExecutionServiceFacade Create()
        {
            var pluginType = ConfigurationManager.AppSettings["SqlReportsService"];

            if (!string.IsNullOrEmpty(pluginType))
            {
                var sqlReportService = Type.GetType(pluginType);
                Log4NetHelper.Logger.Debug(string.Format("SqlReportExecutionServiceFacade initialised {0}", sqlReportService.Name));
                return (ISqlReportExecutionServiceFacade)ObjectFactory.GetInstance(sqlReportService);
            }

            Log4NetHelper.Logger.Debug("SqlReportExecutionServiceFacade initialised SqlReportExecutionServiceFacade");
            return new SqlReportExecutionServiceFacade(); 
        }
    }
}
