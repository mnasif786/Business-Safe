using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.RepositoryContracts
{
    public interface IRiskAssessmentRepository : IRepository<RiskAssessment, long>
    {
        RiskAssessment GetByIdAndCompanyId(long riskAssessmentId, long companyId);
        RiskAssessment GetByIdAndCompanyIdIncludeDeleted(long riskAssessmentId, long companyId);
        bool DoesAssessmentExistWithTheSameReference<T>(long companyId, string reference, long? riskAssessmentId = null) where T : class;
        bool DoesAssessmentExistWithTheSameTitle<T>(long clientId, string title, long? riskAssessmentId) where T : class;
        bool DoesAssessmentExistForRiskAssessor(long riskAssessorId, long companyId);
    }
}