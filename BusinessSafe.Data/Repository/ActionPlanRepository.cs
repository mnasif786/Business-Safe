using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Data.Common;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using NHibernate.Criterion;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using NHibernate.Linq;

namespace BusinessSafe.Data.Repository
{
    public class ActionPlanRepository : Repository<ActionPlan, long>, IActionPlanRepository
    {
        public ActionPlanRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        {
        }

        public int Count(IList<long> allowedSiteIds, long companyId, long? siteGroupId, long? siteId, DateTime? submittedFrom, DateTime? submittedTo, bool showArchived)
        {
            var query = SessionManager.Session.Query<ActionPlan>();

            query = CreateQuery(allowedSiteIds, companyId, siteGroupId, siteId, submittedFrom, submittedTo, showArchived, query);

            return query.Count();
        }

        public ActionPlan GetByIdAndCompanyId(long actionPlanId, long companyId)
        {
            var query = SessionManager.Session.Query<ActionPlan>();

            query = query.Where(x => x.Id == actionPlanId && x.CompanyId == companyId);

            return query.FirstOrDefault();
        }

        public IEnumerable<ActionPlan> Search(IList<long> allowedSiteIds, long companyId, long? siteGroupId, long? siteId, DateTime? submittedFrom,
            DateTime? submittedTo, bool showArchived, int page, int pageSize,
            ActionPlanOrderByColumn orderBy, bool @ascending)
        {
            var query = SessionManager.Session.Query<ActionPlan>();

            query = CreateQuery(allowedSiteIds, companyId, siteGroupId, siteId, submittedFrom, submittedTo, showArchived, query);

            query = AddOrder(orderBy, @ascending, query);

            return query.Skip(pageSize*(page - 1)).Take(pageSize).ToList();
        }


        private IQueryable<ActionPlan> CreateQuery(IList<long> allowedSiteIds, long companyId, long? siteGroupId, long? siteId,
            DateTime? submittedFrom, DateTime? submittedTo,
            bool showArchived,
            IQueryable<ActionPlan> query)
        {
            query = query.Where(x => x.CompanyId == companyId && !x.Deleted);

            if (allowedSiteIds != null && allowedSiteIds.Count > 0)
            {
                query = query.Where(x => x.Site == null != allowedSiteIds.Contains(x.Site.Id));
            }

            if (siteGroupId.HasValue)
            {
                var siteGroup = SessionManager
                    .Session
                    .CreateCriteria<SiteGroup>()
                    .Add(Restrictions.Eq("Id", siteGroupId.Value))
                    .UniqueResult<SiteGroup>();

                var siteIdsDescendingFromGroup = siteGroup.GetThisAndAllDescendants().Select(x => x.Id);
                query = query.Where(x => siteIdsDescendingFromGroup.ToArray().Contains(x.Site.Id));
            }

            if (siteId.HasValue)
            {
                query = query.Where(x => x.Site.Id == siteId.Value);
            }

            if (submittedFrom.HasValue)
            {
                query = query.Where(x => x.SubmittedOn >= submittedFrom.Value);
            }

            if (submittedTo.HasValue)
            {
                query = query.Where(x => x.SubmittedOn <= submittedTo.Value);
            }

            query = query.Where(x => x.NoLongerRequired == showArchived);



            return query;
        }

        private static IQueryable<ActionPlan> AddOrder(ActionPlanOrderByColumn orderBy, bool @ascending, IQueryable<ActionPlan> query)
        {
            switch (orderBy)
            {
                case ActionPlanOrderByColumn.None:
                    break;
                case ActionPlanOrderByColumn.Title:
                    query = @ascending ? query.OrderBy(x => x.Title) : query.OrderByDescending(x => x.Title);
                    break;
                case ActionPlanOrderByColumn.Site:
                    query = @ascending ? query.OrderBy(x => x.Site.Name) : query.OrderByDescending(x => x.Site.Name);
                    break;
                case ActionPlanOrderByColumn.DateOfVisit:
                    query = @ascending ? query.OrderBy(x => x.DateOfVisit) : query.OrderByDescending(x => x.DateOfVisit);
                    break;
                case ActionPlanOrderByColumn.VisitBy:
                    query = @ascending ? query.OrderBy(x => x.VisitBy) : query.OrderByDescending(x => x.VisitBy);
                    break;
                case ActionPlanOrderByColumn.SubmittedOn:
                    query = @ascending ? query.OrderBy(x => x.SubmittedOn) : query.OrderByDescending(x => x.SubmittedOn);
                    break;
                default:
                    query = query.OrderByDescending(x => x.SubmittedOn);
                    break;
            }
            return query;
        }

        private void SelectActionPlanIndexItem(IEnumerable<ActionPlan> actionPlan)
        {
            //THIS IS AN OPTIMISED VERSION OF THE ACTION PLAN QUERY. Partly finished because its taking too long to do as a refactor as part of another ticket.
            //However it is work that is scheduled to be done. ALP
            var t = actionPlan.Select(x => new ActionPlanIndex
            {
                Title = x.Title
                ,
                SiteName = x.Site != null ? x.Site.Name : ""
                ,
                DateOfVisit = x.DateOfVisit
                ,
                VisitBy = x.VisitBy
                ,
                SubmittedOn = x.SubmittedOn
                ,
                AnyActionsOverdue = x.Actions
                    .Where(action => action.Deleted == false && action.NoLongerRequired == false)
                    .SelectMany(action => action.ActionTasks)
                    .Any(task => task.Deleted == false && task.TaskStatus == TaskStatus.Outstanding &&
                                 task.TaskCompletionDueDate != null && task.TaskCompletionDueDate.Value.Date < DateTime.Today)
                , AnyActionsOutstanding = x.Actions
                    .SelectMany(action => action.ActionTasks)
                    .Any(task => task.Deleted == false && task.TaskStatus == TaskStatus.Outstanding &&
                                 task.TaskCompletionDueDate != null && task.TaskCompletionDueDate.Value.Date >= DateTime.Today)
                ,
                AnyActionsCompleted = x.Actions
                    .SelectMany(action => action.ActionTasks)
                    .Any(task => task.Deleted == false && task.TaskStatus == TaskStatus.Completed)
                , AnyActionsNoLongerRequired = x.Actions
                    .SelectMany(action => action.ActionTasks)
                    .Any(task => task.Deleted == false && task.TaskStatus == TaskStatus.NoLongerRequired)
            });
        }
    }
}
