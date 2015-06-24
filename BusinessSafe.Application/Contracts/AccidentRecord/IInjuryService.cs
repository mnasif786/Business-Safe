using System.Collections.Generic;
using BusinessSafe.Application.DataTransferObjects;

namespace BusinessSafe.Application.Contracts.AccidentRecord
{
    public interface IInjuryService
    {
        IEnumerable<InjuryDto> GetAll();
        IEnumerable<InjuryDto> GetAllInjuriesForAccidentRecord(long companyId, long accidentRecord);
    }
}