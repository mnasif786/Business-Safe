using System;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities.SafeCheck;

namespace BusinessSafe.Domain.RepositoryContracts.SafeCheck
{
    public interface IMaintenanceUserRepository : IRepository<MaintenanceUser, Guid>
    {
        //MaintenanceUser GetByFullname(string fullName);
        MaintenanceUser GetByUserName(string userName);
    }
}
