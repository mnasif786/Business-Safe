using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;

namespace BusinessSafe.Messages.Events
{
    public class EmployeeChecklistCompleted: IEvent
    {
        public Guid EmployeeChecklistId { get; set; }
        public DateTime CompletedDate { get; set; }
    }
}
