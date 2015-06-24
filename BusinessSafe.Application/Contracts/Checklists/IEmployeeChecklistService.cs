using System;
using System.Collections.Generic;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request.Checklists;
using BusinessSafe.Domain.Common;

namespace BusinessSafe.Application.Contracts.Checklists
{
    public interface IEmployeeChecklistService
    {
        EmployeeChecklistDto GetById(Guid id);
        EmployeeChecklistDto GetWithCompletedOnEmployeesBehalfBy(Guid id);
        IEnumerable<EmployeeChecklistDto> GetByPersonalRiskAssessmentId(long riskAssessmentId);
        bool UserIsAuthentic(AuthenticateUserRequest request);
        void Save(SaveEmployeeChecklistRequest request);
        ValidationMessageCollection ValidateComplete(CompleteEmployeeChecklistRequest request);
        void Complete(CompleteEmployeeChecklistRequest request);

        void ToggleFurtherActionRequired(Guid employeeChecklistId, bool isRequired, Guid assessingUserId);
    }
}
