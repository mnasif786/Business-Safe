using System;

namespace BusinessSafe.Application.Request
{
    public class ReinstateEmployeeAsNotDeleteRequest
    {
        public Guid EmployeeId { get; set; }
        public long CompanyId { get; set; }
        public Guid UserId { get; set; }
    }
}