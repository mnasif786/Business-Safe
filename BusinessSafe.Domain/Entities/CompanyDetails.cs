using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class CompanyDetails 
    {
        public long Id { get; protected set; }
        public string CompanyName { get; protected set; }
        public string CAN { get; protected set; }
        public string AddressLine1 { get; protected set; }
        public string AddressLine2 { get; protected set; }
        public string AddressLine3 { get; protected set; }
        public string AddressLine4 { get; protected set; }
        public string Postcode { get; protected set; }
        public string Telephone { get; protected set; }
        public string Website { get; protected set; }
        public string MainContact { get; protected set; }

        public virtual bool Save(string body, string subject)
        {
            return true;
        }

        public static CompanyDetails Create(long id, string companyName, string can, string addressLine1, string addressLine2, string addressLine3,
                                            string addressLine4, string postcode, string telephone, string website, string mainContact)
        {
            return new CompanyDetails
            {
                Id = id,
                CompanyName = companyName,
                CAN = can,
                AddressLine1 = addressLine1,
                AddressLine2 = addressLine2,
                AddressLine3 = addressLine3,
                AddressLine4 = addressLine4,
                Postcode = postcode,
                Telephone = telephone,
                Website = website,
                MainContact = mainContact
            };
        }
    }
}