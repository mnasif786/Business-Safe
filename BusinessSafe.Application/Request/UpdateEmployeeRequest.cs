using System;

namespace BusinessSafe.Application.Request
{
    public class UpdateEmployeeRequest : SaveEmployeeRequest
    {
        public Guid EmployeeId { get; set; }
    }
}