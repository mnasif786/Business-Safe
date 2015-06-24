using System;
using NServiceBus;

namespace BusinessSafe.Messages.Emails.Commands
{
    public class SendOffWorkReminderEmail : IMessage
    {
        public long AccidentRecordId { get; set; }
        public string RecipientEmail { get; set; }
        public string Title { get; set; }
        public string AccidentRecordReference { get; set; }
        public DateTime DateOfAccident { get; set; }
    }
}
