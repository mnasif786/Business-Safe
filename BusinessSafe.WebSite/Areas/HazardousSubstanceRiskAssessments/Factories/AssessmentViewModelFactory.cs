using BusinessSafe.Application.Contracts.HazardousSubstanceRiskAssessments;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.ViewModels;
using BusinessSafe.WebSite.Helpers;
using System.Web;

namespace BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Factories
{
    public class AssessmentViewModelFactory : IAssessmentViewModelFactory
    {
        private readonly IHazardousSubstanceRiskAssessmentService _hazardousSubstanceRiskAssessmentService;
        private readonly IControlSystemService _controlSystemService;
        private long? _companyId;
        private long? _hazardousSubstanceRiskAssessmentId;
        private IVirtualPathUtilityWrapper _virtualPathUtilityWrapper;

        public AssessmentViewModelFactory(
            IHazardousSubstanceRiskAssessmentService hazardousSubstanceRiskAssessmentService, 
            IControlSystemService controlSystemService, 
            IVirtualPathUtilityWrapper virtualPathUtilityWrapper)
        {
            _virtualPathUtilityWrapper = virtualPathUtilityWrapper;
            _hazardousSubstanceRiskAssessmentService = hazardousSubstanceRiskAssessmentService;
            _controlSystemService = controlSystemService;
        }

        public IAssessmentViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public IAssessmentViewModelFactory WithRiskAssessmentId(long hazardousSubstanceRiskAssessmentId)
        {
            _hazardousSubstanceRiskAssessmentId = hazardousSubstanceRiskAssessmentId;
            return this;
        }

        public AssessmentViewModel GetViewModel()
        {
            var viewModel = new AssessmentViewModel();

            var hazardousSubstanceRiskAssessment = _hazardousSubstanceRiskAssessmentService.GetRiskAssessment(_hazardousSubstanceRiskAssessmentId.Value, _companyId.Value);


            if (hazardousSubstanceRiskAssessment.Group != null)
            {
                viewModel.HazardGroup = hazardousSubstanceRiskAssessment.Group.Code;

                var controlSystemDto = _controlSystemService.Calculate(hazardousSubstanceRiskAssessment.Group.Code, hazardousSubstanceRiskAssessment.MatterState, hazardousSubstanceRiskAssessment.Quantity, hazardousSubstanceRiskAssessment.DustinessOrVolatility);
                var url = _virtualPathUtilityWrapper.ToAbsolute("~/Documents/Document/DownloadPublicDocument?enc=" + HttpUtility.UrlEncode(EncryptionHelper.Encrypt("documentLibraryId=" + controlSystemDto.DocumentLibraryId)));
                viewModel.ControlSystemId = controlSystemDto.Id;
                viewModel.WorkApproach = controlSystemDto.Description;
                viewModel.Url = url;
            }
            else
            {
                viewModel.WorkApproach = "None";
                viewModel.Url = string.Empty;
            }

            viewModel.RiskAssessmentId = hazardousSubstanceRiskAssessment.Id;
            viewModel.Quantity = hazardousSubstanceRiskAssessment.Quantity;
            viewModel.MatterState = hazardousSubstanceRiskAssessment.MatterState;
            viewModel.DustinessOrVolatility = hazardousSubstanceRiskAssessment.DustinessOrVolatility;
            viewModel.HealthSurveillanceRequired = hazardousSubstanceRiskAssessment.HealthSurveillanceRequired;
            viewModel.CompanyId = _companyId.Value;
            viewModel.RiskAssessmentId = _hazardousSubstanceRiskAssessmentId.Value;
            return viewModel;
        }
    }
}