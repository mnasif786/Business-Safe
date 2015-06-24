using System.Collections.Generic;
using BusinessSafe.WebSite.Areas.ActionPlans.ViewModels;

namespace BusinessSafe.WebSite.Areas.ActionPlans.Factories
{
    public interface ISearchActionPlanViewModelFactory
    {
        ISearchActionPlanViewModelFactory WithCompanyId(long companyId);
        ISearchActionPlanViewModelFactory WithAllowedSiteIds(IList<long> allowedSiteIds);
        ISearchActionPlanViewModelFactory WithPageNumber(int page);
        ISearchActionPlanViewModelFactory WithPageSize(int size);
        ISearchActionPlanViewModelFactory WithOrderBy(string orderBy);
        ISearchActionPlanViewModelFactory WithSiteGroupId(long? siteGroupId);
        ISearchActionPlanViewModelFactory WithSiteId(long? siteId);
        ISearchActionPlanViewModelFactory WithSubmittedFrom(string submittedFrom);
        ISearchActionPlanViewModelFactory WithSubmittedTo(string submittedTo);
        ISearchActionPlanViewModelFactory WithShowArchived(bool showArchived);
        ActionPlanIndexViewModel GetViewModel();

        
    }
}