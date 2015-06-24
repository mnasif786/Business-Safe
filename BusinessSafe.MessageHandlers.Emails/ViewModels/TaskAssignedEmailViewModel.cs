using System;
namespace BusinessSafe.MessageHandlers.Emails.ViewModels
{
    public class TaskAssignedEmailViewModel
    {
        public string From { get; set; }
        public string Subject { get; set; }
        public string To { get; set; }
        public string EmailTitle { get; set; }
        public string TaskType { get; set; }
        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public bool IncludeReference { get; set; }
        public string TaskReference { get; set; }
        public string AssignedBy { get; set; }
        public string CompletionDueDate { get; set; }
        public string TaskListUrl { get; set; }
        public string ActionRequired { get; set; }
        public bool IsIRN { get; set; }
        public string ReferenceTagTitle { get; set; }
        public string ActionRequiredTagTitle { get; set; }
    }
}
