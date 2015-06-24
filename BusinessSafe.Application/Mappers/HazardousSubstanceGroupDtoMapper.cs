using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class HazardousSubstanceGroupDtoMapper
    {
        public HazardousSubstanceGroupDto Map(HazardousSubstanceGroup entity)
        {
            return new HazardousSubstanceGroupDto
                   {
                       Id = entity.Id,
                       Code = entity.Code
                   };
        }

        public IEnumerable<HazardousSubstanceGroupDto> Map(IEnumerable<HazardousSubstanceGroup> entities)
        {
            return entities.Select(Map);
        }
    }
}
