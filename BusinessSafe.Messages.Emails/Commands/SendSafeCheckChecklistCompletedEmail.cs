using System;
using NServiceBus;

namespace BusinessSafe.Messages.Emails.Commands
{
    public class SendSafeCheckChecklistCompletedEmail : IMessage
    {
        public Guid ChecklistId { get; set; }
        public string Can { get; set; }
        public string Postcode { get; set; }
      }
}
