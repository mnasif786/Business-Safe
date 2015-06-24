using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.RepositoryContracts
{
    public interface IMultiHazardRiskAssessmentHazardRepository : IRepository<MultiHazardRiskAssessmentHazard, long>
    {
        MultiHazardRiskAssessmentHazard GetByIdAndCompanyId(long riskAssessmentId, long companyId);

        MultiHazardRiskAssessmentHazard GetByRiskAssessmentIdAndHazardIdAndCompanyId(long riskAssessmentId,
                                                                                     long hazardId, long companyId);
    }
 }