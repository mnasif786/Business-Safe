using System.Collections.Generic;
using System.Security.Principal;
using BusinessSafe.WebSite.Areas.Employees.ViewModels;

namespace BusinessSafe.WebSite.Areas.Employees.Factories
{
    public interface IEmployeeSearchViewModelFactory
    {
        EmployeeSearchViewModel GetViewModel();
        IEmployeeSearchViewModelFactory WithCompanyId(long companyId);
        IEmployeeSearchViewModelFactory WithAllowedSites(IList<long> allowedSites);
        IEmployeeSearchViewModelFactory WithEmployeeReference(string employeeReference);
        IEmployeeSearchViewModelFactory WithForeName(string foreName);
        IEmployeeSearchViewModelFactory WithSurname(string surname);
        IEmployeeSearchViewModelFactory WithSiteId(long siteId);
        IEmployeeSearchViewModelFactory WithShowDeleted(bool showDeleted);
        IEmployeeSearchViewModelFactory WithCurrentUser(IPrincipal currentUser);
    }
}