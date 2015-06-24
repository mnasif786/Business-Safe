using System;
using System.Collections.Generic;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.RepositoryContracts
{
    public interface IRoleRepository : IRepository<Role, Guid>
    {
        IEnumerable<Role> GetAllByCompanyId(long companyId);
        Role GetByIdAndCompanyId(Guid roleId, long companyId);
		Role GetAdminRole();
    }
}