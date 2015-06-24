using System.Collections.Generic;
using BusinessSafe.Application.Contracts.Documents;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Application.Implementations.Documents
{

    public class DocHandlerDocumentTypeService : IDocHandlerDocumentTypeService
    {
        private readonly IDocHandlerDocumentTypeRepository _docHandlerDocumentTypeRepository;
        public DocHandlerDocumentTypeService(IDocHandlerDocumentTypeRepository docHandlerDocumentTypeRepository)
        {
            _docHandlerDocumentTypeRepository = docHandlerDocumentTypeRepository;
        }

        public IEnumerable<DocHandlerDocumentTypeDto> GetForDocumentGroup(DocHandlerDocumentTypeGroup docHandlerDocumentTypeGroup)
        {
            var docHandlerDocumentTypes = _docHandlerDocumentTypeRepository.GetByDocHandlerDocumentTypeGroup(docHandlerDocumentTypeGroup);
            return new DocHandlerDocumentTypeDtoMapper().Map(docHandlerDocumentTypes);
        }
    }
}