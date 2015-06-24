using System;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Request
{
    public class CreateEmployeeAsUserRequest
    {
        public Guid EmployeeId { get; set; }
        public Guid NewUserId { get; set; }
        public long CompanyId { get; set; }
        public Guid RoleId { get; set; }
        public long SiteId { get; set; }
        public Guid ActioningUserId { get; set; }
        public long MainSiteId { get; set; }
        public EmployeeContactDetail EmployeeContact { get; set; }
        public bool PermissionsForAllSites { get; set; }
    }
}
