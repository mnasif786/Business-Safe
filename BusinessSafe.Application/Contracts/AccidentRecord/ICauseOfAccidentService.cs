using System.Collections.Generic;
using BusinessSafe.Application.DataTransferObjects;

namespace BusinessSafe.Application.Contracts.AccidentRecord
{
    public interface ICauseOfAccidentService
    {
        IEnumerable<CauseOfAccidentDto> GetAll();
    }
}