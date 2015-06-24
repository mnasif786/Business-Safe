using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.WebSite.Areas.ActionPlans.ViewModels;

namespace BusinessSafe.WebSite.Areas.ActionPlans.Factories
{
    interface IAssignActionPlanTaskViewModel
    {
        IAssignActionPlanTaskViewModel WithActionPlanId(long actionPlanId);
        AssignActionPlanTaskViewModel GetViewModel();
        IAssignActionPlanTaskViewModel WithCompanyId(long companyId);
    }
}
