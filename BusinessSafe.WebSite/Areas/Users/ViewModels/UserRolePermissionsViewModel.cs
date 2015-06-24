using System.Collections.Generic;
using System.Security.Principal;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.Users.ViewModels
{
    public class UserRolePermissionsViewModel
    {
        public bool IsSystemRole { get; set; }
        public string RoleName { get; set; }
        public IList<PermissionsGroupViewModel> PermissionGroups { get; set; }
        public string RoleId{ get; set; }
        public bool CustomRoleEditingEnabled { get; set; }

        public bool IsSaveEnabled(IPrincipal user)
        {
            if (!CustomRoleEditingEnabled)
                return false;

            if (IsSystemRole)
                return false;

            if(user.IsInRole(Permissions.AddUsers.ToString()) || user.IsInRole(Permissions.EditUsers.ToString()))
            {
                return true;
            }

            return false;
        }

        public bool IsDeleteEnabled(IPrincipal user)
        {
            if (!CustomRoleEditingEnabled)
                return false;

            if (IsSystemRole)
                return false;

            if (RoleId == string.Empty)
                return false;

            if (user.IsInRole(Permissions.DeleteUsers.ToString()))
            {
                return true;
            }

            return false;
        }

        public bool IsNewRole
        {
            get { return RoleId == string.Empty; }
        }
    }
}