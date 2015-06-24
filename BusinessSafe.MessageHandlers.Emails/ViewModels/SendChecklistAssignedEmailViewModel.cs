using System;

namespace BusinessSafe.MessageHandlers.Emails.ViewModels
{
    public class SendChecklistAssignedEmailViewModel
    {
        public string From { get; set; }
        public string Subject { get; set; }
        public string To { get; set; }
        public string Can { get; set; }
        public string VisitDate { get; set; }
        public string VisitBy { get; set; }
        public string LinkUrl { get; set; }
        public string CompletedDate { get; set; }
        public string EmailReportTo { get; set; }
        public string PostReportTo { get; set; }
    }
}
