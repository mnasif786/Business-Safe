using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.Company.ViewModels
{
    public class AccidentRecordDistributionListViewModel
    {                        
        public long? SiteId { get; set; }
        public long CompanyId { get; set; }

        public IEnumerable<AutoCompleteViewModel> Sites { get; set; }
        public List<EmployeeDto> EmployeesToSelectFrom { get; set; }
        public List<SelectedEmployeeViewModel> SelectedEmployees { get; set; }
    }
}