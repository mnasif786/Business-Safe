using BusinessSafe.WebSite.Areas.Responsibilities.ViewModels;

namespace BusinessSafe.WebSite.Areas.Responsibilities.Factories
{
    public interface ICreateUpdateResponsibilityTaskViewModelFactory
    {
        ICreateUpdateResponsibilityTaskViewModelFactory WithCompanyId(long companyId);
        ICreateUpdateResponsibilityTaskViewModelFactory WithResponsibilityId(long responsibilityId);
        ICreateUpdateResponsibilityTaskViewModelFactory WithTaskId(long? taskId);

        ICreateUpdateResponsibilityTaskViewModelFactory WithAutoLaunchedAfterCreatingResponsibility(
            bool? autoLaunchedAfterCreatingResponsibility);

        CreateUpdateResponsibilityTaskViewModel GetViewModel();
    }
}