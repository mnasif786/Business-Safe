using System;
using System.Linq;
using BusinessSafe.Data.Common;
using BusinessSafe.Domain.Entities.SafeCheck;
using BusinessSafe.Domain.RepositoryContracts.SafeCheck;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using NHibernate.Linq;

namespace BusinessSafe.Data.Repository.SafeCheck
{
    public class ConsultantRepository : Repository<Consultant, Guid>, IConsultantRepository
    {
        public ConsultantRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        {
        }

        public Consultant GetByFullname(string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
                return null;

            var name = fullName.Split(' ');
            var query = SessionManager.Session.Query<Consultant>();
            query = query.Where(x => x.Forename == name[0]);

            if (name.Length > 1)
            {
                query = query.Where(x => x.Surname == name[1]);
            }

            return query.FirstOrDefault();
        }

        public Consultant GetByUsername(string username, bool includeDeleted)
        {
            if (string.IsNullOrEmpty(username))
                return null;

            var query = SessionManager.Session.Query<Consultant>();
            query = query.Where(x => x.Username == username);

            if (!includeDeleted)
            {
                query = query.Where(x => !x.Deleted);
            }

            return query.FirstOrDefault();

        }
    }
}
