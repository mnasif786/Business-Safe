using System.Web;

namespace BusinessSafe.MessageHandlers.Emails.ViewModels
{
    public class ActionTaskCompletedViewModel
    {
        public string From { get; set; }
        public string Subject { get; set; }
        public string To { get; set; }
        public string TaskReference { get; set; }
        public string Title { get; set; }
        public string AreaOfNonCompliance { get; set; }
        public string ActionRequired { get; set; }
        public string AssignedTo { get; set; }
        public string TaskType { get; set; }
        public HtmlString BusinessSafeOnlineLink { get; set; }
        public string User { get; set; }
        public bool IsIrn { get; set; }

        //Label Properties
        public string TaskReferenceLabel { get; set; }
        public string ActionRequiredLabel { get; set; }


    }
}
