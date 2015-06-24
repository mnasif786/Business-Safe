using System;
using System.Collections.Generic;
using BusinessSafe.Application.Contracts.AccidentRecord;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Application.Implementations.AccidentRecords
{
    public class CauseOfAccidentService : ICauseOfAccidentService
    {
        private readonly ICauseOfAccidentRepository _causeOfAccidentRepository;

        public CauseOfAccidentService(ICauseOfAccidentRepository causeOfAccidentRepository)
        {
            _causeOfAccidentRepository = causeOfAccidentRepository;
        }
        public IEnumerable<CauseOfAccidentDto> GetAll()
        {
            return new CauseOfAccidentDtoMapper().Map(_causeOfAccidentRepository.GetAll()); 
        }
    }
}