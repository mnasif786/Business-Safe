using System.ComponentModel;

namespace BusinessSafe.Domain.Entities
{
    public enum PermissionActivity
    {
        [Description("View")]
        View = 1,

        [Description("Add")]
        Add = 2,

        [Description("Edit")]
        Edit = 3,

        [Description("Delete")]
        Delete = 4
    }
}