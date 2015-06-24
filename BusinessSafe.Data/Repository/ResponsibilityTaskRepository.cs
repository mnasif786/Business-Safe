using System.Linq;
using BusinessSafe.Data.Common;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using NHibernate.Linq;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using System.Collections.Generic;
using NHibernate.Criterion;
using NHibernate.SqlCommand;

namespace BusinessSafe.Data.Repository
{
    public class ResponsibilityTaskRepository : Repository<ResponsibilityTask, long>, IResponsibilityTaskRepository
    {
        public ResponsibilityTaskRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        {
        }

        public ResponsibilityTask GetByIdAndCompanyId(long id, long companyId)
        {
            var query = SessionManager.Session.Query<ResponsibilityTask>()
                .Where(x => x.Id == id)
                .Where(x => x.Deleted == false)
                .Where(x => x.TaskAssignedTo.CompanyId == companyId);

            var result = query.ToList();

            return result.SingleOrDefault();
        }
    }
}
