using System;
using System.Collections.Generic;
using BusinessSafe.Application.Contracts.AccidentRecord;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Application.Implementations.AccidentRecords
{
    public class AccidentTypeService : IAccidentTypeService
    {
        private readonly IAccidentTypeRepository _accidentTypeRepository;

        public AccidentTypeService(IAccidentTypeRepository accidentTypeRepository)
        {
            _accidentTypeRepository = accidentTypeRepository;
        }

        public IEnumerable<AccidentTypeDto> GetAll()
        {
            return new AccidentTypeDtoMapper().Map(_accidentTypeRepository.GetAll());
        }

        public IEnumerable<AccidentTypeDto> GetAllForCompany(long companyId)
        {
            return new AccidentTypeDtoMapper().Map(_accidentTypeRepository.GetAllForCompany(companyId));
        }
    }
}