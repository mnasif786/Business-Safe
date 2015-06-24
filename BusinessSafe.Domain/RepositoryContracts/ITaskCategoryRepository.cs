using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.RepositoryContracts
{
    public interface ITaskCategoryRepository : IRepository<TaskCategory, long>
    {
        TaskCategory GetFutherControlMeasureTaskCategoryById(long id);
        TaskCategory GetGeneralRiskAssessmentTaskCategory();
        TaskCategory GetHazardousSubstanceRiskAssessmentTaskCategory();
        TaskCategory GetPersonalRiskAssessmentTaskCategory();
        TaskCategory GetFireRiskAssessmentTaskCategory();
        TaskCategory GetResponsibilityTaskCategory();
        TaskCategory GetActionTaskCategory();
    }
}