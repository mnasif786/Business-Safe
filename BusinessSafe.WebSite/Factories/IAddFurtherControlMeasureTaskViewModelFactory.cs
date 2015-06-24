using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Factories
{
    public interface IAddFurtherControlMeasureTaskViewModelFactory
    {
        AddRiskAssessmentFurtherControlMeasureTaskViewModel GetViewModel();
        IAddFurtherControlMeasureTaskViewModelFactory WithCompanyId(long companyId);
        IAddFurtherControlMeasureTaskViewModelFactory WithRiskAssessmentHazardId(long riskAssessmentHazardId);
        IAddFurtherControlMeasureTaskViewModelFactory WithFurtherControlMeasureTaskCategory(FurtherControlMeasureTaskCategoryEnum furtherControlMeasureTaskCategoryEnum);
    }
}