using BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels;

namespace BusinessSafe.WebSite.Areas.FireRiskAssessments.Factories
{
    public interface IAddFireRiskAssessmentFurtherControlMeasureTaskViewModelFactory
    {
        IAddFireRiskAssessmentFurtherControlMeasureTaskViewModelFactory WithCompanyId(long companyId);
        IAddFireRiskAssessmentFurtherControlMeasureTaskViewModelFactory WithRiskAssessmentId(long riskAssessmentId);
        IAddFireRiskAssessmentFurtherControlMeasureTaskViewModelFactory WithSignificantFindingId(long significantFindingId);
        AddEditFireRiskAssessmentFurtherControlMeasureTaskViewModel GetViewModel();
    }
}   