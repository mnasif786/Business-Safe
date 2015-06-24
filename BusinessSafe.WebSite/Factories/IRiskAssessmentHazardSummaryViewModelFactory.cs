using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Factories
{
    public interface IRiskAssessmentHazardSummaryViewModelFactory
    {
        IRiskAssessmentHazardSummaryViewModelFactory WithRiskAssessmentHazardId(long riskAssessmentHazardId);
        IRiskAssessmentHazardSummaryViewModelFactory WithCompanyId(long companyId);
        RiskAssessmentHazardSummaryViewModel GetViewModel();
    }
}