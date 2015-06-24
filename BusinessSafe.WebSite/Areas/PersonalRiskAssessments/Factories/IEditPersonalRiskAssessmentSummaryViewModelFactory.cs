using System;
using System.Collections.Generic;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels;

namespace BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Factories
{
    public interface IEditPersonalRiskAssessmentSummaryViewModelFactory
    {
        IEditPersonalRiskAssessmentSummaryViewModelFactory WithRiskAssessmentId(long riskAssessmentId);
        IEditPersonalRiskAssessmentSummaryViewModelFactory WithCompanyId(long companyId);
        IEditPersonalRiskAssessmentSummaryViewModelFactory WithCurrentUserId(Guid currentUserId);
        IEditPersonalRiskAssessmentSummaryViewModelFactory WithAllowableSiteIds(IList<long> allowableSites);
        EditSummaryViewModel GetViewModel();
        EditSummaryViewModel GetViewModel(EditSummaryViewModel model);
    }
}