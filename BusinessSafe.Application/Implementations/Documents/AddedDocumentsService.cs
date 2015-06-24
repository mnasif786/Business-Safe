using System;
using System.Collections.Generic;
using BusinessSafe.Application.Contracts.Documents;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Request.Documents;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Application.Implementations.Documents
{
    public class AddedDocumentsService : IAddedDocumentsService
    {
        private readonly IPeninsulaLog _log;
        private readonly IDocumentRepository _documentRepository;
        private readonly IUserForAuditingRepository _userForAuditingRepository;
        private readonly ISiteStructureElementRepository _siteRepository;
        private readonly IDocumentTypeRepository _documentTypeRepository;

        public AddedDocumentsService(IPeninsulaLog log, IDocumentRepository documentRepository, IUserForAuditingRepository userForAuditingRepository, ISiteStructureElementRepository siteRepository, IDocumentTypeRepository documentTypeRepository)
        {
            _log = log;
            _documentRepository = documentRepository;
            _userForAuditingRepository = userForAuditingRepository;
            _siteRepository = siteRepository;
            _documentTypeRepository = documentTypeRepository;
        }

        public void Add(IEnumerable<CreateDocumentRequest> createDocumentRequests, Guid userId, long companyId)
        {
            _log.Add(createDocumentRequests);

            try
            {
                foreach (var createDocumentRequest in createDocumentRequests)
                {
                    var parameters = new CreateAddedDocumentParameters()
                                         {
                                             ClientId = companyId,
                                             Site = _siteRepository.GetById(createDocumentRequest.SiteId),
                                             CreatedBy = _userForAuditingRepository.GetByIdAndCompanyId(userId, companyId),
                                             CreatedOn = DateTime.Now,
                                             Description = createDocumentRequest.Description,
                                             DocumentLibraryId = createDocumentRequest.DocumentLibraryId,
                                             DocumentType = _documentTypeRepository.GetById((long)createDocumentRequest.DocumentType),
                                             Extension = createDocumentRequest.Extension,
                                             Filename = createDocumentRequest.Filename,
                                             FilesizeByte = createDocumentRequest.FilesizeByte,
                                             Title = createDocumentRequest.Title
                                         };
                    var addedDocument = AddedDocument.Create(parameters);

                    _documentRepository.SaveOrUpdate(addedDocument);
                }
            }
            catch (Exception ex)
            {
                _log.Add(ex);

                throw;
            }
        }
    }
}