using BusinessSafe.Domain.Entities;

namespace BusinessSafe.WebSite.ViewModels
{
    public class PermissionViewModel
    {
        public int PermissionId { get; set; }
        public PermissionActivity PermissionActivity { get; set; }
        public string PermissionName { get; set; }
        public bool Selected { get; set; }
    }
}

