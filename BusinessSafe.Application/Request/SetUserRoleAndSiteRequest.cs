using System;

namespace BusinessSafe.Application.Request
{
    public class SetUserRoleAndSiteRequest
    {
        public Guid UserToUpdateId { get; set; }
        public long CompanyId { get; set; }
        public Guid RoleId { get; set; }
        public long SiteId { get; set; }
        public Guid ActioningUserId { get; set; }
        public bool PermissionsApplyToAllSites { get; set; }
    }
}
