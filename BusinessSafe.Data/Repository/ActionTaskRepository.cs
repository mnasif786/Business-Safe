using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Data.Common;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using NHibernate.Linq;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository
{
    public class ActionTaskRepository :  Repository<ActionTask, long>, IActionTaskRepository
    {

        public ActionTaskRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        {
        }

        public ActionTask GetByIdAndCompanyId(long id, long companyId)
        {
            var query = SessionManager.Session.Query<ActionTask>()
                .Where(x => x.Id == id)
                .Where(x => x.Deleted == false)
                .Where(x => x.TaskAssignedTo.CompanyId == companyId);

            var result = query.ToList();

            return result.SingleOrDefault();
        }
    }
}
