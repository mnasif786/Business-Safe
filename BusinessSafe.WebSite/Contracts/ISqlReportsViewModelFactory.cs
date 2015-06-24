using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessSafe.WebSite.Areas.SqlReports.Helpers;
using BusinessSafe.WebSite.Areas.SqlReports.ViewModels;

namespace BusinessSafe.WebSite.Contracts
{   
    public interface ISqlReportsViewModelFactory : IViewModelFactory<SqlReportViewModel>
    {
        ISqlReportsViewModelFactory WithSiteId(long? siteId);
        ISqlReportsViewModelFactory WithSiteGroupId(long? siteGroupId);
        ISqlReportsViewModelFactory WithCompanyId(long companyId);
        ISqlReportsViewModelFactory WithReportTypes();
    }
}