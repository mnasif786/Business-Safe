using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using BusinessSafe.Domain.Entities.SafeCheck;

namespace BusinessSafe.MessageHandlers.Emails.ViewModels
{
    public class SendIRNNotificationEmailViewModel
    {
        public string From { get; set; }
        public string Subject { get; set; }
        public string To { get; set; }
        public HtmlString BusinessSafeOnlineLink { get; set; }                
        public string CAN { get; set; }
        public string VisitDate { get; set; }
        public string CompletedBy { get; set; }
        public string CompletedOn { get; set; }
    
        public IList<ImmediateRiskNotification> IRNList { get; set; }
    }
}