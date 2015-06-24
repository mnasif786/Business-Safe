using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class SourceOfFuelDtoMapper
    {
        public SourceOfFuelDto Map(SourceOfFuel entity)
        {
            return new SourceOfFuelDto()
                       {
                           Id = entity.Id,
                           Name = entity.Name
                       };
        }

        public IEnumerable<SourceOfFuelDto> Map(IList<SourceOfFuel> entities)
        {
            return entities.Select(Map);
        }
    }
}