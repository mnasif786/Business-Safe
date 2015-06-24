using BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels;
using System.Security.Principal;

namespace BusinessSafe.WebSite.Areas.FireRiskAssessments.Factories
{
    public interface IPremisesInformationViewModelFactory
    {
        IPremisesInformationViewModelFactory WithRiskAssessmentId(long riskAssessmentId);
        IPremisesInformationViewModelFactory WithCompanyId(long companyId);
        IPremisesInformationViewModelFactory WithUser(IPrincipal user);
        PremisesInformationViewModel GetViewModel();
        PremisesInformationViewModel GetViewModel(PremisesInformationViewModel viewModel);
        
    }
}