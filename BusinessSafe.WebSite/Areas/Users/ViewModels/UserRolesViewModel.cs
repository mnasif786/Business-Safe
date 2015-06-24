using System.Collections.Generic;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.Users.ViewModels
{
    public class UserRolesViewModel
    {   
        public long CompanyId { get; set; }
        public IEnumerable<AutoCompleteViewModel> CompanyRoles { get; set; }
        public bool IsNewUserRole { get; set; }
        public string RoleId { get; set; }
    }
}