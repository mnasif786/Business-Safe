using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Data.Common;
using BusinessSafe.Domain.RepositoryContracts.SafeCheck;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernate.SqlCommand;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using Checklist = BusinessSafe.Domain.Entities.SafeCheck.Checklist;
using System.Linq;
namespace BusinessSafe.Data.Repository.SafeCheck
{
    public class ChecklistRepository : Repository<Checklist, Guid>, ICheckListRepository
    {       
        public ChecklistRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        {
        }

        public override Checklist GetById(Guid id)
        {
            var result = SessionManager.Session
                .CreateCriteria<Checklist>()
                .CreateAlias("Questions", "questions", JoinType.InnerJoin)
                .SetFetchMode("questions", FetchMode.Eager)
                .CreateAlias("questions.Question", "question", JoinType.InnerJoin)
                .SetFetchMode("question", FetchMode.Eager)
                .CreateAlias("question.PossibleResponses", "responses", JoinType.LeftOuterJoin)
                .SetFetchMode("responses", FetchMode.Eager)
                .Add(Restrictions.Eq("Id", id))
                .UniqueResult<Checklist>();
            return result;
        }

        public IEnumerable<Checklist> GetByClientId(long client)
        {
            return GetByClientId(client, true);
        }

        public IEnumerable<Checklist> GetByClientId(long client, bool includeDeleted)
        {
            var query = SessionManager.Session.Query<Checklist>()
                .Where(x => x.ClientId == client);

            if (!includeDeleted)
            {
                query = query.Where(x => !x.Deleted);
            }

            return query.ToList();
        }

        public IEnumerable<Checklist> Search(int? clientId, string createdBy, string visitDate, string status,
            bool includeDeleted, Guid? QaAdvisorId)
        {
            var query = SessionManager.Session.Query<Checklist>();
            query = query.FetchMany(x => x.Answers)
                .ThenFetch(x => x.Question);

            if (clientId.HasValue && clientId.Value > default(int))
            {
                query = query.Where(x => x.ClientId == clientId);
            }

            if (!string.IsNullOrEmpty(createdBy) && QaAdvisorId.HasValue)
            {
                query = query.Where(x => x.ChecklistCreatedBy == createdBy || x.QaAdvisor.Id == QaAdvisorId);
            }
            else if (!string.IsNullOrEmpty(createdBy))
            {
                query = query.Where(x => x.ChecklistCreatedBy == createdBy);
            }
            else if (QaAdvisorId.HasValue)
            {
                query = query.Where(x => x.QaAdvisor.Id == QaAdvisorId);
            }

            DateTime validVisitDate;
            if (!string.IsNullOrEmpty(visitDate) && DateTime.TryParse(visitDate, out validVisitDate))
            {
                query = query.Where(x => x.VisitDate != null && x.VisitDate.Value.Date == validVisitDate);
            }

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(x => x.Status == status);
            }

            query = !includeDeleted ? query.Where(x => !x.Deleted) : query.Where(x => x.Deleted);

            return query.ToList();
        }

        public IList<string> GetDistinctCreatedBy()
        {
            var criteria = SessionManager.Session.CreateCriteria<Checklist>();

            criteria.SetProjection(
                Projections.Distinct(Projections.ProjectionList()
                .Add(Projections.Alias(Projections.Property("ChecklistCreatedBy"), "ChecklistCreatedBy"))))
                .Add(Restrictions.Eq("Deleted",false));

            var result = criteria.List<string>();

            return result;
        }
    }
}