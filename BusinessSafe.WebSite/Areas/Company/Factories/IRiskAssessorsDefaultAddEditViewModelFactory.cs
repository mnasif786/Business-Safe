using System.Collections.Generic;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Areas.Company.ViewModels;

namespace BusinessSafe.WebSite.Areas.Company.Factories
{
    public interface IRiskAssessorsDefaultAddEditViewModelFactory
    {
        IRiskAssessorsDefaultAddEditViewModelFactory WithShowingDeleted(bool showingDeleted);
        IRiskAssessorsDefaultAddEditViewModelFactory WithRiskAssessors(IEnumerable<RiskAssessorDto> riskAssessorDtos);
        RiskAssessorsDefaultAddEdit GetViewModel();
    }
}