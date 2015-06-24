using System.Collections.Generic;
using BusinessSafe.Data.Common;
using BusinessSafe.Data.CustomExceptions;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using NHibernate;
using NHibernate.Criterion;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository
{
    public class PeopleAtRiskRepository : Repository<PeopleAtRisk, long>, IPeopleAtRiskRepository
    {
        public PeopleAtRiskRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        {}

        public IList<PeopleAtRisk> GetByCompanyId(long companyId)
        {
            return SessionManager
                            .Session
                            .CreateCriteria<PeopleAtRisk>()
                            .Add(Restrictions.Or(Restrictions.Eq("CompanyId", companyId), Restrictions.IsNull("CompanyId")))
                            .Add(Restrictions.Eq("Deleted", false))
                            .Add(Restrictions.IsNull("RiskAssessmentId"))
                            .SetMaxResults(200)
                            .List<PeopleAtRisk>();
        }

        public IEnumerable<PeopleAtRisk> GetAllByNameSearch(string nameToSearch, long personAtRiskId, long companyId)
        {
            ICriteria executableCriteria = CompanyDefaultsRepositoryCriteriaHelper
                                            .GetCompanyDefaultExistsDetachedCriteria<PeopleAtRisk>(personAtRiskId, companyId, nameToSearch)
                                            .GetExecutableCriteria(SessionManager.Session);
            return executableCriteria.List<PeopleAtRisk>();
        }
        
        public IList<PeopleAtRisk> GetAllPeopleAtRiskForRiskAssessments(long companyId, long riskAssessmentId)
        {
            return SessionManager
                    .Session
                    .CreateCriteria<PeopleAtRisk>()
                    .Add(Restrictions.Or(Restrictions.Eq("CompanyId", companyId), Restrictions.IsNull("CompanyId")))
                    .Add(Restrictions.Eq("Deleted", false))
                    .Add(Restrictions.Or(Restrictions.Eq("RiskAssessmentId", riskAssessmentId), Restrictions.IsNull("RiskAssessmentId")))
                    .SetMaxResults(500)
                    .List<PeopleAtRisk>();
        }

        public PeopleAtRisk GetByIdAndCompanyId(long id, long companyId)
        {
         var result = SessionManager
                    .Session
                    .CreateCriteria<PeopleAtRisk>()
                    .Add(Restrictions.Eq("Id", id))
                    .Add(Restrictions.Or(Restrictions.Eq("CompanyId", companyId), Restrictions.IsNull("CompanyId")))
                    .Add(Restrictions.Eq("Deleted", false))
                    .SetMaxResults(200)
                    .UniqueResult<PeopleAtRisk>();

            if(result == null)
                throw new PersonAtRiskNotFoundException(id);
            return result;
        }
    }
}

