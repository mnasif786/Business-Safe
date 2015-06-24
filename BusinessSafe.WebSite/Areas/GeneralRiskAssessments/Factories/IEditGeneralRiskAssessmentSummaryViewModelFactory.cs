using System.Collections.Generic;
using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.ViewModels;

namespace BusinessSafe.WebSite.Areas.GeneralRiskAssessments.Factories
{
    public interface IEditGeneralRiskAssessmentSummaryViewModelFactory
    {
        IEditGeneralRiskAssessmentSummaryViewModelFactory WithRiskAssessmentId(long riskAssessmentId);
        IEditGeneralRiskAssessmentSummaryViewModelFactory WithCompanyId(long companyId);
        IEditGeneralRiskAssessmentSummaryViewModelFactory WithAllowableSiteIds(IList<long> allowableSites);
        EditSummaryViewModel GetViewModel();
        EditSummaryViewModel GetViewModel(EditSummaryViewModel viewModel);
    }
}