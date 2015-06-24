
using System.Collections.Generic;

using BusinessSafe.WebSite.Areas.Responsibilities.ViewModels.Wizard;

namespace BusinessSafe.WebSite.Areas.Responsibilities.Factories
{
    public interface IGenerateResponsibilitiesViewModelFactory
    {
        IGenerateResponsibilitiesViewModelFactory WithResponsibilityTemplateIds(long[] selectedResponsibilityIds);
        IGenerateResponsibilitiesViewModelFactory WithCompanyId(long companyId);
        IGenerateResponsibilitiesViewModelFactory WithAllowedSiteIds(IList<long> siteIds);

        GenerateResponsibilitiesViewModel GetViewModel();

    }
}