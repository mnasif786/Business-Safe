using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Contracts.HazardousSubstanceRiskAssessments
{
    public interface IControlSystemService
    {
        ControlSystemDto Calculate(string hazardousSubstanceGroupCode, MatterState? matterState, Quantity? quantity,
                                   DustinessOrVolatility? dustinessOrVolatility);
    }
}
