using System;

namespace BusinessSafe.Application.Request
{
    public class EmployeeWithNewEmailRequest
    {
        public Guid EmployeeId { get; set; }
        public string NewEmail { get; set; }
        public bool NeedsNewEmail { get; set; }
    }
}
