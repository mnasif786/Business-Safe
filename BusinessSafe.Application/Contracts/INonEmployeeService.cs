using System.Collections.Generic;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Response;

namespace BusinessSafe.Application.Contracts
{
    public interface INonEmployeeService
    {
        IEnumerable<NonEmployeeDto> GetAllNonEmployeesForCompany(long companyId);
        CompanyDefaultSaveResponse SaveNonEmployee(SaveNonEmployeeRequest request);
        NonEmployeeDto GetNonEmployee(long nonEmployeeId, long companyId);
        void MarkNonEmployeeAsDeleted(MarkNonEmployeeAsDeletedRequest request);
    }
}