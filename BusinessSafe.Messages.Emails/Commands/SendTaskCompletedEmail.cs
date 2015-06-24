using System;
using NServiceBus;

namespace BusinessSafe.Messages.Emails.Commands
{
    public class SendTaskCompletedEmail : IMessage
    {
        public string TaskReference { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string RiskAssessorName { get; set; }
        public string RiskAssessorEmail { get; set; }
        public string TaskAssignedTo { get; set; }
    }
}
