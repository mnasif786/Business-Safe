using System;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels;

namespace BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Factories
{
    public interface IEmployeeChecklistGeneratorViewModelFactory
    {
        IEmployeeChecklistGeneratorViewModelFactory WithRiskAssessmentId(long riskAssessmentId);
        IEmployeeChecklistGeneratorViewModelFactory WithCompanyId(long companyId);
        IEmployeeChecklistGeneratorViewModelFactory WithRiskAssessorEmail(string email);
        IEmployeeChecklistGeneratorViewModelFactory WithCurrentUserId(Guid currentUserId);
        EmployeeChecklistGeneratorViewModel GetViewModel();

        EmployeeChecklistGeneratorViewModel GetViewModel(EmployeeChecklistGeneratorViewModel viewModel);
    }
}