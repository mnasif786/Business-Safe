using System.Collections.Generic;
using BusinessSafe.Application.DataTransferObjects;

namespace BusinessSafe.Application.Tests.Builder
{
    public class SiteAddressDtoBuilder
    {
        private static int _id;
        private static string _addressLine1;
        private static string _addressLine2;
        private static string _addressLine3;
        private static string _addressLine4;
        private static string _contactName;        
        private ContactDto _contactDto;
        private static string _postcode;
        private static string _telephone;
        private static string _addressLine5;
        private static string _county;

        public static SiteAddressDtoBuilder Create()
        {
            _addressLine1 = default(string);
            _addressLine2 = default(string);
            _addressLine3 = default(string);
            _addressLine4 = default(string);
            _addressLine5 = default(string);
            _county = default(string);
            _id = 0;
            _postcode = default(string);
            _telephone = default(string);
            return new SiteAddressDtoBuilder();
        }

        public SiteAddressDto Build()
        {
            var siteAddressDto = new SiteAddressDto(_id, _addressLine1, _addressLine2, _addressLine3, _addressLine4,_addressLine5,_county,
                                                    _postcode, _telephone, _contactDto);

            return siteAddressDto;
        }

        public SiteAddressDtoBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public SiteAddressDtoBuilder WithAddressLine1(string addressLine1)
        {
            _addressLine1 = addressLine1;
            return this;
        }

        public SiteAddressDtoBuilder WithContactName(string contactName)
        {
            _contactName = contactName;
            _contactDto = new ContactDto { ContactName = _contactName };
            return this;
        }

        public SiteAddressDtoBuilder WithSiteContact(ContactDto contactDto)
        {
            _contactDto = contactDto;
            return this;
        }

        public SiteAddressDtoBuilder WithAddressLine2(string addressLine2)
        {
            _addressLine2 = addressLine2;
            return this;
        }
        public SiteAddressDtoBuilder WithAddressLine3(string addressLine3)
        {
            _addressLine3 = addressLine3;
            return this;
        }
        public SiteAddressDtoBuilder WithAddressLine4(string addressLine4)
        {
            _addressLine4 = addressLine4;
            return this;
        }

        public SiteAddressDtoBuilder WithAddressLine5(string addressLine5)
        {
            _addressLine5 = addressLine5;
            return this;
        }

        public SiteAddressDtoBuilder WithCounty(string county)
        {
            _county = county;
            return this;
        }
    }
}