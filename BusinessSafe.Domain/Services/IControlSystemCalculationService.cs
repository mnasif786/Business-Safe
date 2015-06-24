using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.Services
{
    public interface IControlSystemCalculationService
    {
        ControlSystem Calculate(string hazardousSubstanceGroupCode, MatterState? matterState, Quantity? quantity, DustinessOrVolatility? dustinessOrVolatility);
    }
}