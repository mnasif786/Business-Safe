using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Data.Common;
using BusinessSafe.Domain.Entities.SafeCheck;
using BusinessSafe.Domain.RepositoryContracts.SafeCheck;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using NHibernate.Linq;

namespace BusinessSafe.Data.Repository.SafeCheck
{
    public class FavouriteChecklistRepository : Repository<FavouriteChecklist, Guid>, IFavouriteChecklistRepository
    {
        public FavouriteChecklistRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        {
        }

        public FavouriteChecklist Get(Guid checklistId, string user)
        {
            var query = SessionManager.Session.Query<FavouriteChecklist>();
            query = query.Where(q => q.Checklist.Id == checklistId && q.MarkedByUser == user && !q.Deleted);
            return query.FirstOrDefault();

        }

        public FavouriteChecklist Get(Guid checklistId, string user, bool deleted)
        {
            var query = SessionManager.Session.Query<FavouriteChecklist>();
            query = query.Where(q => q.Checklist.Id == checklistId && q.MarkedByUser == user && q.Deleted == deleted);
            return query.FirstOrDefault();

        }

        public IEnumerable<FavouriteChecklist> GetByUser(string user, bool deleted)
        {
            var query = SessionManager.Session.Query<FavouriteChecklist>();
            query = query.Where(q => q.MarkedByUser == user && q.Deleted == deleted);
            return query;

        }
        
    }
}
