using BusinessSafe.WebSite.Areas.Responsibilities.ViewModels;

namespace BusinessSafe.WebSite.Areas.Responsibilities.Factories
{
    public interface IViewResponsibilityTaskViewModelFactory
    {
        IViewResponsibilityTaskViewModelFactory WithCompanyId(long companyId);
        IViewResponsibilityTaskViewModelFactory WithResponsibilityTaskId(long responsibilityTaskId);
        ViewResponsibilityTaskViewModel GetViewModel();
    }
}