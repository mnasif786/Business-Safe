using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class RiskAssessmentReviewTaskDtoMapper
    {
        public RiskAssessmentReviewTaskDto MapWithAssignedToAndReview(RiskAssessmentReviewTask entity)
        {
            var dto = new TaskDtoMapper().MapWithAssignedTo(entity) as RiskAssessmentReviewTaskDto;

            dto.RiskAssessmentReview =
                new RiskAssessmentReviewDtoMapper().Map(entity.RiskAssessmentReview);

            return dto;
        }
    }
}
