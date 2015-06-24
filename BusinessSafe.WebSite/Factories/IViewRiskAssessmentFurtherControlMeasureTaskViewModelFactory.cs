using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.ViewModels;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Factories
{
    public interface IViewRiskAssessmentFurtherControlMeasureTaskViewModelFactory
    {
        IViewRiskAssessmentFurtherControlMeasureTaskViewModelFactory WithCompanyId(long companyId);
        IViewRiskAssessmentFurtherControlMeasureTaskViewModelFactory WithFurtherControlMeasureTaskId(long furtherControlMeasureTaskId);
        ViewRiskAssessmentFurtherControlMeasureTaskViewModel GetViewModel();
    }
}