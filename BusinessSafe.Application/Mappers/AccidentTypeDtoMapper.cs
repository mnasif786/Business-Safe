using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class AccidentTypeDtoMapper
    {
        public AccidentTypeDto Map(AccidentType entity)
        {
            return new AccidentTypeDto
                       {
                           Id = entity.Id,
                           Description = entity.Description
                       };
        }

        public IEnumerable<AccidentTypeDto> Map(IEnumerable<AccidentType> entities)
        {
            return entities.Select(Map);
        }
    }
}
