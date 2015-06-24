using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class PictogramDtoMapper
    {
        public PictogramDto Map(Pictogram entity)
        {
            return new PictogramDto
                   {
                       Id = entity.Id,
                       Title = entity.Title,
                       HazardousSubstanceStandard = entity.HazardousSubstanceStandard
                   };
        }

        public IEnumerable<PictogramDto> Map(IEnumerable<Pictogram> entities)
        {
            return entities.Select(Map);
        }
    }
}
