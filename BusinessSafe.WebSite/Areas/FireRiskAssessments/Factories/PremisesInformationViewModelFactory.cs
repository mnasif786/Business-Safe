using BusinessSafe.Application.Contracts.FireRiskAssessments;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using System.Security.Principal;

namespace BusinessSafe.WebSite.Areas.FireRiskAssessments.Factories
{
    public class PremisesInformationViewModelFactory : IPremisesInformationViewModelFactory
    {
        private long _riskAssessmentId;
        private long _companyId;
        private IPrincipal _user;
        private readonly IFireRiskAssessmentService _riskAssessmentService;
        
        public PremisesInformationViewModelFactory(IFireRiskAssessmentService riskAssessmentService)
        {
            _riskAssessmentService = riskAssessmentService;
        }

        public IPremisesInformationViewModelFactory WithRiskAssessmentId(long riskAssessmentId)
        {
            _riskAssessmentId = riskAssessmentId;
            return this;
        }

        public IPremisesInformationViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public IPremisesInformationViewModelFactory WithUser(IPrincipal user)
        {
            _user = user;
            return this;
        }
        
        public PremisesInformationViewModel GetViewModel()
        {
            var riskAssessment = _riskAssessmentService.GetRiskAssessment(_riskAssessmentId, _companyId);
            
            var viewModel = new PremisesInformationViewModel()
                                {
                                    CompanyId = _companyId,
                                    RiskAssessmentId = _riskAssessmentId,
                                    PremisesProvidesSleepingAccommodation = riskAssessment.PremisesProvidesSleepingAccommodation,
                                    PremisesProvidesSleepingAccommodationConfirmed = riskAssessment.PremisesProvidesSleepingAccommodationConfirmed,
                                    IsSaveButtonEnabled = _user.IsInRole(Permissions.EditGeneralandHazardousSubstancesRiskAssessments.ToString()),
                                    Location = riskAssessment.Location,
                                    BuildingUse = riskAssessment.BuildingUse,
                                    NumberOfPeople = riskAssessment.NumberOfPeople,
                                    NumberOfFloors = riskAssessment.NumberOfFloors,
                                    ElectricityEmergencyShutOff = riskAssessment.ElectricityEmergencyShutOff,
                                    GasEmergencyShutOff = riskAssessment.GasEmergencyShutOff,
                                    WaterEmergencyShutOff = riskAssessment.WaterEmergencyShutOff,
                                    OtherEmergencyShutOff = riskAssessment.OtherEmergencyShutOff
                                };
            
            return viewModel;
        }

        public PremisesInformationViewModel GetViewModel(PremisesInformationViewModel viewModel)
        {
            var result = new PremisesInformationViewModel()
            {
                CompanyId = viewModel.CompanyId,
                RiskAssessmentId = viewModel.RiskAssessmentId,
                PremisesProvidesSleepingAccommodation = viewModel.PremisesProvidesSleepingAccommodation,
                PremisesProvidesSleepingAccommodationConfirmed = viewModel.PremisesProvidesSleepingAccommodationConfirmed,
                IsSaveButtonEnabled = _user.IsInRole(Permissions.EditGeneralandHazardousSubstancesRiskAssessments.ToString()),
                Location = viewModel.Location,
                BuildingUse = viewModel.BuildingUse,
                NumberOfFloors = viewModel.NumberOfFloors,
                NumberOfPeople = viewModel.NumberOfPeople,
                ElectricityEmergencyShutOff = viewModel.ElectricityEmergencyShutOff,
                GasEmergencyShutOff = viewModel.GasEmergencyShutOff,
                WaterEmergencyShutOff = viewModel.WaterEmergencyShutOff,
                OtherEmergencyShutOff = viewModel.OtherEmergencyShutOff
            };

            
            return result;
        }
    }
}