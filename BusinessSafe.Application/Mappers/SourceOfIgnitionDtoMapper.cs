using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class SourceOfIgnitionDtoMapper
    {
        public SourceOfIgnitionDto Map(SourceOfIgnition entity)
        {
            return new SourceOfIgnitionDto()
                       {
                           Id = entity.Id,
                           Name = entity.Name
                       };
        }

        public IEnumerable<SourceOfIgnitionDto> Map(IList<SourceOfIgnition> entities)
        {
            return entities.Select(Map);
        }
    }
}