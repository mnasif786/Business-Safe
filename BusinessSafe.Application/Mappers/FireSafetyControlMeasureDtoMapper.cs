using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public static class FireSafetyControlMeasureDtoMapper
    {
        public static FireSafetyControlMeasureDto Map(FireSafetyControlMeasure entity)
        {
            return new FireSafetyControlMeasureDto()
                       {
                           Id = entity.Id,
                           Name = entity.Name
                       };
        }

        public static IEnumerable<FireSafetyControlMeasureDto> Map(IList<FireSafetyControlMeasure> entities)
        {
            return entities.Select(Map);
        }
    }
}