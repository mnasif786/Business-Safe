using System.Collections.Generic;
using BusinessSafe.Application.DataTransferObjects;

namespace BusinessSafe.Application.Tests.Builder
{
    public class SiteGroupDtoBuilder
    {
        private static int _groupId;
        private static string _addressLine1;
        private static string _addressLine2;
        private static string _addressLine3;
        private static string _addressLine4;
        private static string _contactName;
        private ContactDto _contactDto;
        private static string _postcode;
        private static string _telephone;

        public static SiteGroupDtoBuilder Create()
        {
            _addressLine1 = default(string);
            _addressLine2 = default(string);
            _addressLine3 = default(string);
            _addressLine4 = default(string);
            _groupId = 0;
            _postcode = default(string);
            _telephone = default(string);
            return new SiteGroupDtoBuilder();
        }

        public SiteGroupDto Build()
        {
            var siteGroupDto = new SiteGroupDto { Id = _groupId };
            return siteGroupDto;
        }

        public SiteGroupDtoBuilder WithGroupId(int groupId)
        {
            _groupId = groupId;
            return this;
        }

        //public SiteGroupDtoBuilder WithAddressLine1(string addressLine1)
        //{
        //    _addressLine1 = addressLine1;
        //    return this;
        //}

        //public SiteGroupDtoBuilder WithContactName(string contactName)
        //{
        //    _contactName = contactName;
        //    _contactDto = new ContactDto { ContactName = _contactName };
        //    return this;
        //}

        //public SiteGroupDtoBuilder WithSiteContact(ContactDto contactDto)
        //{
        //    _contactDto = contactDto;
        //    return this;
        //}

        //public SiteGroupDtoBuilder WithAddressLine2(string addressLine2)
        //{
        //    _addressLine2 = addressLine2;
        //    return this;
        //}
        //public SiteGroupDtoBuilder WithAddressLine3(string addressLine3)
        //{
        //    _addressLine3 = addressLine3;
        //    return this;
        //}
        //public SiteGroupDtoBuilder WithAddressLine4(string addressLine4)
        //{
        //    _addressLine4 = addressLine4;
        //    return this;
        //}
    }
}