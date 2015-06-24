using System;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.RepositoryContracts
{
    public interface IUserForAuditingRepository : IRepository<UserForAuditing, Guid>
    {
        UserForAuditing GetByIdAndCompanyId(Guid userId, long companyId);
        UserForAuditing GetSystemUser();
    }
}