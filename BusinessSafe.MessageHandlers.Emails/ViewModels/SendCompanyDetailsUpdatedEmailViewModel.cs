namespace BusinessSafe.MessageHandlers.Emails.ViewModels
{
    public class SendCompanyDetailsUpdatedEmailViewModel
    {
        public string From { get; set; }
        public string Subject { get; set; }
        public string To { get; set; }

        public long CompanyId { get; set; }
        public string CAN { get; set; } // this is labelled 'Company Id' in the actual email as per ticket GRL52

        public string ActioningUserName { get; set; }

        public string OldCompanyName { get; set; }
        public string OldAddressLine1 { get; set; }
        public string OldAddressLine2 { get; set; }
        public string OldAddressLine3 { get; set; }
        public string OldAddressLine4 { get; set; }
        public string OldPostcode { get; set; }
        public string OldTelephone { get; set; }
        public string OldWebsite { get; set; }
        public string OldMainContact { get; set; }
        public string OldBusinssSafeContactName { get; set; }

        public string CompanyName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string Postcode { get; set; }
        public string Telephone { get; set; }
        public string Website { get; set; }
        public string MainContact { get; set; }
        public string BusinssSafeContactName { get; set; }
    }
}