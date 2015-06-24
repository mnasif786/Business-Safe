using System;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels;

namespace BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Factories
{
    public interface IPremisesInformationViewModelFactory
    {
        IPremisesInformationViewModelFactory WithRiskAssessmentId(long riskAssessmentId);
        IPremisesInformationViewModelFactory WithCompanyId(long companyId);
        IPremisesInformationViewModelFactory WithCurrentUserId(Guid currentUserId);
        PremisesInformationViewModel GetViewModel();
        PremisesInformationViewModel GetViewModel(PremisesInformationViewModel viewModel);
    }
}