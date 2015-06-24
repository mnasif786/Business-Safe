using System.Collections.Generic;
using BusinessSafe.Application.Contracts.AccidentRecord;
using BusinessSafe.Application.DataTransferObjects;

namespace BusinessSafe.Application.Implementations.AccidentRecords
{
    public class InjuryDetailsService : IInjuryDetailsService
    {
        public InjuryAccidentDetailsDto GetAccidentDetails(long accidentId)
        {
            return new InjuryAccidentDetailsDto(InjurySeverity.Unknown, new List<string> { "universal 3" }, new List<string>(), "", "", InjuredToHospital.Unknown, InjuredAbleToWork.Unknown, UnableToWork.Unknown);
        }
    }
}