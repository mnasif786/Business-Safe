using System;
using System.Collections.Generic;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Response;
using BusinessSafe.Domain.Entities;
using FluentValidation.Results;

namespace BusinessSafe.Application.Contracts.Employee
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDto> GetAll(long companyId);
        EmployeeDto GetEmployee(Guid employeeId, long companyId);
        UpdateEmployeeResponse Update(UpdateEmployeeRequest request);
        AddEmployeeResponse Add(AddEmployeeRequest request);
        List<EmployeeDto> Search(SearchEmployeesRequest request);
        void MarkEmployeeAsDeleted(MarkEmployeeAsDeletedRequest request);
        void ReinstateEmployeeAsNotDeleted(ReinstateEmployeeAsNotDeleteRequest request);
        ValidationResult ValidateRegisterAsUser(CreateEmployeeAsUserRequest request);
        void CreateUser(CreateEmployeeAsUserRequest request);
        IEnumerable<EmployeeDto> GetEmployeesForSearchTerm(string searchTerm, long companyId, int pageLimit);
        void UpdateEmailAddress(UpdateEmployeeEmailAddressRequest request);
        void UpdateOnlineRegistrationDetails(UpdateOnlineRegistrationDetailsRequest request);
        List<EmployeeName> GetEmployeeNames(long companyId);
    }
}