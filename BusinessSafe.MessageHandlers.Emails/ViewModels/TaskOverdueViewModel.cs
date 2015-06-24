using System.Web;

namespace BusinessSafe.MessageHandlers.Emails.ViewModels
{
    public class TaskOverdueViewModel
    {
        public string From { get; set; }
        public string Subject { get; set; }
        public string To { get; set; }
        public string TaskReference { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string RiskAssessor { get; set; }
        public HtmlString BusinessSafeOnlineLink { get; set; }
        public string TaskCompletionDueDate { get; set; }
        public string TaskAssignedTo { get; set; }
        public string TaskTypeDescription { get; set; }
        public string AreaOfNonCompliance { get; set; }
        public string ActionRequired { get; set; }
        public bool IsIRN { get; set; }
        public string ReferenceLabel { get; set; }
        public string ActionRequiredLabel { get; set; }
    }
}