using System.Collections.Generic;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels;
using FluentValidation.Results;

namespace BusinessSafe.WebSite.Areas.FireRiskAssessments.Factories
{
    public interface IFireRiskAssessmentChecklistViewModelFactory
    {
        IFireRiskAssessmentChecklistViewModelFactory WithCompanyId(long companyId);
        IFireRiskAssessmentChecklistViewModelFactory WithRiskAssessmentId(long riskAssessmentId);
        FireRiskAssessmentChecklistViewModel GetViewModel();
        FireRiskAssessmentChecklistViewModel GetViewModel(FireRiskAssessmentChecklistViewModel viewModel, IList<ValidationFailure> errors);
        
    }
}