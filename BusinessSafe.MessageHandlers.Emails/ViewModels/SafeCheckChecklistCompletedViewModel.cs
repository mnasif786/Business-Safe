using System.Web;

namespace BusinessSafe.MessageHandlers.Emails.ViewModels
{
    public class SafeCheckChecklistCompletedViewModel
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Can { get; set; }
        public string ReportUrl { get; set; }
        public string User { get; set; }
        public string Postcode { get; set; }
        public string VisitDate { get; set; }
        public string CompletedDate { get; set; }
        public string CompletedBy { get; set; }
    }
}
