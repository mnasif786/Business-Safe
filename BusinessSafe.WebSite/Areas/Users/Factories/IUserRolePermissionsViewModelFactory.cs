using BusinessSafe.WebSite.Areas.Users.ViewModels;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.Users.Factories
{
    public interface IUserRolePermissionsViewModelFactory
    {
        UserRolePermissionsViewModel GetViewModel();
        IUserRolePermissionsViewModelFactory WithEnableCustomRoleEditing(bool enableCustomRoleEditing);
        IUserRolePermissionsViewModelFactory WithCompanyId(long companyId);
        IUserRolePermissionsViewModelFactory WithRoleId(string roleId);
    }
}