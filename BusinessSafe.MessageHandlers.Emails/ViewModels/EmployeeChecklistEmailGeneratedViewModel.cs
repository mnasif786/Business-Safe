using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web;
using System.Linq;

namespace BusinessSafe.MessageHandlers.Emails.ViewModels
{
    public class EmployeeChecklistEmailGeneratedViewModel
    {
        public string From { get; set; }
        public string Subject { get; set; }
        public string To { get; set; }
        public IList<string> Message { get; set; }
        public IEnumerable<string> ChecklistUrls { get; set; }
        public HtmlString CompletionDueDateForChecklists { get; set; }
        //public HtmlString GetCompletionDueDateInformation()

    }
}