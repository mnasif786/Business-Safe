using System;
using NServiceBus;

namespace BusinessSafe.Messages.Events
{
    public class TaskAssigned : IEvent
    {
        public Guid TaskGuid { get; set; }
    }
}
