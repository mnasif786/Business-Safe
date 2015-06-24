using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public static class InjuryDtoMapper
    {
        public static InjuryDto Map(this Injury entity)
        {
            return new InjuryDto
                       {
                           Id = entity.Id,
                           Description = entity.Description
                       };
        }

        public static IEnumerable<InjuryDto> Map(this IEnumerable<Injury> entities)
        {
            return entities.Select(Map);
        }
    }
}
