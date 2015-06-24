using System.Collections.Generic;

namespace BusinessSafe.WebSite.Areas.Users.ViewModels
{
    public class SaveUserRoleViewModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public List<string> Permisssions { get; set; }
        public long CompanyId { get; set; }
    }
}