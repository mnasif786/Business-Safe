using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessSafe.WebSite.Areas.SqlReports.Helpers;

namespace BusinessSafe.WebSite.Custom_Exceptions
{
    public class CompanyNotValidForUserException : ArgumentNullException
    {
        public CompanyNotValidForUserException(long companyId, Guid userId) : base(string.Format("Company {0} is not valid for User Id {1}", companyId, userId.ToString())) { }
    }

    public class SqlReportDefinitonNotFoundException : ArgumentException
    {
        public SqlReportDefinitonNotFoundException(SqlReportHelper.ReportType report) : base(string.Format("Report definition not found for {0}", report.ToString())) { }
    }
}