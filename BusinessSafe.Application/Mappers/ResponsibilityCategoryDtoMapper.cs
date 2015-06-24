using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class ResponsibilityCategoryDtoMapper
    {
        public ResponsibilityCategoryDto Map(ResponsibilityCategory entity)
        {
            return new ResponsibilityCategoryDto()
            {
                Id = entity.Id,
                Category = entity.Category
            };
        }

        public IEnumerable<ResponsibilityCategoryDto> Map(IEnumerable<ResponsibilityCategory> entities)
        {
            return entities.Select(Map);
        }
    }
}
