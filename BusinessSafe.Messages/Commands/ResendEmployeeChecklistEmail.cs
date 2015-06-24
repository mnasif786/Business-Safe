using System;
using NServiceBus;

namespace BusinessSafe.Messages.Commands
{
    public class ResendEmployeeChecklistEmail : ICommand
    {
        public Guid EmployeeChecklistId { get; set; }
        public Guid ResendUserId { get; set; }
        public long RiskAssessmentId { get; set; }
    }
}