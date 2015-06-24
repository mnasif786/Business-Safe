using System;

namespace BusinessSafe.Application.Request
{
    public class UpdateEmployeeEmailAddressRequest
    {
        public Guid EmployeeId { get; set; }
        public string Email { get; set; }
        public long CompanyId { get; set; }
        public Guid CurrentUserId { get; set; }
    }
}