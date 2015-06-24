using System;
using System.Collections.Generic;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request.Checklists;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Contracts.Checklists
{
    public interface IEmployeeChecklistEmailService
    {
        IList<Guid> Generate(GenerateEmployeeChecklistEmailRequest request);
        IEnumerable<EmployeeChecklistEmailDto> GetByIds(IList<Guid> employeeChecklistEmailIds);
        EmployeeChecklistEmail Regenerate(ResendEmployeeChecklistEmailRequest resendEmployeeChecklistEmailRequest);
    }
}