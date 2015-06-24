using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;

namespace BusinessSafe.Messages.Emails.Commands
{
    public class SendHardCopyToOfficeEmail : IMessage
    {
        public Guid ChecklistId { get; set; }
        public string CAN { get; set; }
        public DateTime? VisitDate { get; set; }
        public string VisitBy { get; set; }
        public string SubmittedBy { get; set; }
        public DateTime? SubmittedDate { get; set; }

        public string SiteName { get; set; }
        public string SiteAddressLine1 { get; set; }
        public string SiteAddressLine2 { get; set; }
        public string SiteAddressLine3 { get; set; }
        public string SiteAddressLine4 { get; set; }
        public string SiteAddressLine5 { get; set; }
        
        public string SitePostcode { get; set; }        
    }
}