using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.RepositoryContracts
{
    public interface IFurtherControlMeasureTasksRepository : IRepository<FurtherControlMeasureTask, long>
    {
        FurtherControlMeasureTask GetByIdIncludeDeleted(long id);
        FurtherControlMeasureTask GetByIdAndCompanyId(long id, long companyId);
    }
}