using System;

namespace BusinessSafe.Application.Request
{
    public class AddUserRoleRequest
    {
        public string RoleName { get; set; }
        public int[] Permissions { get; set; }
        public long CompanyId { get; set; }
        public Guid UserId { get; set; }


        public AddUserRoleRequest()
        {
            Permissions = new int[] {};
        }
    }
}