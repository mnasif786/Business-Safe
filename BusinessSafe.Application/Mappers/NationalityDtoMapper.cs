using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class NationalityDtoMapper
    {
        public NationalityDto Map(Nationality entity)
        {
            return new NationalityDto
                       {
                           Id = entity.Id,
                           Name = entity.Name
                       };
        }

        public IEnumerable<NationalityDto> Map(IEnumerable<Nationality> entities)
        {
            return entities.Select(Map);
        }
    }
}
