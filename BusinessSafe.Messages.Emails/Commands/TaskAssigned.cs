using NServiceBus;

namespace BusinessSafe.Messages.Emails.Commands
{
    public class TaskAssigned : IMessage
    {
        public long TaskId { get; set; }
    }
}
