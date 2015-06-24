using BusinessSafe.WebSite.ViewModels;
using BusinessSafe.Application.DataTransferObjects;

namespace BusinessSafe.WebSite.Factories
{
    public interface IViewFurtherControlMeasureTaskViewModelFactory
    {
        IViewFurtherControlMeasureTaskViewModelFactory WithCompanyId(long companyId);
        IViewFurtherControlMeasureTaskViewModelFactory WithFurtherControlMeasureTaskId(long furtherControlMeasureTaskId);
        ViewFurtherControlMeasureTaskViewModel GetViewModel();
        ViewFurtherControlMeasureTaskViewModel GetViewModel(FurtherControlMeasureTaskDto furtherControlMeasureTask);
    }
}