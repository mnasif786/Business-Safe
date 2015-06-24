using System.Collections.Generic;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels;

namespace BusinessSafe.WebSite.Areas.FireRiskAssessments.Factories
{
    public interface IEditFireRiskAssessmentSummaryViewModelFactory
    {
        IEditFireRiskAssessmentSummaryViewModelFactory WithRiskAssessmentId(long riskAssessmentId);
        IEditFireRiskAssessmentSummaryViewModelFactory WithCompanyId(long companyId);
        IEditFireRiskAssessmentSummaryViewModelFactory WithAllowableSiteIds(IList<long> allowableSites);
        EditSummaryViewModel GetViewModel();
        EditSummaryViewModel GetViewModel(EditSummaryViewModel viewModel);
    }
}