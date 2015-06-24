using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public static class BodyPartDtoMapper
    {
        public static BodyPartDto Map(this BodyPart entity)
        {
            return new BodyPartDto
                       {
                           Id = entity.Id,
                           Description = entity.Description
                       };
        }

        public static IEnumerable<BodyPartDto> Map(this IEnumerable<BodyPart> entities)
        {
            return entities.Select(Map);
        }
    }
}
