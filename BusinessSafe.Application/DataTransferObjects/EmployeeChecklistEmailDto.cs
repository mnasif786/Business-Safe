using System;
using System.Collections.Generic;

namespace BusinessSafe.Application.DataTransferObjects
{
    public class EmployeeChecklistEmailDto
    {
        public Guid Id { get; set; }
        public long? EmailPusherId { get; set; }
        public string Message { get; set; }
        public IEnumerable<EmployeeChecklistDto> EmployeeChecklists { get; set; }
        public string RecipientEmail { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}