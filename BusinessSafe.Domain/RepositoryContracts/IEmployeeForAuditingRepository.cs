using System;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.RepositoryContracts
{
    public interface IEmployeeForAuditingRepository : IRepository<EmployeeForAuditing, Guid>
    {
        EmployeeForAuditing GetByIdAndCompanyId(Guid employeeForAuditingId, long companyId);
        EmployeeForAuditing GetByIdAndCompanyIdWithoutChecking(Guid employeeForAuditingId, long companyId);
    }
}