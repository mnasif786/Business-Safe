using System;
using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.ViewModels;

namespace BusinessSafe.WebSite.Tests.Builder
{
    public class PremisesInformationViewModelBuilder
    {
        private static PremisesInformationViewModel _viewModel;
        private long _companyId = 1;
        private const string _reference = "Reference";
        private const string _title = "Title";
        private DateTime? _dateOfAssessment = DateTime.Today;
        private string _location = "Valid Location";
        private string _taskProcess = "Valid Task Process";
        private int _riskAssessmentId;

        public static PremisesInformationViewModelBuilder Create()
        {
            _viewModel = new PremisesInformationViewModel();
            return new PremisesInformationViewModelBuilder();
        }

        public PremisesInformationViewModel Build()
        {
            _viewModel.CompanyId = _companyId;
            _viewModel.Reference = _reference;
            _viewModel.Title = _title;
            _viewModel.LocationAreaDepartment = _location;
            _viewModel.TaskProcessDescription = _taskProcess;
            _viewModel.RiskAssessmentId = _riskAssessmentId;
            return _viewModel;
        }

        public PremisesInformationViewModelBuilder WithDateOfAssessment(DateTime? dateOfAssessment)
        {
            _dateOfAssessment = dateOfAssessment;
            return this;
        }

        public PremisesInformationViewModelBuilder WithLocationAreaDepartment(string location)
        {
            _location = location;
            return this;
        }

        public PremisesInformationViewModelBuilder WithTaskProcess(string taskProcess)
        {
            _taskProcess = taskProcess;
            return this;
        }

        public PremisesInformationViewModelBuilder WithRiskAssessmentId(int riskAssessmentId)
        {
            _riskAssessmentId = riskAssessmentId;
            return this;
        }

        public PremisesInformationViewModelBuilder WithCompanyId(int companyId)
        {
            _companyId = companyId;
            return this;
        }
    }
}