using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessSafe.MessageHandlers.Emails.ViewModels
{
    public class SendHardCopyToOfficeEmailViewModel
    {
        public string Subject { get; set; }
        public string To { get; set; }
        public string From { get; set; }

        public Guid ChecklistId { get; set; }
        public string CAN { get; set; }
        public string VisitDate { get; set; }
        public string VisitBy { get; set; }
        public string SubmittedBy { get; set; }
        public string SubmittedDate{ get; set; }
        public string SiteAddress { get; set; }
    }
}
