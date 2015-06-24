using System.Collections.Generic;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.ViewModels;

namespace BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Factories
{
    public interface IEditHazardousSubstanceRiskAssessmentSummaryViewModelFactory
    {
        IEditHazardousSubstanceRiskAssessmentSummaryViewModelFactory WithRiskAssessmentId(long riskAssessmentId);
        IEditHazardousSubstanceRiskAssessmentSummaryViewModelFactory WithCompanyId(long companyId);
        IEditHazardousSubstanceRiskAssessmentSummaryViewModelFactory WithAllowableSiteIds(IList<long> allowableSites);
        EditSummaryViewModel GetViewModel();
        EditSummaryViewModel AttachDropDownData(EditSummaryViewModel model);
    }
}