using System;
using System.Collections.Generic;
using BusinessSafe.Application.Contracts.Documents;
using BusinessSafe.Application.Custom_Exceptions;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Application.Request.Documents;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using System.Linq;

namespace BusinessSafe.Application.Implementations.Documents
{
    public class DocumentService: IDocumentService
    {
        private readonly IPeninsulaLog _log;
        private readonly IDocumentRepository _documentRepository;
        private readonly IUserForAuditingRepository _userForAuditingRepository;
        private readonly ITaskDocumentRepository _taskDocumentRepository;
        private readonly IRiskAssessmentDocumentRepository _riskAssessmentDocumentRepository;
        private readonly IAddedDocumentRepository _addedDocumentRepository;

        public DocumentService(IPeninsulaLog log, IDocumentRepository documentRepository, IUserForAuditingRepository userForAuditingRepository, ITaskDocumentRepository taskDocumentRepository, IRiskAssessmentDocumentRepository riskAssessmentDocumentRepository, IAddedDocumentRepository addedDocumentRepository)
        {
            _log = log;
            _documentRepository = documentRepository;
            _userForAuditingRepository = userForAuditingRepository;
            _taskDocumentRepository = taskDocumentRepository;
            _riskAssessmentDocumentRepository = riskAssessmentDocumentRepository;
            _addedDocumentRepository = addedDocumentRepository;
        }

        public DocumentDto GetDocument(long documentId, long companyId)
        {
            _log.Add(new object[] {documentId, companyId});

            try
            {
                var document = _documentRepository.GetByIdAndCompanyId(documentId, companyId);
                return new DocumentDtoMapper().Map(document);
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public IEnumerable<DocumentDto> Search(SearchDocumentRequest request)
        {
            _log.Add(request);

            //todo: refactor to use document repository. When you do, put in restrictions on allowed sites.
            //var documents = _documentRepository.Search(request.CompanyId, request.TitleLike, request.DocumentTypeId,request.SiteId);
            //return new DocumentDtoMapper().Map(documents);

            //This really isn't going to work if we need to start paging documents.
            var documents = _addedDocumentRepository.Search(request.CompanyId, request.TitleLike, request.DocumentTypeId, request.SiteId, request.SiteGroupId, request.AllowedSiteIds)
                .Select(d => (Document)d)
                .ToList();
            documents.AddRange(_taskDocumentRepository.Search(request.CompanyId, request.TitleLike, request.DocumentTypeId, request.SiteId, request.SiteGroupId, request.AllowedSiteIds));
            documents.AddRange(_riskAssessmentDocumentRepository.Search(request.CompanyId, request.TitleLike, request.DocumentTypeId, request.SiteId, request.SiteGroupId, request.AllowedSiteIds));

            return new DocumentDtoMapper().Map(documents);

        }

        public void MarkDocumentAsDeleted(MarkDocumentAsDeletedRequest request)
        {
            _log.Add(request);

            try
            {
                var document = _documentRepository.GetByIdAndCompanyId(request.DocumentId, request.CompanyId);
                var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
                document.MarkForDelete(user);
                _documentRepository.SaveOrUpdate(document);

            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public void ValidateDocumentForCompany(long documentLibraryId, long companyId)
        {
            try
            {
                _documentRepository.GetByDocumentLibraryIdAndCompanyId(documentLibraryId, companyId);
            }
            catch (Exception)
            {
                throw new InvalidDocumentForCompanyException(documentLibraryId, companyId);
            }
        }
    }
}