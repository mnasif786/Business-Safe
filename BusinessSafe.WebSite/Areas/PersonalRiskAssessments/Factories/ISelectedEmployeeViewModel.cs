using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels;

namespace BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Factories
{
    public interface ISelectedEmployeeViewModelFactory
    {
        ISelectedEmployeeViewModelFactory WithCompanyId(long companyId);
        List<SelectedEmployeeViewModel> GetViewModel();
        ISelectedEmployeeViewModelFactory WithRiskAssessmentId(long personalRiskAssessmentId);
        ISelectedEmployeeViewModelFactory WithUserId(Guid userId);
    }
}