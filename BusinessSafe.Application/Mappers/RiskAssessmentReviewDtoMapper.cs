using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class RiskAssessmentReviewDtoMapper
    {
        public IEnumerable<RiskAssessmentReviewDto> Map(IEnumerable<RiskAssessmentReview> riskAssessmentReviews)
        {
            return riskAssessmentReviews.Select(Map).ToList();
        }

        public RiskAssessmentReviewDto Map(RiskAssessmentReview entity)
        {
            return new RiskAssessmentReviewDto()
                       {
                           RiskAssessment = new RiskAssessmentDtoMapper().MapWithEmployeesAndNonEmployeesAndSiteAndRiskAssessor(entity.RiskAssessment),
                           Comments = entity.Comments,
                           CompletedBy = entity.CompletedBy != null ? new EmployeeDtoMapper().MapWithNationalityAndContactDetailsAndEmergencyContactDetails(entity.CompletedBy) : null,
                           CompletedDate = entity.CompletedDate,
                           CompletionDueDate = entity.CompletionDueDate,
                           Id = entity.Id,
                           IsReviewOutstanding = entity.IsReviewOutstanding,
                           ReviewAssignedTo = entity.ReviewAssignedTo != null ? new EmployeeDtoMapper().MapWithUser(entity.ReviewAssignedTo) : null,
                           RiskAssessmentReviewTask = entity.RiskAssessmentReviewTask != null ? new TaskDtoMapper().MapWithAssignedTo(entity.RiskAssessmentReviewTask) as RiskAssessmentReviewTaskDto : null
                       };
        }
    }
}