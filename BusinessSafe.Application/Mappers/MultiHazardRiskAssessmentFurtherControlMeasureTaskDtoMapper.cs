using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class MultiHazardRiskAssessmentFurtherControlMeasureTaskDtoMapper
    {
        public MultiHazardRiskAssessmentFurtherControlMeasureTaskDto MapWithAssignedToAndRiskAssessmentHazard(MultiHazardRiskAssessmentFurtherControlMeasureTask entity)
        {
            var dto = new TaskDtoMapper().MapWithAssignedTo(entity) as MultiHazardRiskAssessmentFurtherControlMeasureTaskDto;

            dto.RiskAssessmentHazard =
                entity.MultiHazardRiskAssessmentHazard != null
                    ? new RiskAssessmentHazardDtoMapper().Map(
                        entity.MultiHazardRiskAssessmentHazard)
                    : null;

            return dto;
        }
    }
}
