using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Tests.Builder
{
    public class CompanyDetailsBuilder
    {
        private static int _id;
        private static string _companyName;
        private static string _can;
        private static string _addressLine1;
        private static string _addressLine2;
        private static string _addressLine3;
        private static string _addressLine4;
        private static string _postcode;
        private static string _telephone;
        private static string _website;
        private static string _mainContact;

        public static CompanyDetailsBuilder Build()
        {
            _id = 0;
            _companyName = "company name";
            _can = "can test";
            _addressLine1 = "address line 1";
            _addressLine2 = "address line 2";
            _addressLine3 = "address line 3";
            _addressLine4 = "address line 4";
            _postcode = "test postcode";
            _telephone = "telephone test ";
            _website = "test website";
            _mainContact = "main contact test";

            return new CompanyDetailsBuilder();
        }

        public CompanyDetails Create()
        {
            return CompanyDetails.Create(_id, _companyName, _can, _addressLine1, _addressLine2, _addressLine3, _addressLine4,
                                         _postcode, _telephone, _website, _mainContact);
        }
    }
}