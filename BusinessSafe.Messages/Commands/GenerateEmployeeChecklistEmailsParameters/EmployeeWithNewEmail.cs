using System;

namespace BusinessSafe.Messages.Commands.GenerateEmployeeChecklistEmailsParameters
{
    public class EmployeeWithNewEmail
    {
        public Guid EmployeeId { get; set; }
        public string NewEmail { get; set; }
    }
}
