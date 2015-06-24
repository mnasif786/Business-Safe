using System;

namespace BusinessSafe.Application.Request
{
    public class CompanyDetailsInformation
    {
        public string CompanyName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string Postcode { get; set; }
        public string Telephone { get; set; }
        public string Website { get; set; }
        public string MainContact { get; set; }
        public Guid BusinessSafeContactId { get; set; }
        public string BusinessSafeContactName { get; set; }
    }
}