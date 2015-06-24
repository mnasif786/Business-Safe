using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class ControlMeasureDtoMapper
    {
        public ControlMeasureDto Map(MultiHazardRiskAssessmentControlMeasure entity)
        {
            return new ControlMeasureDto()
            {
                Id = entity.Id,
                RiskAssessmentHazardId = entity.MultiHazardRiskAssessmentHazard.Id,
                ControlMeasure = entity.ControlMeasure
            };
        }

        public IEnumerable<ControlMeasureDto> Map(IEnumerable<MultiHazardRiskAssessmentControlMeasure> entities)
        {
            return entities.Select(Map);
        }
    }
}