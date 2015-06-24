using System;

namespace BusinessSafe.WebSite.Areas.Employees.ViewModels
{
    public class CanDeleteEmployeeViewModel
    {
        public long CompanyId { get; set; }
        public Guid EmployeeId { get; set; }
    }
}