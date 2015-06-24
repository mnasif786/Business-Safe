using System;

namespace BusinessSafe.Application.Request
{
    public class UpdateOnlineRegistrationDetailsRequest
    {
        public Guid EmployeeId { get; set; }
        public string Email { get; set; }
        public long CompanyId { get; set; }
        public Guid CurrentUserId { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
    }
}
