using System;

namespace BusinessSafe.Application.Request
{
    public class UpdateUserRoleRequest
    {
        public string RoleName { get; set; }
        public int[] Permissions { get; set; }
        public long CompanyId { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}