using System;
using NServiceBus;

namespace BusinessSafe.Messages.Events
{
    public class ReviewAssigned : IEvent
    {
        public Guid TaskGuid { get; set; }
    }
}