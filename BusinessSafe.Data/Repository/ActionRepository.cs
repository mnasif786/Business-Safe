using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using NHibernate.Linq;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using BusinessSafe.Data.Common;
using Action = BusinessSafe.Domain.Entities.Action;


namespace BusinessSafe.Data.Repository
{
    public class ActionRepository : Repository< BusinessSafe.Domain.Entities.Action, long>, IActionRepository
    {
        public ActionRepository(IBusinessSafeSessionManager sessionManager) : base(sessionManager)
        {
        }


        public int Count(long actionPlanId)
        {
            var query = SessionManager.Session.Query<BusinessSafe.Domain.Entities.Action>();
            
            query = query.Where(x => x.ActionPlanId == actionPlanId);
            
            return query.Count();
        }

        public IEnumerable<BusinessSafe.Domain.Entities.Action> Search(long actionPlanId, int page, int pageSize, ActionOrderByColumn orderBy, bool @ascending)
        {
            var query = SessionManager.Session.Query<BusinessSafe.Domain.Entities.Action>();

            query = query.Where(x => x.ActionPlanId == actionPlanId);
            
            query = AddOrder(orderBy, @ascending, query);

            return query.Skip(pageSize*(page - 1)).Take(pageSize).ToList();
        }

        public Action GetByIdAndCompanyId(long actionId, long companyId)
        {
            var query = SessionManager.Session.Query<BusinessSafe.Domain.Entities.Action>();
            query = query.Where(a => a.ActionPlan.CompanyId == companyId && a.Id == actionId);
            return query.FirstOrDefault();
        }

        private static IQueryable<BusinessSafe.Domain.Entities.Action> AddOrder(ActionOrderByColumn orderBy, bool @ascending, IQueryable<BusinessSafe.Domain.Entities.Action> query)
        {
            switch (orderBy)
            {
                case ActionOrderByColumn.None:
                    break;
                //case ActionPlanOrderByColumn.Title:
                //    query = @ascending ? query.OrderBy(x => x.Title) : query.OrderByDescending(x => x.Title);
                //    break;
                //case ActionPlanOrderByColumn.Site:
                //    query = @ascending ? query.OrderBy(x => x.Site.Name) : query.OrderByDescending(x => x.Site.Name);
                //    break;
                //case ActionPlanOrderByColumn.DateOfVisit:
                //    query = @ascending ? query.OrderBy(x => x.DateOfVisit) : query.OrderByDescending(x => x.DateOfVisit);
                //    break;
                //case ActionPlanOrderByColumn.VisitBy:
                //    query = @ascending ? query.OrderBy(x => x.VisitBy) : query.OrderByDescending(x => x.VisitBy);
                //    break;
                //case ActionPlanOrderByColumn.SubmittedOn:
                //    query = @ascending ? query.OrderBy(x => x.SubmittedOn) : query.OrderByDescending(x => x.SubmittedOn);
                //    break;
            }
            return query;
        } 
    }
}
