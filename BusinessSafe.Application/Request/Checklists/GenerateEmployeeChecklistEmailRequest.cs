using System;
using System.Collections.Generic;

namespace BusinessSafe.Application.Request.Checklists
{
    public class GenerateEmployeeChecklistEmailRequest
    {
        public IList<EmployeeWithNewEmailRequest> RequestEmployees { get; set; } 
        public List<long> ChecklistIds { get; set; }
        public string Message { get; set; }
        public Guid GeneratingUserId { get; set; }
        public long RiskAssessmentId { get; set; }
        public bool? SendCompletedChecklistNotificationEmail { get; set; }
        public DateTime? CompletionDueDateForChecklists { get; set; }
        public string CompletionNotificationEmailAddress { get; set; }

        public GenerateEmployeeChecklistEmailRequest()
        {}
    }
}
