using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Factories
{
    public interface IEditFurtherControlMeasureTaskViewModelFactory
    {
        IEditFurtherControlMeasureTaskViewModelFactory WithCompanyId(long companyId);
        IEditFurtherControlMeasureTaskViewModelFactory WithFurtherControlMeasureTaskId(long furtherControlMeasureTaskId);
        IEditFurtherControlMeasureTaskViewModelFactory WithCanDeleteDocuments(bool canDeleteDocuments);
        EditRiskAssessmentFurtherControlMeasureTaskViewModel GetViewModel();
    }
}