using System.Collections.Generic;
using BusinessSafe.WebSite.Areas.Responsibilities.ViewModels;

namespace BusinessSafe.WebSite.Areas.Responsibilities.Factories
{
    public interface IResponsibilityViewModelFactory
    {
        IResponsibilityViewModelFactory WithCompanyId(long companyId);
        IResponsibilityViewModelFactory WithResponsibilityId(long? responsibilityId);
        IResponsibilityViewModelFactory WithShowCreateResponsibilityTaskDialogOnLoad(bool? showCreateResponsibilityTask);
        IResponsibilityViewModelFactory WithAllowedSiteIds(IList<long> allowedSiteIds);
        ResponsibilityViewModel GetViewModel();
        
    }
}
