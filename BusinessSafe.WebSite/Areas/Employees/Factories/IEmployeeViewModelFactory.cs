using System;
using BusinessSafe.WebSite.Areas.Employees.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;

namespace BusinessSafe.WebSite.Areas.Employees.Factories
{
    public interface IEmployeeViewModelFactory
    {
        EmployeeViewModel GetViewModel();
        IEmployeeViewModelFactory WithEmployeeId(Guid? employeeId);
        IEmployeeViewModelFactory WithCompanyId(long companyId);
        IEmployeeViewModelFactory WithCurrentUser(ICustomPrincipal currentUser);
    }
}