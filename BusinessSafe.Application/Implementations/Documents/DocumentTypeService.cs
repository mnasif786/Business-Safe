using System;
using System.Collections.Generic;

using BusinessSafe.Application.Contracts.Documents;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using BusinessSafe.Application.Mappers;

namespace BusinessSafe.Application.Implementations.Documents
{
    public class DocumentTypeService : IDocumentTypeService
    {
        private readonly IDocumentTypeRepository _documentTypeRepository;
        private readonly IPeninsulaLog _log;

        public DocumentTypeService(
            IDocumentTypeRepository documentTypeRepository,
            IPeninsulaLog log)
        {
            _documentTypeRepository = documentTypeRepository;
            _log = log;
        }

        public IEnumerable<DocumentTypeDto> GetAll()
        {
            _log.Add();

            try
            {
                var documentTypes = _documentTypeRepository.GetAll();
                return new DocumentTypeDtoMapper().Map(documentTypes);
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }
    }
}