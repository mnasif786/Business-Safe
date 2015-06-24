using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.ViewModels;

namespace BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Factories
{
    public interface IControlMeasuresViewModelFactory
    {
        IControlMeasuresViewModelFactory WithCompanyId(long companyId);
        IControlMeasuresViewModelFactory WithHazardousSubstanceRiskAssessmentId(long hazardousSubstanceRiskAssessmentId);
        ControlMeasuresViewModel GetViewModel();
    }
}