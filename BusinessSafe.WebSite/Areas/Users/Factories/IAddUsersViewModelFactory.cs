using System;
using System.Security.Principal;
using BusinessSafe.WebSite.Areas.Users.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;

namespace BusinessSafe.WebSite.Areas.Users.Factories
{
    public interface IAddUsersViewModelFactory
    {
        AddUsersViewModel GetViewModel(long companyId, Guid? employeeId, bool? saveSuccess, bool canChangeEmployee);
        AddUsersViewModel GetViewModel();
        IAddUsersViewModelFactory WithCompanyId(long companyId);
        IAddUsersViewModelFactory WithEmployeeId(Guid userId);
        IAddUsersViewModelFactory WithCurrentUser(ICustomPrincipal currentUser);
    }
}