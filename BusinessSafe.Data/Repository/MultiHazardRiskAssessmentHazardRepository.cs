using BusinessSafe.Data.Common;
using BusinessSafe.Data.CustomExceptions;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using NHibernate.Criterion;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository
{
    public class MultiHazardRiskAssessmentHazardRepository : Repository<MultiHazardRiskAssessmentHazard, long>, IMultiHazardRiskAssessmentHazardRepository
    {
        public MultiHazardRiskAssessmentHazardRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        {
        }

        public MultiHazardRiskAssessmentHazard GetByIdAndCompanyId(long id, long companyId)
        {
            var result = SessionManager
                .Session
                .CreateCriteria<MultiHazardRiskAssessmentHazard>()
                .Add(Restrictions.Eq("Id", id))
                .Add(Restrictions.Eq("Deleted", false))
                .CreateCriteria("MultiHazardRiskAssessment", "riskAssessment")
                .Add(Restrictions.Eq("riskAssessment.CompanyId", companyId))
                .SetMaxResults(1)
                .UniqueResult<MultiHazardRiskAssessmentHazard>();

            if(result == null)
                throw new RiskAssessmentHazardNotFoundException(id);

            return result;
        }

        public MultiHazardRiskAssessmentHazard GetByRiskAssessmentIdAndHazardIdAndCompanyId(long riskAssessmentId, long hazardId, long companyId)
        {
            var result = SessionManager
                .Session
                .CreateCriteria<MultiHazardRiskAssessmentHazard>()
                .Add(Restrictions.Eq("Deleted", false))
                .CreateAlias("MultiHazardRiskAssessment", "riskAssessment")
                .Add(Restrictions.Eq("riskAssessment.Id", riskAssessmentId))
                .Add(Restrictions.Eq("riskAssessment.CompanyId", companyId))
                .CreateAlias("Hazard", "hazard")
                .Add(Restrictions.Eq("hazard.Id", hazardId))
                .SetMaxResults(1)
                .UniqueResult<MultiHazardRiskAssessmentHazard>();

            return result;
        }
    }
}