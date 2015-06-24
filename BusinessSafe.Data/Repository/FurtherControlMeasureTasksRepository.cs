using System;
using System.Linq;
using BusinessSafe.Data.Common;
using BusinessSafe.Data.CustomExceptions;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using NHibernate.Criterion;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using NHibernate.Linq;

namespace BusinessSafe.Data.Repository
{
    public class FurtherControlMeasureTasksRepository : Repository<FurtherControlMeasureTask, long>, IFurtherControlMeasureTasksRepository
    {
        public FurtherControlMeasureTasksRepository(IBusinessSafeSessionManager sessionManager) : base(sessionManager) { }

        public new FurtherControlMeasureTask GetById(long id)
        {
            var result = SessionManager
                .Session
                .CreateCriteria<FurtherControlMeasureTask>()
                .Add(Restrictions.Eq("Id", id))
                .Add(Restrictions.Eq("Deleted", false))
                .SetMaxResults(1)
                .UniqueResult<FurtherControlMeasureTask>();

            if (result == null)
                throw new TaskNotFoundException(id);

            return result;
        }

        public FurtherControlMeasureTask GetByIdIncludeDeleted(long id)
        {
            var result = SessionManager
                .Session
                .CreateCriteria<FurtherControlMeasureTask>()
                .Add(Restrictions.Eq("Id", id))
                .SetMaxResults(1)
                .UniqueResult<FurtherControlMeasureTask>();

            if (result == null)
                throw new TaskNotFoundException(id);

            return result;
        }

        public FurtherControlMeasureTask GetByIdAndCompanyId(long id, long companyId)
        {
            var query = SessionManager.Session.Query<FurtherControlMeasureTask>()
                .Where(x => x.Id == id)
                .Where(x => x.Deleted == false)
                .Where(x => x.TaskAssignedTo.CompanyId == companyId);

            var result = query.ToList();

            if (!result.Any())
            {
                throw new TaskNotFoundException(id);
            }

            return result.First();
        }
    }
}