using System.Collections.Generic;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.RepositoryContracts
{
    public interface IRiskAssessmentReviewRepository : IRepository<RiskAssessmentReview, long>
    {
        IEnumerable<RiskAssessmentReview> Search(long riskAssessmentId);
        RiskAssessmentReview GetByIdAndCompanyId(long id, long companyId);
    }
}