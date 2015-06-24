using System;
using System.Collections.Generic;

using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.RepositoryContracts
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        IEnumerable<User> GetAllByCompanyId(long companyId);
        User GetByIdAndCompanyId(Guid userId, long companyId);
        IEnumerable<User> GetByIdsAndCompanyId(Guid[] ids, long companyId);
        User GetByIdAndCompanyIdIncludeDeleted(Guid userId, long companyId);
        User GetSystemUser();
        IEnumerable<User> Search(long companyId, string forenameLike, string surnameLike, IList<long> allowableSites, long siteId, bool showDeleted, int maximumResults);
        IEnumerable<User> GetAllByCompanyIdAndRole(long companyId, Guid roleId);
    }
}