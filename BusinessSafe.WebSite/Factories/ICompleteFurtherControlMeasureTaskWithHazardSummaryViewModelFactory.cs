using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Factories
{
    public interface ICompleteFurtherControlMeasureTaskWithHazardSummaryViewModelFactory
    {
        ICompleteFurtherControlMeasureTaskWithHazardSummaryViewModelFactory WithCompanyId(long companyId);
        ICompleteFurtherControlMeasureTaskWithHazardSummaryViewModelFactory WithFurtherControlMeasureTaskId(long furtherControlMeasureTaskId);
        CompleteRiskAssessmentFurtherControlMeasureTaskViewModel GetViewModel();
    }
}