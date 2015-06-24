using NServiceBus;

namespace BusinessSafe.Messages.Emails.Commands
{
    public class SendReviewOverdueRiskAssessorEmail : IMessage
    {
        public string TaskReference { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string TaskAssignedTo { get; set; }
        public string RecipientEmail { get; set; }
        public string TaskCompletionDueDate { get; set; }
    }
}