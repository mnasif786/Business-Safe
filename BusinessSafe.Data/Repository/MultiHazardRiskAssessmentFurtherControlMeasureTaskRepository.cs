using BusinessSafe.Data.Common;
using BusinessSafe.Data.CustomExceptions;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using NHibernate.Criterion;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository
{
    public class MultiHazardRiskAssessmentFurtherControlMeasureTaskRepository : Repository<MultiHazardRiskAssessmentFurtherControlMeasureTask, long>, IMultiHazardRiskAssessmentFurtherControlMeasureTaskRepository
    {
        public MultiHazardRiskAssessmentFurtherControlMeasureTaskRepository(IBusinessSafeSessionManager sessionManager) : base(sessionManager) { }

        public MultiHazardRiskAssessmentFurtherControlMeasureTask GetByIdAndCompanyId(long id, long companyId)
        {
            var result = SessionManager
                .Session
                .CreateCriteria<MultiHazardRiskAssessmentFurtherControlMeasureTask>()
                .Add(Restrictions.Eq("Id", id))
                .CreateCriteria("MultiHazardRiskAssessmentHazard", "riskAssessmentHazard")
                .CreateCriteria("riskAssessmentHazard.GeneralRiskAssessment", "riskAssessment")
                .Add(Restrictions.Eq("riskAssessment.CompanyId", companyId))
                .SetMaxResults(1)
                .UniqueResult<MultiHazardRiskAssessmentFurtherControlMeasureTask>();

            if (result == null)
                throw new FurtherControlMeasureNotFoundException(id);

            return result;
        }
    }
}