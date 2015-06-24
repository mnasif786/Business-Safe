using System;
using System.Collections.Generic;
using NServiceBus;

namespace BusinessSafe.Messages.Events
{
    public class EmployeeChecklistEmailGenerated : IEvent
    {
        public Guid EmployeeChecklistEmailId { get; set; }
        public string RecipientEmail { get; set; }
        public List<Guid> EmployeeChecklistIds { get; set; }
        public string Message { get; set; }
        public DateTime? CompletionDueDateForChecklists { get; set; }
    }
}
