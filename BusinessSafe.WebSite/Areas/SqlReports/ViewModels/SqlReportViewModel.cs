using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.SqlReports.Helpers;
using BusinessSafe.WebSite.Extensions;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.SqlReports.ViewModels
{
      
    public class SqlReportViewModel
    {       
        public long CompanyId { get; set; }

        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }

        public long AccidentRecordId { get; set; }      

        public string SubstanceId { get; set; }

        public long? SiteId { get; set; }
        public long? SiteGroupId { get; set; }
        public long? ReportId { get; set; }

        public IEnumerable<AutoCompleteViewModel> Sites { get; set; }
        public IEnumerable<AutoCompleteViewModel> SiteGroups { get; set; }
        public IEnumerable<AutoCompleteViewModel> ReportTypes { get; set; }                   
    }    
}