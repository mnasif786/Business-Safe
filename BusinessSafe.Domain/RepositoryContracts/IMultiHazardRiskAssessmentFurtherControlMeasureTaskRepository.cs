using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.RepositoryContracts
{
    public interface IMultiHazardRiskAssessmentFurtherControlMeasureTaskRepository : IRepository<MultiHazardRiskAssessmentFurtherControlMeasureTask, long>
    {
        MultiHazardRiskAssessmentFurtherControlMeasureTask GetByIdAndCompanyId(long id, long companyId);
    }
}