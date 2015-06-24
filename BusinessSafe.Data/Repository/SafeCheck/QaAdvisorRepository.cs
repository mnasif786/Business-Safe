using System;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using BusinessSafe.Data.Common;
using BusinessSafe.Domain.Entities.SafeCheck;
using BusinessSafe.Domain.RepositoryContracts.SafeCheck;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using NHibernate.Linq;

namespace BusinessSafe.Data.Repository.SafeCheck
{
    public class QaAdvisorRepository : Repository<QaAdvisor, Guid>, IQaAdvisorRepository
    {
        public QaAdvisorRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        {
        }

        public QaAdvisor GetByFullname(string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
                return null;

            var name = fullName.Split(' ');
            var query = SessionManager.Session.Query<QaAdvisor>();
            query = query.Where(x => x.Forename == name[0]);

            if (name.Length > 1)
            {
                query = query.Where(x => x.Surname == name[1]);
            }

            return query.FirstOrDefault();
        }
    }
}
