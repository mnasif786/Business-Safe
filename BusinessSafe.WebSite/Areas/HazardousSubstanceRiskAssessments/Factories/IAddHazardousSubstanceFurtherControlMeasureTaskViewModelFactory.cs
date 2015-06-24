using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.ViewModels;

namespace BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Factories
{
    public interface IAddHazardousSubstanceFurtherControlMeasureTaskViewModelFactory
    {
        AddHazardousSubstanceFurtherControlMeasureTaskViewModel GetViewModel();
        IAddHazardousSubstanceFurtherControlMeasureTaskViewModelFactory WithCompanyId(long companyId);
        IAddHazardousSubstanceFurtherControlMeasureTaskViewModelFactory WithRiskAssessmentId(long riskAssessmentId);
        IAddHazardousSubstanceFurtherControlMeasureTaskViewModelFactory WithCanDeleteDocuments(bool canDeleteDocuments);
    }
}