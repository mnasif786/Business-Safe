using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class FireRiskAssessmentFurtherControlMeasureTaskDtoMapper
    {
        public FireRiskAssessmentFurtherControlMeasureTaskDto MapWithAssignedToAndSignificantFinding(FireRiskAssessmentFurtherControlMeasureTask entity)
        {
            var dto = new TaskDtoMapper().MapWithAssignedTo(entity) as FireRiskAssessmentFurtherControlMeasureTaskDto;

            dto.SignificantFinding =
                    new SignificantFindingDtoMapper().Map(entity.SignificantFinding);

            return dto;
        }
    }
}
