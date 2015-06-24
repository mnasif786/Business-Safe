using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.WebSite.Areas.ActionPlans.ViewModels;

namespace BusinessSafe.WebSite.Areas.ActionPlans.Factories
{
    public interface ISearchActionViewModelFactory
    {
        ISearchActionViewModelFactory WithActionPlanId(long actionPlanId);
        ImmediateRiskNotificationActionsIndexViewModel GetViewModel();
        ISearchActionViewModelFactory WithCompanyId(long companyId);
    }
}
