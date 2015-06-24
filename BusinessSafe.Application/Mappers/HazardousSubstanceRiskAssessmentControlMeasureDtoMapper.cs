using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class HazardousSubstanceRiskAssessmentControlMeasureDtoMapper
    {
        public HazardousSubstanceRiskAssessmentControlMeasureDto Map(HazardousSubstanceRiskAssessmentControlMeasure entity)
        {
            return new HazardousSubstanceRiskAssessmentControlMeasureDto()
                       {
                           Id = entity.Id,
                           HazardousSubstanceRiskAssessmentId = entity.HazardousSubstanceRiskAssessment.Id,
                           ControlMeasure = entity.ControlMeasure
                       };
        }

        public IEnumerable<HazardousSubstanceRiskAssessmentControlMeasureDto> Map(IEnumerable<HazardousSubstanceRiskAssessmentControlMeasure> entities)
        {
            return entities.Select(Map);
        }
    }
}