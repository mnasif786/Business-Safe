using System.Collections.Generic;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;

namespace BusinessSafe.Application.Contracts.GeneralRiskAssessments
{
    public interface IRiskAssessmentLookupService
    {
        IEnumerable<NonEmployeeDto> SearchForNonEmployeesNotAttachedToRiskAssessment(NonEmployeesNotAttachedToRiskAssessmentSearchRequest request);
        IEnumerable<EmployeeDto> SearchForEmployeesNotAttachedToRiskAssessment(EmployeesNotAttachedToRiskAssessmentSearchRequest request);
    }
}