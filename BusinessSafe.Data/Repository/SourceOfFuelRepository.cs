using System.Collections.Generic;
using BusinessSafe.Data.Common;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using NHibernate.Criterion;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository
{
    public class SourceOfFuelRepository : Repository<SourceOfFuel, long>, ISourceOfFuelRepository
    {
        public SourceOfFuelRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        {
        }

        public IEnumerable<SourceOfFuel> GetByCompanyId(long companyId)
        {
            return SessionManager
                .Session
                .CreateCriteria<SourceOfFuel>()
                .Add(Restrictions.Or(Restrictions.Eq("CompanyId", companyId), Restrictions.IsNull("CompanyId")))
                .Add(Restrictions.Eq("Deleted", false))
                .Add(Restrictions.IsNull("RiskAssessmentId"))
                .SetMaxResults(500)
                .List<SourceOfFuel>();
        }

        public IList<SourceOfFuel> GetAllSourceOfFuelForRiskAssessments(long companyId, long riskAssessmentId)
        {
            return SessionManager
                    .Session
                    .CreateCriteria<SourceOfFuel>()
                    .Add(Restrictions.Or(Restrictions.Eq("CompanyId", companyId), Restrictions.IsNull("CompanyId")))
                    .Add(Restrictions.Eq("Deleted", false))
                    .Add(Restrictions.Or(Restrictions.Eq("RiskAssessmentId", riskAssessmentId), Restrictions.IsNull("RiskAssessmentId")))
                    .SetMaxResults(500)
                    .List<SourceOfFuel>();
        }
    }
}