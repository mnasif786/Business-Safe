using BusinessSafe.Application.DataTransferObjects;

namespace BusinessSafe.Application.Contracts.AccidentRecord
{
    public interface IInjuryDetailsService
    {
        InjuryAccidentDetailsDto GetAccidentDetails(long accidentId);
    }
}