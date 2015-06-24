using System;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.RepositoryContracts
{
    public interface IEmployeeChecklistEmailRepository : IRepository<EmployeeChecklistEmail, Guid>
    { }
}