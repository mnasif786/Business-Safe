using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Data.CustomExceptions;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using NHibernate.Transform;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using NHibernate.Linq;
using System.Linq;

namespace BusinessSafe.Data.Repository
{


    public class GeneralRiskAssessmentRepository : Common.Repository<GeneralRiskAssessment, long>, IGeneralRiskAssessmentRepository
    {
        public GeneralRiskAssessmentRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        {
        }

        public GeneralRiskAssessment GetByIdAndCompanyId(long riskAssessmentId, long companyId)
        {
            var result = SessionManager
                .Session
                .CreateCriteria<GeneralRiskAssessment>()
                .Add(Restrictions.Eq("Id", riskAssessmentId))
                .Add(Restrictions.Eq("CompanyId", companyId))
                // Deleted Restrictions not applicable because need to view delete Risk Assessments
                .SetMaxResults(1)
                .UniqueResult<GeneralRiskAssessment>();

            if (result == null)
                throw new RiskAssessmentNotFoundException(riskAssessmentId);

            return result;
        }

        public IEnumerable<GeneralRiskAssessment> Search(string title, long clientId, IEnumerable<long> siteIds,
                                                         DateTime? createdFrom, DateTime? createdTo, long? siteGroupId,
                                                         long? siteId, Guid currentUserId, bool showDeleted,
                                                         bool showArchived, int page, int pageSize
                                                         , RiskAssessmentOrderByColumn orderBy, OrderByDirection orderByDirection)
        {

            var query = SessionManager.Session.Query<GeneralRiskAssessment>()
                .Fetch(x => x.RiskAssessmentSite) //adding fetching so the entities are eager loaded. need to put the fetch before where clauses
                .Fetch(x => x.RiskAssessor)
                .CreateQuery(SessionManager, title, clientId, siteIds, createdFrom, createdTo, siteGroupId, siteId, currentUserId, showDeleted, showArchived);

            query = query.AddOrder(orderBy, orderByDirection);
            return query
                .Skip(pageSize*(page - 1))
                .Take(pageSize)
                .ToList();


        }


        public int Count(string title, long clientId, IEnumerable<long> siteIds, DateTime? createdFrom,
                         DateTime? createdTo, long? siteGroupId, long? siteId, Guid currentUserId, bool showDeleted,
                         bool showArchived)
        {
            var query = SessionManager.Session.Query<GeneralRiskAssessment>().CreateQuery(SessionManager, title, clientId, siteIds, createdFrom, createdTo, siteGroupId, siteId, currentUserId, showDeleted, showArchived);

            return query.Count();
        }

    }
}