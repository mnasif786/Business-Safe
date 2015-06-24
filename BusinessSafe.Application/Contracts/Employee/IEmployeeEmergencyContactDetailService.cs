using BusinessSafe.Application.Request;
using BusinessSafe.Application.DataTransferObjects;

namespace BusinessSafe.Application.Contracts.Employee
{
    public interface IEmployeeEmergencyContactDetailService
    {
        void CreateEmergencyContactDetailForEmployee(CreateEmergencyContactRequest request);
        void UpdateEmergencyContactDetailForEmployee(UpdateEmergencyContactRequest updateEmergencyContactRequest);
        void MarkEmployeeEmergencyContactAsDeleted(MarkEmployeeEmergencyContactAsDeletedRequest markEmployeeEmergencyContactAsDeletedRequest);
        EmployeeEmergencyContactDetailDto GetByIdAndCompanyId(int id, long companyId);
    }
}