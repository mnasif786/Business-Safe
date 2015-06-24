using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;
using System.Collections.Generic;

namespace BusinessSafe.Domain.RepositoryContracts
{
    public interface IResponsibilityTaskRepository : IRepository<ResponsibilityTask, long>
    {
        ResponsibilityTask GetByIdAndCompanyId(long id, long companyId);
    }
}
