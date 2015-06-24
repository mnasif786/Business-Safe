using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Factories
{
    public interface IEditHazardousSubstanceFurtherControlMeasureTaskViewModelFactory
    {
        AddEditFurtherControlMeasureTaskViewModel GetViewModel();
        IEditHazardousSubstanceFurtherControlMeasureTaskViewModelFactory WithCompanyId(long companyId);

        IEditHazardousSubstanceFurtherControlMeasureTaskViewModelFactory WithFurtherControlMeasureTaskId(
            long furtherControlMeasureTaskId);

        IEditHazardousSubstanceFurtherControlMeasureTaskViewModelFactory WithCanDeleteDocuments(bool canDeleteDocuments);
    }
}