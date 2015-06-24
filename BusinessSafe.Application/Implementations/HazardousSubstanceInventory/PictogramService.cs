using System.Collections.Generic;

using BusinessSafe.Application.Contracts.HazardousSubstanceInventory;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Application.Implementations.HazardousSubstanceInventory
{
    public class PictogramService : IPictogramService
    {
        private readonly IPictogramRepository pictogramRepository;

        public PictogramService(IPictogramRepository pictogramRepository)
        {
            this.pictogramRepository = pictogramRepository;
        }

        public IEnumerable<PictogramDto> GetAll()
        {
            var pictograms = pictogramRepository.GetAll();
            return new PictogramDtoMapper().Map(pictograms);
        }
    }
}
