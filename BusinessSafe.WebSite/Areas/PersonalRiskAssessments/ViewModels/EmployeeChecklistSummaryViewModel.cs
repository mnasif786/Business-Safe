using System;

namespace BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels
{
    public class EmployeeChecklistSummaryViewModel
    {
        public Guid Id { get; set; }
        public object FriendlyReference { get; set; }
        public string ChecklistTitle { get; set; }
        public string RecipientFullName { get; set; }
        public string RecipientEmail { get; set; }
        public string DueDateForCompletion { get; set; }
        public string CompleteDate { get; set; }
        public string MessageBody { get; set; }
        public bool NotificationRequired { get; set; }
        public string CompletionNotificationEmailAddress { get; set; }
        public string LastRecipientEmail { get; set; }
        public string ChecklistUrl { get; set; }
        public bool ShowCompletedOnEmployeesBehalfBySection { get; set; }
        public string CompletedOnEmployeesBehalfName { get; set; }
        public bool? IsFurtherActionRequired { get; set; }
        public string AssessedBy { get; set; }
        public string AssessmentDate { get; set; }
    }
}