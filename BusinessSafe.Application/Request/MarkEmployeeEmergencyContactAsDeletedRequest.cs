using System;

namespace BusinessSafe.Application.Request
{
    public class MarkEmployeeEmergencyContactAsDeletedRequest
    {
        public long EmergencyContactId { get; set; }
        public long CompanyId { get; set; }
        public Guid UserId { get; set; }
        public Guid EmployeeId { get; set; }
    }
}