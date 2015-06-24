using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class CauseOfAccidentDtoMapper
    {
        public CauseOfAccidentDto Map(CauseOfAccident entity)
        {
            return new CauseOfAccidentDto
                       {
                           Id = entity.Id,
                           Description = entity.Description
                       };
        }

        public IEnumerable<CauseOfAccidentDto> Map(IEnumerable<CauseOfAccident> entities)
        {
            return entities.Select(Map);
        }
    }
}
