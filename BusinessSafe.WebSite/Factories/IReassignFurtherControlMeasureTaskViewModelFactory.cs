using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Factories
{
    public interface IReassignFurtherControlMeasureTaskViewModelFactory
    {
        IReassignFurtherControlMeasureTaskViewModelFactory WithCompanyId(long companyId);
        IReassignFurtherControlMeasureTaskViewModelFactory WithFurtherControlMeasureTaskId(long furtherControlMeasureTaskId);
        ReassignFurtherControlMeasureTaskViewModel GetViewModel();
    }
}