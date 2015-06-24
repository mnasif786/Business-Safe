namespace BusinessSafe.MessageHandlers.Emails.ViewModels
{
    public class ReviewAssignedEmailViewModel
    {
        public string From { get; set; }
        public string Subject { get; set; }
        public string To { get; set; }
        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public string TaskReference { get; set; }
        public string AssignedBy { get; set; }
        public string CompletionDueDate { get; set; }
        public string TaskListUrl { get; set; }
    }
}