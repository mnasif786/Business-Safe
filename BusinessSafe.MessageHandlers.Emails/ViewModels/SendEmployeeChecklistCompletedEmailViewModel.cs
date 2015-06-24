using System.Web;

namespace BusinessSafe.MessageHandlers.Emails.ViewModels
{
    public class SendEmployeeChecklistCompletedEmailViewModel
    {
        public string From { get; set; }
        public string Subject { get; set; }
        public string To { get; set; }
        public string ChecklistName { get; set; }
        public string EmployeeWhoCompleted { get; set; }
        public string CompletedOn { get; set; }
        public HtmlString BusinessSafeOnlineLink { get; set; }
    }
}
