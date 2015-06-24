using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;

namespace BusinessSafe.Messages.Emails.Commands
{
    public class SendChecklistAssignedEmail : IMessage
    {
        public Guid ChecklistId { get; set; }
        public Guid AssignedToId { get; set; }
        public string Can { get; set; }
        public string Postcode { get; set; }
        public string SiteName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string Address5 { get; set; }
    }
}
