using BusinessSafe.Application.Contracts.HazardousSubstanceRiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.Services;

namespace BusinessSafe.Application.Implementations.HazardousSubstanceRiskAssessments
{
    public class ControlSystemService : IControlSystemService
    {
        private readonly IControlSystemCalculationService _controlSystemCalculationService;

        public ControlSystemService(IControlSystemCalculationService controlSystemCalculationService)
        {
            _controlSystemCalculationService = controlSystemCalculationService;
        }

        public ControlSystemDto Calculate(string hazardousSubstanceGroupCode, MatterState? matterState, Quantity? quantity, DustinessOrVolatility? dustinessOrVolatility)
        {
            var controlSystem = _controlSystemCalculationService.Calculate(hazardousSubstanceGroupCode, matterState, quantity, dustinessOrVolatility);
            return new ControlSystemDtoMapper().Map(controlSystem);
        }
    }
}
