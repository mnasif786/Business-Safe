using System;

namespace BusinessSafe.WebSite.Areas.Users.ViewModels
{
    public class ViewUserViewModel
    {
        public long CompanyId;
        public string Name;
        public string EmployeeReference;
        public string JobTitle;
        public string Department;
        public string ManagerName;
        public string Role;
        public string PermissionLevel;
        public Guid RoleId;
        public Guid EmployeeId;
    }
}