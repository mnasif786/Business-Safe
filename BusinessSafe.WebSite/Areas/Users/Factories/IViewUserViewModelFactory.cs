using System;
using BusinessSafe.WebSite.Areas.Users.ViewModels;

namespace BusinessSafe.WebSite.Areas.Users.Factories
{
    public interface IViewUserViewModelFactory
    {
        IViewUserViewModelFactory WithCompanyId(long companyId);
        IViewUserViewModelFactory WithEmployeeId(Guid userId);
        ViewUserViewModel GetViewModel();
    }
}