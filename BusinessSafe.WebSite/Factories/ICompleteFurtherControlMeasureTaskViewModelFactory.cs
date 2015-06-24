using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Factories
{
    public interface ICompleteFurtherControlMeasureTaskViewModelFactory
    {
        ICompleteFurtherControlMeasureTaskViewModelFactory WithCompanyId(long companyId);
        ICompleteFurtherControlMeasureTaskViewModelFactory WithFurtherControlMeasureTaskId(long furtherControlMeasureTaskId);
        CompleteFurtherControlMeasureTaskViewModel GetViewModel();
    }
}