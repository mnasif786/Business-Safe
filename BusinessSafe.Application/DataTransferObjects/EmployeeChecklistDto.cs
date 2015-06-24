using System;
using System.Collections.Generic;

using BusinessSafe.Application.Mappers;

namespace BusinessSafe.Application.DataTransferObjects
{
    public class EmployeeChecklistDto
    {
        public Guid Id { get; set; }
        public EmployeeDto Employee { get; set; }
        public ChecklistDto Checklist { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public string Password { get; set; }
        public IEnumerable<PersonalAnswerDto> Answers { get; set; }
        public string FriendlyReference { get; set; }
        public PersonalRiskAssessmentDto PersonalRiskAssessment { get; set; }
        public DateTime? DueDateForCompletion { get; set; }
        public string CompletionNotificationEmailAddress { get; set; }
        public bool? SendCompletedChecklistNotificationEmail { get; set; }
        public string LastRecipientEmail { get; set; }
        public string LastMessage { get; set; }
        public IList<EmployeeChecklistEmailDto> EmployeeChecklistEmails { get; set; }
        public virtual UserDto CompletedOnEmployeesBehalfBy { get; set; }
        public bool? IsFurtherActionRequired { get; set; }
        public EmployeeForAuditingDto AssessedByEmployee { get; set; }
        public DateTime? AssessmentDate { get; set; }
    }
}
