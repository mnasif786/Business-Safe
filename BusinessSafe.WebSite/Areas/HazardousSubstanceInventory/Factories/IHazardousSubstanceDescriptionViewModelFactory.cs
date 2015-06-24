using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.ViewModels;

namespace BusinessSafe.WebSite.Areas.HazardousSubstanceInventory.Factories
{
    public interface IHazardousSubstanceDescriptionViewModelFactory
    {
        IHazardousSubstanceDescriptionViewModelFactory WithRiskAssessmentId(long riskAssessmentId);
        IHazardousSubstanceDescriptionViewModelFactory WithCompanyId(long companyId);
        DescriptionViewModel GetViewModel();
    }
}