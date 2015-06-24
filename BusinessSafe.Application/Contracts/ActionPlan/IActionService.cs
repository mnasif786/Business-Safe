using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Contracts.ActionPlan
{
    public interface IActionService
    {
        int Count(SearchActionRequest request);
        IEnumerable<ActionDto> Search(SearchActionRequest request);
        void AssignActionTask(AssignActionTaskRequest request);
        void SendTaskAssignedEmail(long actionId, long companyId);
        ActionDto GetByIdAndCompanyId(long actionId, long companyId);
    }
}
