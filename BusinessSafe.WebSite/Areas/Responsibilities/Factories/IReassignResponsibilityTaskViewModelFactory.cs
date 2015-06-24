using BusinessSafe.WebSite.Areas.Responsibilities.ViewModels;

namespace BusinessSafe.WebSite.Areas.Responsibilities.Factories
{
    public interface IReassignResponsibilityTaskViewModelFactory
    {
        IReassignResponsibilityTaskViewModelFactory WithCompanyId(long companyId);
        IReassignResponsibilityTaskViewModelFactory WithResponsibilityTaskId(long responsibilityTaskId);
        ReassignResponsibilityTaskViewModel GetViewModel();
    }
}