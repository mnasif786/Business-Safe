using System;
using System.Collections.Generic;
using BusinessSafe.Data.Common;
using BusinessSafe.Data.CustomExceptions;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using NHibernate.Criterion;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository
{
    public class RoleRepository : Repository<Role, Guid>, IRoleRepository
    {
        public RoleRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        {
        }

        public IEnumerable<Role> GetAllByCompanyId(long companyId)
        {
            return SessionManager
                            .Session
                            .CreateCriteria<Role>()
                            .Add(Restrictions.Or(Restrictions.Eq("CompanyId", companyId), Restrictions.Eq("CompanyId",(long) 0)))
                            .Add(Restrictions.Eq("Deleted", false))
                            .SetMaxResults(200)
                            .List<Role>();
        }

        public Role GetByIdAndCompanyId(Guid roleId, long companyId)
        {
            var result = SessionManager
                            .Session
                            .CreateCriteria<Role>()
                            .Add(Restrictions.Eq("Id", roleId))
                            .Add(Restrictions.Or(Restrictions.Eq("CompanyId", companyId), Restrictions.Eq("CompanyId", (long)0)))
                            .Add(Restrictions.Eq("Deleted", false))
                            .SetMaxResults(1)
                            .UniqueResult<Role>();

            if (result == null)
                throw new RoleNotFoundException(roleId);

            return result;
		}
        public Role GetAdminRole()
        {
            return GetById(new Guid("BACF7C01-D210-4DBC-942F-15D8456D3B92"));
        }
    }
}