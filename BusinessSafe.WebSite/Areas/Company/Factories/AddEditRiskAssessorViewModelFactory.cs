using System.Linq;
using BusinessSafe.Application.Contracts.Company;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Mappers;
using BusinessSafe.WebSite.Areas.Company.ViewModels;
using BusinessSafe.WebSite.Extensions;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.Company.Factories
{
    public class AddEditRiskAssessorViewModelFactory : IAddEditRiskAssessorViewModelFactory
    {
        private readonly IRiskAssessorService _riskAssessorService;
        private long _companyId;
        private long _riskAssessorId;

        public AddEditRiskAssessorViewModelFactory(IRiskAssessorService riskAssessorService)
        {
            _riskAssessorService = riskAssessorService;
        }

        public IAddEditRiskAssessorViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public IAddEditRiskAssessorViewModelFactory WithRiskAssessorId(long riskAssessorId)
        {
            _riskAssessorId = riskAssessorId;
            return this;
        }

        public AddEditRiskAssessorViewModel GetViewModel()
        {
            AddEditRiskAssessorViewModel viewModel;


            viewModel = new AddEditRiskAssessorViewModel
            {
                SaveButtonEnabled = true
            };

            if (_companyId > 0 && _riskAssessorId > 0)
            {
                var riskAssessor = _riskAssessorService.GetByIdAndCompanyId(_riskAssessorId, _companyId);
                viewModel.EmployeeId = riskAssessor.Employee.Id;
                viewModel.Employee = riskAssessor.Employee.Forename + " " + riskAssessor.Employee.Surname;
                viewModel.SiteId = riskAssessor.Site != null ? riskAssessor.Site.Id : (long?)null;
                viewModel.Site = riskAssessor.Site != null ? riskAssessor.Site.Name : null;
                viewModel.DoNotSendReviewDueNotification = riskAssessor.DoNotSendReviewDueNotification;
                viewModel.DoNotSendTaskOverdueNotifications = riskAssessor.DoNotSendTaskOverdueNotifications;
                viewModel.DoNotSendTaskCompletedNotifications = riskAssessor.DoNotSendTaskCompletedNotifications;
                viewModel.HasAccessToAllSites = riskAssessor.HasAccessToAllSites;
            }
            return viewModel;
        }
    }
}
