using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class FireRiskAssessmentControlMeasureDtoMapper
    {
        public FireRiskAssessmentControlMeasureDto MapWithControlMeasure(FireRiskAssessmentControlMeasure entity)
        {
            return new FireRiskAssessmentControlMeasureDto()
                       {
                           //RiskAssessment = new FireRiskAssessmentDtoMapper().Map(entity.RiskAssessment),
                           ControlMeasure = FireSafetyControlMeasureDtoMapper.Map(entity.FireSafetyControlMeasure)
                       };
        }

        public IEnumerable<FireRiskAssessmentControlMeasureDto> MapWithControlMeasure(IList<FireRiskAssessmentControlMeasure> entities)
        {
            return entities.Select(MapWithControlMeasure);
        }
    }
}