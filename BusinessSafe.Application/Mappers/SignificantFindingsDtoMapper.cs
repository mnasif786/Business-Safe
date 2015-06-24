using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class SignificantFindingsDtoMapper
    {
        public SignificantFindingDto Map(SignificantFinding entity)
        {
            return new SignificantFindingDto()
                       {
                           Id = entity.Id,
                           FireAnswer = new FireAnswerDtoMapper().Map(entity.FireAnswer),
                           FurtherActionTasks = GetRiskAssessmentFurtherActionTasks(entity)
                       };
        }

        public IEnumerable<SignificantFindingDto> Map(IList<SignificantFinding> entities)
        {
            return entities.Select(x => Map(x));
        }

        private static IEnumerable<TaskDto> GetRiskAssessmentFurtherActionTasks(SignificantFinding riskAssessmentHazard)
        {
            return new TaskDtoMapper().MapWithAssignedTo(riskAssessmentHazard.FurtherControlMeasureTasks);
        }
    }
}