using System;
using System.Linq;
using BusinessSafe.Data.Common;
using BusinessSafe.Domain.Entities.SafeCheck;
using BusinessSafe.Domain.RepositoryContracts.SafeCheck;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using NHibernate.Linq;

namespace BusinessSafe.Data.Repository.SafeCheck
{
    public class MaintenanceUserRepository : Repository<MaintenanceUser, Guid>, IMaintenanceUserRepository
    {
        public MaintenanceUserRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        {
        }

        //public MaintenanceUser GetByFullname(string fullName)
        //{
        //    if (string.IsNullOrEmpty(fullName))
        //        return null;

        //    var name = fullName.Split(' ');
        //    var query = SessionManager.Session.Query<MaintenanceUser>();
        //    query = query.Where(x => x.Forename == name[0]);

        //    if (name.Length > 1)
        //    {
        //        query = query.Where(x => x.Surname == name[1]);
        //    }

        //    return query.FirstOrDefault();
        //}

       public MaintenanceUser GetByUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                return null;
             
            var query = SessionManager.Session.Query<MaintenanceUser>();
            query = query.Where(x => x.Username == userName && x.Deleted == false);

            return query.FirstOrDefault();
        }
    }
}
