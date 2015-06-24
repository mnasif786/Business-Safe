using NServiceBus;

namespace BusinessSafe.Messages.Emails.Commands
{
    public class SendCompanyDetailsUpdatedEmail : IMessage
    {
        public string ActioningUserName { get; set; }
        public long CompanyId { get; set; }
        public string CAN { get; set; }
        public CompanyDetailsInformation ExistingCompanyDetailsInformation { get; set; }
        public CompanyDetailsInformation NewCompanyDetailsInformation { get; set; }

        public SendCompanyDetailsUpdatedEmail()
        {
            ExistingCompanyDetailsInformation = new CompanyDetailsInformation();
            NewCompanyDetailsInformation = new CompanyDetailsInformation();
        }
    }

    public class CompanyDetailsInformation {
        public string CompanyName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string Postcode { get; set; }
        public string Telephone { get; set; }
        public string Website { get; set; }
        public string MainContact { get; set; }
        public string BusinessSafeContactName { get; set; }
        
    }
}
