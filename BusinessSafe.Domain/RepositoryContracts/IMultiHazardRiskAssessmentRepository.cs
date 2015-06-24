using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.RepositoryContracts
{
    public interface IMultiHazardRiskAssessmentRepository : IRepository<MultiHazardRiskAssessment, long>
    {
        bool IsHazardAttachedToRiskAssessments(long hazardId, long companyId);
        MultiHazardRiskAssessment GetByIdAndCompanyId(long riskAssessmentId, long companyId);
    }
}