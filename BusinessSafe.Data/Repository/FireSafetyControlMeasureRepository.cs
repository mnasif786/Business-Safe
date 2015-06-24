using System.Collections.Generic;
using BusinessSafe.Data.Common;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using NHibernate.Criterion;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository
{
    public class FireSafetyControlMeasureRepository : Repository<FireSafetyControlMeasure, long>, IFireSafetyControlMeasureRepository
    {
        public FireSafetyControlMeasureRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        {
        }

        public IEnumerable<FireSafetyControlMeasure> GetByCompanyId(long companyId)
        {
            return SessionManager
                .Session
                .CreateCriteria<FireSafetyControlMeasure>()
                .Add(Restrictions.Or(Restrictions.Eq("CompanyId", companyId), Restrictions.IsNull("CompanyId")))
                .Add(Restrictions.Eq("Deleted", false))
                .Add(Restrictions.IsNull("RiskAssessmentId"))
                .SetMaxResults(500)
                .List<FireSafetyControlMeasure>();
        }

        public IList<FireSafetyControlMeasure> GetAllFireSafetyControlMeasureForRiskAssessments(long companyId, long riskAssessmentId)
        {
            return SessionManager
                    .Session
                    .CreateCriteria<FireSafetyControlMeasure>()
                    .Add(Restrictions.Or(Restrictions.Eq("CompanyId", companyId), Restrictions.IsNull("CompanyId")))
                    .Add(Restrictions.Eq("Deleted", false))
                    .Add(Restrictions.Or(Restrictions.Eq("RiskAssessmentId", riskAssessmentId), Restrictions.IsNull("RiskAssessmentId")))
                    .SetMaxResults(500)
                    .List<FireSafetyControlMeasure>();
        }
    }
}