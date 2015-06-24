using System.Collections.Generic;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;

namespace BusinessSafe.Application.Contracts.ActionPlan
{
    public interface IActionPlanService
    {
        int Count(SearchActionPlanRequest searchActionPlanRequest);

        ActionPlanDto GetByIdAndCompanyId(long actionPlanId, long companyId);

        IEnumerable<ActionPlanDto> Search(SearchActionPlanRequest searchActionPlanRequest);
    }
}