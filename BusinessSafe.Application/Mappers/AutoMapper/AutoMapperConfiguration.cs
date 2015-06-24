using AutoMapper;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.RestAPI.Responses;

namespace BusinessSafe.Application.Mappers.AutoMapper
{
    public class AutoMapperConfiguration
    {

        AutoMapperConfiguration()
        {
            Mapper.CreateMap<Contact, ContactDto>();

            Mapper.CreateMap<SiteAddressResponse, SiteAddressDto>()
                  .ConstructUsing(x => new SiteAddressDto((int)x.Id, x.Address1, x.Address2, x.Address3, x.Address4, x.Address5, x.County, x.Postcode, x.Telephone, null));

            Mapper.CreateMap<SiteAddressResponse, SiteAddressDto>()
                  .ConstructUsing(sar => new SiteAddressDto(sar.Id, sar.Address1, sar.Address2, sar.Address3, sar.Address4, sar.Address5, sar.County, sar.Postcode, sar.Telephone, Mapper.Map<Contact, ContactDto>(sar.SiteContact)));
        }

        class Nested
        {
            static Nested()
            {
            }

            internal static readonly AutoMapperConfiguration _instance = new AutoMapperConfiguration();
        }

        public static AutoMapperConfiguration Instance
        {
            get { return Nested._instance; }
        }
    }
}