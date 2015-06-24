using System;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels;

namespace BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Factories
{
    public interface IChecklistManagerViewModelFactory
    {
        IChecklistManagerViewModelFactory WithCompanyId(long companyId);
        IChecklistManagerViewModelFactory WithRiskAssessmentId(long riskAssessmentId);
        IChecklistManagerViewModelFactory WithCurrentUserId(Guid currentUserId);
        IChecklistManagerViewModelFactory WithEmailContains(string emailContains);
        IChecklistManagerViewModelFactory WithNameContains(string nameContains);
        ChecklistManagerViewModel GetViewModel();
    }
}