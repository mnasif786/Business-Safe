using System;
using System.Collections.Generic;

namespace BusinessSafe.Application.Request
{
    public class SaveChecklistGeneratorRequest
    {
        public long PersonalRiskAssessmentId { get; set; }
        public bool? HasMultipleChecklistRecipients { get; set; }
        public IList<EmployeeWithNewEmailRequest> RequestEmployees { get; set; }
        public IList<long> ChecklistIds { get; set; }
        public string Message { get; set; }
        public Guid CurrentUserId { get; set; }
        public bool? SendCompletedChecklistNotificationEmail { get; set; }
        public DateTime? CompletionDueDateForChecklists { get; set; }
        public string CompletionNotificationEmailAddress { get; set; }
    }
}
