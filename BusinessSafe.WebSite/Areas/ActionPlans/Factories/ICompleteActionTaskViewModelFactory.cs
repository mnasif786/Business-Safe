using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.WebSite.Areas.ActionPlans.ViewModels;

namespace BusinessSafe.WebSite.Areas.ActionPlans.Factories
{
    public interface ICompleteActionTaskViewModelFactory
    {
       ICompleteActionTaskViewModelFactory WithCompanyId(long companyId);
       ICompleteActionTaskViewModelFactory WithActionTaskId(long actionTaskId);

       CompleteActionTaskViewModel GetViewModel();        
    }
}
