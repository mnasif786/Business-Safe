using System.Collections.Generic;
using System.Security.Principal;
using BusinessSafe.WebSite.Areas.Users.ViewModels;

namespace BusinessSafe.WebSite.Areas.Users.Factories
{
    public interface IUserSearchViewModelFactory
    {
        IUserSearchViewModelFactory WithCompanyId(long companyId);
        IUserSearchViewModelFactory WithForeName(string foreName);
        IUserSearchViewModelFactory WithSurname(string surname);
        IUserSearchViewModelFactory WithSiteGroupId(long siteGroupId);
        IUserSearchViewModelFactory WithSiteId(long siteId);
        IUserSearchViewModelFactory WithShowDeleted(bool showDeleted);
        IUserSearchViewModelFactory WithAllowedSiteIds(IList<long> allowedSiteIds);
        IUserSearchViewModelFactory WithCurrentUser(IPrincipal currentUser);
        UserSearchViewModel GetViewModel();
    }
}