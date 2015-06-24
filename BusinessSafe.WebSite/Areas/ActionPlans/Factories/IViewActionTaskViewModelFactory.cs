using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.WebSite.Areas.ActionPlans.ViewModels;

namespace BusinessSafe.WebSite.Areas.ActionPlans.Factories
{
    public interface IViewActionTaskViewModelFactory
    {
        IViewActionTaskViewModelFactory WithCompanyId(long companyId);
        IViewActionTaskViewModelFactory WithActionTaskId(long actionTaskId);
        ViewActionTaskViewModel GetViewModel();
    }
}
