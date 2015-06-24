using System.Collections.Generic;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.Employees.ViewModels
{
    public class EmployeeSearchViewModel
    {
        public IEnumerable<AutoCompleteViewModel> Sites { get; set; }
        public string EmployeeReference { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public List<EmployeesSearchEmployeeViewModel> Employees { get; set; }
        public long CompanyId { get; set; }
        public long SiteId { get; set; }
    }
}