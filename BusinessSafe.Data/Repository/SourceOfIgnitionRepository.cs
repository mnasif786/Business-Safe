using System.Collections.Generic;
using BusinessSafe.Data.Common;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using NHibernate.Criterion;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository
{
    public class SourceOfIgnitionRepository : Repository<SourceOfIgnition, long>, ISourceOfIgnitionRepository
    {
        public SourceOfIgnitionRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        {
        }

        public IEnumerable<SourceOfIgnition> GetByCompanyId(long companyId)
        {
            return SessionManager
                .Session
                .CreateCriteria<SourceOfIgnition>()
                .Add(Restrictions.Or(Restrictions.Eq("CompanyId", companyId), Restrictions.IsNull("CompanyId")))
                .Add(Restrictions.Eq("Deleted", false))
                .Add(Restrictions.IsNull("RiskAssessmentId"))
                .SetMaxResults(500)
                .List<SourceOfIgnition>();
        }

        public IList<SourceOfIgnition> GetAllSourceOfIgnitionForRiskAssessments(long companyId, long riskAssessmentId)
        {
            return SessionManager
                    .Session
                    .CreateCriteria<SourceOfIgnition>()
                    .Add(Restrictions.Or(Restrictions.Eq("CompanyId", companyId), Restrictions.IsNull("CompanyId")))
                    .Add(Restrictions.Eq("Deleted", false))
                    .Add(Restrictions.Or(Restrictions.Eq("RiskAssessmentId", riskAssessmentId), Restrictions.IsNull("RiskAssessmentId")))
                    .SetMaxResults(500)
                    .List<SourceOfIgnition>();
        }
    }
}