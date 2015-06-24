using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class JurisdictionDtoMapper
    {
        public JurisdictionDto Map(Jurisdiction entity)
        {
            return new JurisdictionDto
                       {
                           Id = entity.Id,
                           Name = entity.Name
                       };
        }

        public IEnumerable<JurisdictionDto> Map(IEnumerable<Jurisdiction> entities)
        {
            return entities.Select(Map);
        }
    }
}
