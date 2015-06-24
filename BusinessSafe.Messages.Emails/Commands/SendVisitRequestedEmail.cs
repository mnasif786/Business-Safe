using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;

namespace BusinessSafe.Messages.Emails.Commands
{
    public class SendVisitRequestedEmail : IMessage
    {
        public string CompanyName { get; set; }
        public string CAN { get; set; }

        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }

        public string DateTo { get; set; }
        public string DateFrom { get; set; }
        public string RequestTime { get; set; }

        public string Comments { get; set; }
        public string SiteName { get; set; }
        public string Postcode { get; set; }
    }
}
