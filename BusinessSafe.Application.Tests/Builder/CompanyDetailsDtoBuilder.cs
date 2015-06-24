using BusinessSafe.Application.DataTransferObjects;

namespace BusinessSafe.Application.Tests.Builder
{
    public class CompanyDetailsDtoBuilder
    {
        private static int _id;
        private static string _addressLine1;

        public static CompanyDetailsDtoBuilder Create()
        {
            _id = 0;
            return new CompanyDetailsDtoBuilder();
        }

        public CompanyDetailsDto Build()
        {
            return new CompanyDetailsDto(_id, "company name", "can", _addressLine1, "address line 2", "address line 3",
                                         "address line 4", 12, "post code test", "123", "www.site.com", "01234");
        }

        public CompanyDetailsDtoBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public CompanyDetailsDtoBuilder WithAddressLine1(string addressLine1)
        {
            _addressLine1 = addressLine1;
            return this;
        }
    }
}