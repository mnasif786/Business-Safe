using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.AccidentRecord;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Data.Repository;

namespace BusinessSafe.Application.Implementations.AccidentRecords
{
    public class InjuryService : IInjuryService
    {
        private readonly InjuryRepository _injuryRepository;

        public InjuryService(InjuryRepository injuryRepository)
        {
            _injuryRepository = injuryRepository;
        }

        public IEnumerable<InjuryDto> GetAll()
        {
            return _injuryRepository.GetAll()
                .Where(x=> !x.Deleted)
                .ToList()
                .Map();
        }

        public IEnumerable<InjuryDto> GetAllInjuriesForAccidentRecord(long companyId, long accidentRecord)
        {
            return _injuryRepository.GetAllInjuriesForAccidentRecord(companyId, accidentRecord)
               .Where(x => !x.Deleted)
               .ToList()
               .Map();
        }
    }
}