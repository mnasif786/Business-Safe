using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.ViewModels;

namespace BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Factories
{
    public interface ICreateRiskAssessmentViewModelFactory
    {
        ICreateRiskAssessmentViewModelFactory WithCompanyId(long companyId);

        ICreateRiskAssessmentViewModelFactory WithNewHazardousSubstanceId(long? newHazardousSubstanceId);

        CreateRiskAssessmentViewModel GetViewModel();

        CreateRiskAssessmentViewModel GetViewModel(CreateRiskAssessmentViewModel createRiskAssessmentViewModel);
    }
}
