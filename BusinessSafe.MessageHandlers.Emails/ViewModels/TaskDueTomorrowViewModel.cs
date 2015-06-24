
namespace BusinessSafe.MessageHandlers.Emails.ViewModels
{
    public class TaskDueTomorrowViewModel
    {
        public string TaskType { get; set; }
        public string Name { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string To { get; set; }
        public string Message { get; set; }
        public string DueDate { get; set; }
        public string TaskReference { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AssignedBy { get; set; }
        public System.Web.HtmlString BusinessSafeOnlineLink { get; set; }
    }
}
