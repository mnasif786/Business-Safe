using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels;

namespace BusinessSafe.WebSite.Areas.FireRiskAssessments.Factories
{
    public interface ISignificantFindingViewModelFactory
    {
        ISignificantFindingViewModelFactory WithCompanyId(long companyId);
        ISignificantFindingViewModelFactory WithRiskAssessmentId(long riskAssessmentId);
        ISignificantFindingViewModelFactory WithUser(IPrincipal user);
        RiskAssessmentSignificantFindingsViewModel GetViewModel();
    }
}