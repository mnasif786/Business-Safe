using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.RepositoryContracts
{
    public interface IEmploymentStatusRepository : IRepository<EmploymentStatus, int>
    { }
}