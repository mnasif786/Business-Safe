using NServiceBus;

namespace BusinessSafe.Messages.Emails.Commands
{
    public class SendResponsibilityTaskCompletedEmail : IMessage
    {
        public string TaskReference { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ResponsibilityOwnerName { get; set; }
        public string ResponsibilityOwnerEmail { get; set; }
        public string TaskAssignedTo { get; set; }
        public string CompletedBy { get; set; }
    }
}
