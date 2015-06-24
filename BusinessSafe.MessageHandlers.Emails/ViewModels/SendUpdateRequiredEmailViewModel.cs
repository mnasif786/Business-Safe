using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessSafe.MessageHandlers.Emails.ViewModels
{
    public class SendUpdateRequiredEmailViewModel
    {
        public string From { get; set; }
        public string Subject { get; set; }
        public string To { get; set; }
        public string Can { get; set; }
        public string VisitDate { get; set; }
        public string ReportUrl { get; set; }
        public string AdvisorName { get; set; }
        public string Postcode { get; set; }
        public string QaComments { get; set; }
    }
}
