using System.Collections.Generic;
using AutoMapper;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Mappers.AutoMapper;
using BusinessSafe.Application.RestAPI.Responses;


namespace BusinessSafe.Application.Mappers
{
    public class SiteAddressDtoMapper
    {
        private readonly AutoMapperConfiguration _autoMapperConfiguration = AutoMapperConfiguration.Instance;

        public SiteAddressDto Map(SiteAddressResponse siteAddressResponse)
        {
            var contact = Mapper.Map<Contact, ContactDto>(siteAddressResponse.SiteContact);
            var siteAddressDto = Mapper.Map<SiteAddressResponse, SiteAddressDto>(siteAddressResponse);
            siteAddressDto.SiteAddress = contact;
            return siteAddressDto;
        }

        public IEnumerable<SiteAddressDto> Map(IEnumerable<SiteAddressResponse> siteAddressResponse)
        {
            return Mapper.Map<IEnumerable<SiteAddressResponse>, List<SiteAddressDto>>(siteAddressResponse);
        }
    }
}