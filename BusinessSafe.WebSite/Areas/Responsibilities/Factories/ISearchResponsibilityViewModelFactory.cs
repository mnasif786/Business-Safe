using System;
using System.Collections.Generic;
using BusinessSafe.WebSite.Areas.Responsibilities.ViewModels;

namespace BusinessSafe.WebSite.Areas.Responsibilities.Factories
{
    public interface ISearchResponsibilityViewModelFactory
    {
        ISearchResponsibilityViewModelFactory WithCompanyId(long companyId);
        ISearchResponsibilityViewModelFactory WithCategoryId(long categoryId);
        ISearchResponsibilityViewModelFactory WithSiteId(long siteId);
        ISearchResponsibilityViewModelFactory WithSiteGroupId(long siteGroupId);
        ISearchResponsibilityViewModelFactory WithTitle(string title);
        ISearchResponsibilityViewModelFactory WithCreatedFrom(DateTime createdFrom);
        ISearchResponsibilityViewModelFactory WithCreatedTo(DateTime createdTo);
        ISearchResponsibilityViewModelFactory WithShowDeleted(bool showDeleted);
        ISearchResponsibilityViewModelFactory WithPageSize(int pageSize);
        ISearchResponsibilityViewModelFactory WithPageNumber(int page);
        ISearchResponsibilityViewModelFactory WithOrderBy(string orderBy);
        ISearchResponsibilityViewModelFactory WithAllowedSiteIds(IList<long> allowedSiteIds);

        ResponsibilitiesIndexViewModel GetViewModel();
    }
}