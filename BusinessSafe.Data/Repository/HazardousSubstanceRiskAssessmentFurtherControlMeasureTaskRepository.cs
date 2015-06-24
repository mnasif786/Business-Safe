using BusinessSafe.Data.Common;
using BusinessSafe.Data.CustomExceptions;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using NHibernate.Criterion;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository
{
    public class HazardousSubstanceRiskAssessmentFurtherControlMeasureTaskRepository : Repository<HazardousSubstanceRiskAssessmentFurtherControlMeasureTask, long>, IHazardousSubstanceRiskAssessmentFurtherControlMeasureTaskRepository
    {
        public HazardousSubstanceRiskAssessmentFurtherControlMeasureTaskRepository(IBusinessSafeSessionManager sessionManager) : base(sessionManager) { }

        public HazardousSubstanceRiskAssessmentFurtherControlMeasureTask GetById(long id, long companyId)
        {
            var result = SessionManager
                .Session
                .CreateCriteria<HazardousSubstanceRiskAssessmentFurtherControlMeasureTask>()
                .Add(Restrictions.Eq("Id", id))
                .Add(Restrictions.Eq("Deleted", false))
                .SetMaxResults(1)
                .UniqueResult<HazardousSubstanceRiskAssessmentFurtherControlMeasureTask>();

            if (result == null)
                throw new FurtherControlMeasureNotFoundException(id);

            return result;
        }    
    }
}