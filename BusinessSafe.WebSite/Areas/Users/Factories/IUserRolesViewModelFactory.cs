using BusinessSafe.WebSite.Areas.Users.ViewModels;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.Users.Factories
{
    public interface IUserRolesViewModelFactory
    {
        UserRolesViewModel GetViewModel();
        IUserRolesViewModelFactory WithCompanyId(long companyId);
        IUserRolesViewModelFactory WithRoleId(string roleId);
        UserRolesViewModel GetViewModelForNewUseRole();
        
    }
}