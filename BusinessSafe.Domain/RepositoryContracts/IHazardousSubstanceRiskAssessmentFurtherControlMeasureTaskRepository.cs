using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.RepositoryContracts
{
    public interface IHazardousSubstanceRiskAssessmentFurtherControlMeasureTaskRepository : IRepository<HazardousSubstanceRiskAssessmentFurtherControlMeasureTask, long>
    {
        HazardousSubstanceRiskAssessmentFurtherControlMeasureTask GetById(long id);
    }
}