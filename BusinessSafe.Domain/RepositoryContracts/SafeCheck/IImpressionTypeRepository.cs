using System;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities.SafeCheck;

namespace BusinessSafe.Domain.RepositoryContracts.SafeCheck
{
    public interface IImpressionTypeRepository : IRepository<ImpressionType, Guid>
    {
    }
}
