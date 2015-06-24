using BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels;

namespace BusinessSafe.WebSite.Areas.FireRiskAssessments.Factories
{
    public interface IEditFireRiskAssessmentFurtherControlMeasureTaskViewModelFactory
    {
        IEditFireRiskAssessmentFurtherControlMeasureTaskViewModelFactory WithCompanyId(long companyId);
        IEditFireRiskAssessmentFurtherControlMeasureTaskViewModelFactory WithFurtherControlMeasureTaskId(long furtherControlMeasureTaskId);
        IEditFireRiskAssessmentFurtherControlMeasureTaskViewModelFactory WithCanDeleteDocuments(bool canDelete);
        AddEditFireRiskAssessmentFurtherControlMeasureTaskViewModel GetViewModel();
    }
}