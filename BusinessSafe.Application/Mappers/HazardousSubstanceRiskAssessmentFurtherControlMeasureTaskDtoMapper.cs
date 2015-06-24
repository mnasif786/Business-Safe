using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class HazardousSubstanceRiskAssessmentFurtherControlMeasureTaskDtoMapper
    {
        public HazardousSubstanceRiskAssessmentFurtherControlMeasureTaskDto MapWithAssignedToAndRiskAssessment(HazardousSubstanceRiskAssessmentFurtherControlMeasureTask entity)
        {
            var dto = new TaskDtoMapper().MapWithAssignedTo(entity) as HazardousSubstanceRiskAssessmentFurtherControlMeasureTaskDto;

            dto.HazardousSubstanceRiskAssessment =
                new HazardousSubstanceRiskAssessmentDtoMapper().Map(
                    entity.HazardousSubstanceRiskAssessment);

            return dto;
        }
    }
}
