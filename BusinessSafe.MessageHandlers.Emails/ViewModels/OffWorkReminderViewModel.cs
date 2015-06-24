using System.Web;

namespace BusinessSafe.MessageHandlers.Emails.ViewModels
{
    public class OffWorkReminderViewModel
    {
        public string From { get; set; }
        public string Subject { get; set; }
        public string To { get; set; }
        public string AccidentRecordReference { get; set; }
        public string Title { get; set; }
        public HtmlString BusinessSafeOnlineLink { get; set; }
        public string InjuredPerson { get; set; }
        public string DateOfAccident { get; set; }
        public string Jurisdiction { get; set; }

    }
}