using System;

namespace BusinessSafe.Application.Request
{
    public class MarkUserRoleAsDeletedRequest
    {
        public Guid UserRoleId { get; set; }
        public long CompanyId { get; set; }
        public Guid UserId { get; set; }
    }
}