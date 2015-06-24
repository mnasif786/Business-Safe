using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class FireRiskAssessmentSourceOfIgnitionDtoMapper
    {
        public FireRiskAssessmentSourceOfIgnitionDto MapWithSourceOfIgnition(FireRiskAssessmentSourceOfIgnition entity)
        {
            return new FireRiskAssessmentSourceOfIgnitionDto
                       {
                           SourceOfIgnition = new SourceOfIgnitionDtoMapper().Map(entity.SourceOfIgnition)
                       };
        }

        public IEnumerable<FireRiskAssessmentSourceOfIgnitionDto> MapWithSourceOfIgnition(IEnumerable<FireRiskAssessmentSourceOfIgnition> entites)
        {
            return entites.Select(MapWithSourceOfIgnition);
        }
    }
}
