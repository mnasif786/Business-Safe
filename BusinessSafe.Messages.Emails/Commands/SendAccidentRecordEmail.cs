using NServiceBus;

namespace BusinessSafe.Messages.Emails.Commands
{
    public class SendAccidentRecordEmail : IMessage
    {
        public long AccidentRecordId { get; set; }
        public long CompanyId { get; set; }
    }
}
