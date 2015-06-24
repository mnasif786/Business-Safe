using System.Collections.Generic;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;

namespace BusinessSafe.Application.Contracts.RiskAssessments
{
    public interface IRiskAssessmentReviewService
    {
        void Add(AddRiskAssessmentReviewRequest request);
        void Edit(EditRiskAssessmentReviewRequest request);
        void CompleteRiskAssessementReview(CompleteRiskAssessmentReviewRequest request);
        IEnumerable<RiskAssessmentReviewDto> Search(long riskAssessmentId);
        RiskAssessmentReviewDto GetByIdAndCompanyId(long id, long companyId);
    }
}