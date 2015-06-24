using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class CountryDtoMapper
    {
        public CountryDto Map(Country entity)
        {
            return new CountryDto
                       {
                           Id = entity.Id,
                           Name = entity.Name
                       };
        }

        public IEnumerable<CountryDto> Map(IEnumerable<Country> entities)
        {
            return entities.Select(Map);
        }
    }
}
