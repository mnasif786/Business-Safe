using System.Web;

namespace BusinessSafe.MessageHandlers.Emails.ViewModels
{
    public class ReviewCompletedEmailViewModel
    {
        public string From { get; set; }
        public string Subject { get; set; }
        public string To { get; set; }
        public string TaskReference { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string RiskAssessorName { get; set; }
        public HtmlString BusinessSafeOnlineLink { get; set; }
        public string User { get; set; }
        public string TaskCompletionDueDate { get; set; }
        public string TaskAssignedTo { get; set; }
    }
}