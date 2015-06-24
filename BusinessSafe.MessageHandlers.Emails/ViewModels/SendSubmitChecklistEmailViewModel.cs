using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace BusinessSafe.MessageHandlers.Emails.ViewModels
{
    public class SendSubmitChecklistEmailViewModel
    {
        public string From { get; set; }
        public string Subject { get; set; }
        public List<string> To { get; set; }
        public HtmlString BusinessSafeOnlineLink { get; set; }
        public string HelpDeskLink { get; set; }
        public string AdviceLink { get; set; }
        public string Cc { get; set; }
       // public string NewAttachmentName { get; set; }
       // public string SavedAttachmentName { get; set; }

        public SendSubmitChecklistEmailViewModel()
        {
            To = new List<string>();
        }
    }

   
}
