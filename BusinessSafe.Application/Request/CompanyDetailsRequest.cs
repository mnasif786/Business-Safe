
using System;

namespace BusinessSafe.Application.Request
{
    public class CompanyDetailsRequest
    {
        public long Id { get; set; }
        public string CAN { get; set; }
        public string ActioningUserName { get; set; }
        public Guid UserId { get; set; }
        public CompanyDetailsInformation ExistingCompanyDetails { get; set; }
        public CompanyDetailsInformation NewCompanyDetails { get; set; }

        public CompanyDetailsRequest()
        {

        }

        public CompanyDetailsRequest(string companyName, string can, string addressLine1, string addressLine2, string addressLine3, string addressLine4, string postcode, string telephone, string website, string mainContact, string actioningUserName, string businessSafeContactName)
        {
            CAN = can;
            ActioningUserName = actioningUserName;
            NewCompanyDetails = new CompanyDetailsInformation()
                                {
                                    CompanyName = companyName,
                                    AddressLine1 = addressLine1,
                                    AddressLine2 = addressLine2,
                                    AddressLine3 = addressLine3,
                                    AddressLine4 = addressLine4,
                                    Postcode = postcode,
                                    Telephone = telephone,
                                    Website = website,
                                    MainContact = mainContact,
                                    BusinessSafeContactName = businessSafeContactName
                                };
        }
    }
}

