using System;
using System.Collections.Generic;
using BusinessSafe.Data.CustomExceptions;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using BusinessSafe.Data.Common;
using NHibernate.Criterion;
using NHibernate.Linq;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using System.Linq;

namespace BusinessSafe.Data.Repository
{
    public class FireRiskAssessmentRepository : Repository<FireRiskAssessment, long>, IFireRiskAssessmentRepository
    {
        public FireRiskAssessmentRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        {
        }

        public FireRiskAssessment GetByIdAndCompanyId(long riskAssessmentId, long companyId)
        {
            var result = SessionManager
                .Session
                .CreateCriteria<FireRiskAssessment>()
                .Add(Restrictions.Eq("Id", riskAssessmentId))
                // Deleted Restrictions not applicable because need to view delete Risk Assessments
                .Add(Restrictions.Eq("CompanyId", companyId))
                .SetMaxResults(1)
                .UniqueResult<FireRiskAssessment>();

            if (result == null)
                throw new RiskAssessmentNotFoundException(riskAssessmentId);

            return result;
        }

        public IEnumerable<FireRiskAssessment> Search(string title, long companyId, IEnumerable<long> allowedSiteIds, DateTime? createdFrom, DateTime? createdTo,
                                                        long? siteGroupId, long? siteId, Guid currentUserId,
                                                        bool showDeleted, bool showArchived, int page, int pageSize, RiskAssessmentOrderByColumn orderBy, OrderByDirection orderByDirection)
        {
            var query = SessionManager.Session.Query<FireRiskAssessment>()
                .Fetch(x => x.RiskAssessmentSite) //adding fetching so the entities are eager loaded. need to put the fetch before where clauses
                .Fetch(x => x.RiskAssessor)
                .CreateQuery(SessionManager, title, companyId, allowedSiteIds, createdFrom, createdTo, siteGroupId, siteId, currentUserId, showDeleted, showArchived);

            query = query.AddOrder(orderBy, orderByDirection);

            return query
                .Skip(pageSize*(page - 1))
                .Take(pageSize)
                .ToList();

        }

        public int Count(string title, long companyId, IEnumerable<long> allowedSiteIds, DateTime? createdFrom,
                       DateTime? createdTo, long? siteGroupId, long? siteId, Guid currentUserId, bool showDeleted,
                       bool showArchived)
        {
            var query = SessionManager.Session.Query<FireRiskAssessment>().CreateQuery(SessionManager, title, companyId, allowedSiteIds, createdFrom, createdTo, siteGroupId, siteId, currentUserId, showDeleted, showArchived);

            return query.Count();
        }
      
    }
}
