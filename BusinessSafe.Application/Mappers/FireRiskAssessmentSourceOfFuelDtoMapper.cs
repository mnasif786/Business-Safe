using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;
using System.Linq;
using System.Collections.Generic;

namespace BusinessSafe.Application.Mappers
{
    public class FireRiskAssessmentSourceOfFuelDtoMapper
    {
        public FireRiskAssessmentSourceOfFuelDto MapWithSourceOfFuel(FireRiskAssessmentSourceOfFuel entity)
        {
            return new FireRiskAssessmentSourceOfFuelDto
                       {
                           SourceOfFuel = new SourceOfFuelDtoMapper().Map(entity.SourceOfFuel)
                       };
        }

        public IEnumerable<FireRiskAssessmentSourceOfFuelDto> MapWithSourceOfFuel(IEnumerable<FireRiskAssessmentSourceOfFuel> entities)
        {
            return entities.Select(MapWithSourceOfFuel);
        }
    }
}
