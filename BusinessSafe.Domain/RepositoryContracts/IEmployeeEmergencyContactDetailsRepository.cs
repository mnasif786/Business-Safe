using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.RepositoryContracts
{
    public interface IEmployeeEmergencyContactDetailsRepository : IRepository<EmployeeEmergencyContactDetail, int>
    {
        EmployeeEmergencyContactDetail GetByIdAndCompanyId(int id, long companyId);
    }
}