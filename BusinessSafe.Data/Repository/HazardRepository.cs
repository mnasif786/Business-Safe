using System.Collections.Generic;
using BusinessSafe.Data.Common;
using BusinessSafe.Data.CustomExceptions;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository
{
    public class HazardRepository : Repository<Hazard, long>, IHazardRepository
    {
        public HazardRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        { }

        public IList<Hazard> GetByCompanyId(long companyId)
        {
            return SessionManager
                            .Session
                            .CreateCriteria<Hazard>()
                            .Add(Restrictions.Or(Restrictions.Eq("CompanyId", companyId), Restrictions.IsNull("CompanyId")))
                            .Add(Restrictions.IsNull("RiskAssessment"))
                            .Add(Restrictions.Eq("Deleted", false))
                            .SetMaxResults(200)
                            .List<Hazard>();
        }

        public IList<Hazard> GetAllByNameSearch(string nameToSearch, long hazardId, long companyId)
        {
            ICriteria executableCriteria = CompanyDefaultsRepositoryCriteriaHelper
                                            .GetCompanyDefaultExistsDetachedCriteria<Hazard>(hazardId, companyId, nameToSearch)
                                            .GetExecutableCriteria(SessionManager.Session);
            return  executableCriteria.List<Hazard>();
        }

        public Hazard GetByIdAndCompanyId(long id, long companyId)
        {
            var result = SessionManager
                            .Session
                            .CreateCriteria<Hazard>()
                            .Add(Restrictions.Or(Restrictions.Eq("CompanyId", companyId), Restrictions.IsNull("CompanyId")))
                            .Add(Restrictions.Eq("Id", id))
                            .Add(Restrictions.Eq("Deleted", false))
                            .SetMaxResults(1)
                            .UniqueResult<Hazard>();
            if(result == null)
                throw new HazardNotFoundException(id);
            return result;
        }

        public IList<Hazard> GetByCompanyIdAndHazardTypeId(long companyId, HazardTypeEnum hazardTypeEnum, long riskAssessmentId)
        {
            return SessionManager
                            .Session
                            .CreateCriteria<Hazard>("h")
                            .Add(Restrictions.Or(Restrictions.Eq("CompanyId", companyId), Restrictions.IsNull("CompanyId")))
                            .Add(Restrictions.Or(Restrictions.Eq("h.RiskAssessment.Id", riskAssessmentId), Restrictions.IsNull("h.RiskAssessment")))
                            .Add(Restrictions.Eq("Deleted", false))
                            .CreateAlias("h.HazardTypes", "ht", JoinType.InnerJoin)
                            .Add(Restrictions.Eq("ht.Id", (long)hazardTypeEnum))
                            .SetMaxResults(200)
                            .List<Hazard>();
        }
    }
}
