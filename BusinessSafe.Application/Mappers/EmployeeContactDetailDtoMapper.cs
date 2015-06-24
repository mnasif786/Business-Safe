using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class EmployeeContactDetailDtoMapper
    {
        public EmployeeContactDetailDto MapWithCountry(EmployeeContactDetail entity)
        {
            return new EmployeeContactDetailDto
                       {
                           Id = entity.Id,
                           Address1 = entity.Address1,
                           Address2 = entity.Address2,
                           Address3 = entity.Address3,
                           Town = entity.Town,
                           County = entity.County,
                           PostCode = entity.PostCode,
                           Telephone1 = entity.Telephone1,
                           Telephone2 = entity.Telephone2,
                           PreferedTelephone = entity.PreferedTelephone,
                           Email = entity.Email,
                           Country = entity.Country != null ? new CountryDtoMapper().Map(entity.Country) : null
                       };
        }

        public IEnumerable<EmployeeContactDetailDto> MapWithCountry(IEnumerable<EmployeeContactDetail> entity)
        {
            return entity.Select(MapWithCountry);
        }
    }
}
