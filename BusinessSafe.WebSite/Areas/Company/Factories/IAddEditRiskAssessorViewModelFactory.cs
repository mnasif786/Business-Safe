using BusinessSafe.WebSite.Areas.Company.ViewModels;

namespace BusinessSafe.WebSite.Areas.Company.Factories
{
    public interface IAddEditRiskAssessorViewModelFactory
    {
        IAddEditRiskAssessorViewModelFactory WithCompanyId(long companyId);
        IAddEditRiskAssessorViewModelFactory WithRiskAssessorId(long riskAssessorId);
        AddEditRiskAssessorViewModel GetViewModel();
    }
}