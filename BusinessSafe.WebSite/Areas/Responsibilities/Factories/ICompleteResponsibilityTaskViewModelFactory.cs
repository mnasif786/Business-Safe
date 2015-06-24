using BusinessSafe.WebSite.Areas.Responsibilities.ViewModels;

namespace BusinessSafe.WebSite.Areas.Responsibilities.Factories
{
    public interface ICompleteResponsibilityTaskViewModelFactory
    {
        ICompleteResponsibilityTaskViewModelFactory WithCompanyId(long companyId);
        ICompleteResponsibilityTaskViewModelFactory WithResponsibilityTaskId(long responsibilityTaskId);
        CompleteResponsibilityTaskViewModel GetViewModel();
    }
}