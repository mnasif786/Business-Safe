using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Data.CustomExceptions;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernate.Mapping;
using NHibernate.SqlCommand;
using NHibernate.Transform;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository
{
    public class PersonalRiskAssessmentRepository : Common.Repository<PersonalRiskAssessment, long>, IPersonalRiskAssessmentRepository
    {
        public PersonalRiskAssessmentRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        {
        }

        public PersonalRiskAssessment GetByIdAndCompanyId(long riskAssessmentId, long companyId, Guid currentUserId)
        {
            var query = SessionManager.Session.Query<PersonalRiskAssessment>()
                .Where(x => x.Id == riskAssessmentId && x.CompanyId == companyId);

            var result = query.ToList();

            if (!result.Any())
                throw new RiskAssessmentNotFoundException(riskAssessmentId);

            return query.First();
        }


        public int Count(string title, long clientId, IEnumerable<long> siteIds, DateTime? createdFrom,
                 DateTime? createdTo, long? siteGroupId, long? siteId, Guid currentUserId, bool showDeleted,
                 bool showArchived)
        {
            var employeeId = SessionManager
               .Session.Get<User>(currentUserId)
               .Employee.Id;

            var latestReviewIds = LatestReviewIds(clientId, showDeleted);

            var query = SessionManager.Session.Query<PersonalRiskAssessment>()
                .CanUserAccess(latestReviewIds, currentUserId, employeeId)
                .CreateQuery(SessionManager, title, clientId, siteIds, createdFrom, createdTo, siteGroupId, siteId, currentUserId, showDeleted, showArchived);

            return query.Count();
        }

        public IEnumerable<PersonalRiskAssessment> Search(string title, long clientId, IEnumerable<long> siteIds,
                                                          DateTime? createdFrom, DateTime? createdTo, long? siteGroupId,
                                                          long? siteId, Guid currentUserId, bool showDeleted,
                                                          bool showArchived, int page, int pageSize, RiskAssessmentOrderByColumn orderBy, OrderByDirection orderByDirection)
        {
            //We need to retrieve the employee id for the given user because  the sql produced from the linq query when checking the user id of the risk assessor
            //results in an inner join rather than a left join. The end result was that PRAs with riskAssessor == null were not returned. I tired changint the mapping files to force a left join but to no avail.
            var employeeId = SessionManager
                .Session.Get<User>(currentUserId)
                .Employee.Id;
            
            var latestReviewIds = LatestReviewIds(clientId, showDeleted);

            var query = PersonalRiskAssessments()
                .CanUserAccess(latestReviewIds, currentUserId, employeeId)
                .CreateQuery(SessionManager, title, clientId, siteIds, createdFrom, createdTo, siteGroupId, siteId, currentUserId, showDeleted, showArchived);

            query = query.AddOrder(orderBy, orderByDirection);

            return query
                .Skip(pageSize * (page - 1))
                .Take(pageSize)
                .ToList();

        }

        private long[] LatestReviewIds(long clientId, bool showDeleted)
        {
            var latestReviewIds = RiskAssessmentReviews()
                .Where(x => x.RiskAssessment.CompanyId == clientId)
                .Where(x => x.RiskAssessment.Deleted == showDeleted)
                .Where(x => !x.Deleted)
                .GroupBy(x => x.RiskAssessment.Id)
                .Select(x => x.Max(r => r.Id))
                .ToArray();
            return latestReviewIds;
        }

        public virtual IQueryable<PersonalRiskAssessment> PersonalRiskAssessments()
        {
            return SessionManager.Session.Query<PersonalRiskAssessment>()
                .Fetch(x => x.RiskAssessmentSite) //adding fetching so the entities are eager loaded. need to put the fetch before where clauses
                .Fetch(x => x.RiskAssessor);

        }

        public virtual IQueryable<RiskAssessmentReview> RiskAssessmentReviews()
        {
            return SessionManager.Session.Query<RiskAssessmentReview>();

        }

    }
}