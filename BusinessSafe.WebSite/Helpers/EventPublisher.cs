using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessSafe.Messages.Events;
using NServiceBus;

namespace BusinessSafe.WebSite.Helpers
{
    public class EventPublisher
    {
        private readonly IBus _bus;
        public EventPublisher(IBus bus)
        {
            _bus = bus;
        }

        public void PublishTaskAssigned(Guid taskGuid)
        {
            PublishTaskAssigned(new TaskAssigned { TaskGuid = taskGuid });
        }

        public void PublishTaskAssigned(TaskAssigned taskAssigned)
        {
            if (taskAssigned.TaskGuid == Guid.Empty)
            {
                throw new Exception("Task guid was not specified when publishing the TaskAssigned event");
            }
            _bus.Publish(taskAssigned);
        }
    }
}