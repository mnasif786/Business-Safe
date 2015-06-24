using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;

namespace BusinessSafe.Messages.Emails.Commands
{
    public class SendIRNNotificationEmail : IMessage
    {
        public Guid ChecklistId { get; set; }

        public string CAN { get; set; }
    }
}
