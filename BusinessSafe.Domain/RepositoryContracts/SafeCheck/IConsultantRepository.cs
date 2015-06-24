using System;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities.SafeCheck;

namespace BusinessSafe.Domain.RepositoryContracts.SafeCheck
{
    public interface IConsultantRepository : IRepository<Consultant, Guid>
    {
        Consultant GetByFullname(string fullName);
        Consultant GetByUsername(string username,bool includeDeleted);
    }
}
