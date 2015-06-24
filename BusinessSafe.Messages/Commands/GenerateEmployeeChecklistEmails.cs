using System;
using System.Collections.Generic;
using BusinessSafe.Messages.Commands.GenerateEmployeeChecklistEmailsParameters;
using NServiceBus;

namespace BusinessSafe.Messages.Commands
{
    public class GenerateEmployeeChecklistEmails : ICommand
    {
        public List<EmployeeWithNewEmail> RequestEmployees { get; set; }
        public List<long> ChecklistIds { get; set; }
        public string Message { get; set; }
        public Guid GeneratingUserId { get; set; }
        public long RiskAssessmentId { get; set; }
        public bool? SendCompletedChecklistNotificationEmail { get; set; }
        public DateTime? CompletionDueDateForChecklists { get; set; }
        public string CompletionNotificationEmailAddress { get; set; }
    }
}
