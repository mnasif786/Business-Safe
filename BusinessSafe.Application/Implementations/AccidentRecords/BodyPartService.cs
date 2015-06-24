using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.AccidentRecord;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Application.Implementations.AccidentRecords
{
    public class BodyPartService : IBodyPartService
    {
        private readonly IBodyPartRepository _bodyPartRepository;

        public BodyPartService(IBodyPartRepository bodyPartRepository)
        {
            _bodyPartRepository = bodyPartRepository;
        }

        public IEnumerable<BodyPartDto> GetAll()
        {
            return _bodyPartRepository.GetAll()
                .Where(x => !x.Deleted)
                .ToList()
                .Map();
        }


    }
}
