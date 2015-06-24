using BusinessSafe.WebSite.Areas.ActionPlans.ViewModels;

namespace BusinessSafe.WebSite.Areas.ActionPlans.Factories
{
    public interface IReassignActionTaskViewModelFactory
    {
        IReassignActionTaskViewModelFactory WithCompanyId(long companyId);
        IReassignActionTaskViewModelFactory WithActionPlanId(long actionplanId);
        IReassignActionTaskViewModelFactory WithActionId(long actionId);
        IReassignActionTaskViewModelFactory WithTaskId(long taskId);
        ReassignActionTaskViewModel GetViewModel();
        ReassignActionTaskViewModel GetViewModelByTask();
    }
}