using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Tests.Builder
{
    public class CreatingRiskAssessmentSummaryViewModelBuilder
    {
        private static CreatingRiskAssessmentSummaryViewModel _viewModel;
        private long _companyId = 1;
        private string _reference = "Reference";
        private string _title = "Title";

        public static CreatingRiskAssessmentSummaryViewModelBuilder Create()
        {
            _viewModel = new CreatingRiskAssessmentSummaryViewModel();
            return new CreatingRiskAssessmentSummaryViewModelBuilder();
        }

        public CreatingRiskAssessmentSummaryViewModel Build()
        {
            _viewModel.CompanyId = _companyId;
            _viewModel.Reference = _reference;
            _viewModel.Title = _title;
            return _viewModel;
        }

        public CreatingRiskAssessmentSummaryViewModelBuilder WithCompanyId(int companyId)
        {
            _companyId = companyId;
            return this;
        }

        public CreatingRiskAssessmentSummaryViewModelBuilder WithTitle(string title)
        {
            _title = title;
            return this;
        }

        public CreatingRiskAssessmentSummaryViewModelBuilder WithReference(string reference)
        {
            _reference = reference;
            return this;
        }
    }
}