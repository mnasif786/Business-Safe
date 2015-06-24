using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.ViewModels;

namespace BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Factories
{
    public interface IAssessmentViewModelFactory
    {
        IAssessmentViewModelFactory WithCompanyId(long companyId);
        IAssessmentViewModelFactory WithRiskAssessmentId(long hazardousSubstanceRiskAssessmentId);
        AssessmentViewModel GetViewModel();
    }
}
