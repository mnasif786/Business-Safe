using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Factories
{
    public interface IReassignFurtherControlMeasureTaskWithHazardSummaryViewModelFactory
    {
        IReassignFurtherControlMeasureTaskWithHazardSummaryViewModelFactory WithCompanyId(long companyId);
        IReassignFurtherControlMeasureTaskWithHazardSummaryViewModelFactory WithFurtherControlMeasureTaskId(long furtherControlMeasureTaskId);
        ReassignRiskAssessmentFurtherControlMeasureTaskViewModel GetViewModel();
    }
}