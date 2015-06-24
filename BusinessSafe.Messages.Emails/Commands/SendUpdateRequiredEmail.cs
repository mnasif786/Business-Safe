using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;

namespace BusinessSafe.Messages.Emails.Commands
{
    public class SendUpdateRequiredEmail : IMessage
    {
        public Guid ChecklistId { get; set; }
        public string Can { get; set; }
        public string Postcode { get; set; }
    }
}
