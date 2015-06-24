using System.Collections.Generic;
using BusinessSafe.WebSite.Areas.Responsibilities.ViewModels.Wizard;

namespace BusinessSafe.WebSite.Areas.Responsibilities.Factories
{
    public interface IGenerateResponsibilityTasksViewModelFactory
    {
        IGenerateResponsibilityTasksViewModelFactory WithCompanyId(long companyId);
        IGenerateResponsibilityTasksViewModelFactory WithAllowedSiteIds(IList<long> siteIds);
        GenerateResponsibilityTasksViewModel GetViewModel();
    }
}