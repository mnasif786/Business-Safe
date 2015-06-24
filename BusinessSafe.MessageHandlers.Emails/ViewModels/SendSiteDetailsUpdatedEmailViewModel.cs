namespace BusinessSafe.MessageHandlers.Emails.ViewModels
{
    public class SendSiteDetailsUpdatedEmailViewModel
    {
        public string ActioningUserName { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string To { get; set; }

        public long CompanyId { get; set; }
        public string CAN { get; set; }

        public string SiteContactUpdated { get; set; }
        public string NameUpdated { get; set; }
        public string AddressLine1Updated { get; set; }
        public string AddressLine2Updated { get; set; }
        public string AddressLine3Updated { get; set; }
        public string AddressLine4Updated { get; set; }
        public string AddressLine5Updated { get; set; }
        public string CountyUpdated { get; set; }
        public string PostcodeUpdated { get; set; }
        public string TelephoneUpdated { get; set; }

        public string SiteContactCurrent { get; set; }
        public string NameCurrent { get; set; }
        public string AddressLine1Current { get; set; }
        public string AddressLine2Current { get; set; }
        public string AddressLine3Current { get; set; }
        public string AddressLine4Current { get; set; }
        public string AddressLine5Current { get; set; }
        public string CountyCurrent { get; set; }
        public string PostcodeCurrent { get; set; }
        public string TelephoneCurrent { get; set; }
        public string SiteStatusCurrent { get; set; }
        public string SiteStatusUpdated { get; set; }
    }
}