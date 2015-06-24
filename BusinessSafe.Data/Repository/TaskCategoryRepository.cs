using BusinessSafe.Data.Common;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using NHibernate.Criterion;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository
{
    public class TaskCategoryRepository : Repository<TaskCategory, long>, ITaskCategoryRepository
    {
        public TaskCategoryRepository(IBusinessSafeSessionManager sessionManager) : base(sessionManager) { }

        public TaskCategory GetFutherControlMeasureTaskCategoryById(long id)
        {
            var result = SessionManager
                .Session
                .CreateCriteria<TaskCategory>()
                .Add(Restrictions.Eq("Id", id))
                .SetMaxResults(1)
                .UniqueResult<TaskCategory>();

            return result;
        }

        public TaskCategory GetGeneralRiskAssessmentTaskCategory()
        {
            return LoadById(3);
        }

        public TaskCategory GetHazardousSubstanceRiskAssessmentTaskCategory()
        {
            return LoadById(6);
        }

        public TaskCategory GetPersonalRiskAssessmentTaskCategory()
        {
            return LoadById(5);
        }

        public TaskCategory GetFireRiskAssessmentTaskCategory()
        {
            return LoadById(2);
        }

        public TaskCategory GetResponsibilityTaskCategory()
        {
            return LoadById(7);
        }

        public TaskCategory GetActionTaskCategory()
        {
            return LoadById(8);
        }
    }
}