using System;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels;

namespace BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Factories
{
    public interface IEmployeeChecklistSummaryViewModelFactory
    {
        IEmployeeChecklistSummaryViewModelFactory WithEmployeeChecklistId(Guid employeeChecklistId);
        EmployeeChecklistSummaryViewModel GetViewModel();
    }
}