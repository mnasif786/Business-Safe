using System.Collections.Generic;
using System.Web;

namespace BusinessSafe.WebSite.ViewModels
{
    public class PermissionsGroupViewModel
    {
        public string PermissionGroupName { get; set; }
        public IList<PermissionTarget> PermissionTargets { get; set; }
        public int PermissionGroupId { get; set; }
        public bool IsSystemRole { get; set; }
        public bool CustomRoleEditingEnabled { get; set; }

        public PermissionsGroupViewModel()
        {
            PermissionTargets = new List<PermissionTarget>();
        }
    }

    public class PermissionTarget
    {
        public string PermissionTargetName { get; set; }
        public IList<PermissionViewModel> Permissions { get; set; }
        public bool IsSystemRole { get; set; }
        public bool CustomRoleEditingEnabled { get; set; }

        public PermissionTarget()
        {
            Permissions = new List<PermissionViewModel>();
        }

        public HtmlString GetChecked(PermissionViewModel permission)
        {
            if (permission.Selected)
                return new HtmlString("checked='checked'");
            return new HtmlString("");
        }


        public HtmlString GetEnabled()
        {
            if (!CustomRoleEditingEnabled)
                return new HtmlString("disabled='disabled'");

            if (IsSystemRole)
                return new HtmlString("disabled='disabled'");
            return new HtmlString("");
        }
    }

}