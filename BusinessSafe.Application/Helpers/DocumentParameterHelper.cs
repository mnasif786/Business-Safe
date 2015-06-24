using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Request.Documents;
using BusinessSafe.Domain.ParameterClasses;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Application.Helpers
{
    public interface IDocumentParameterHelper
    {
        IEnumerable<CreateDocumentParameters> GetCreateDocumentParameters(
            IEnumerable<CreateDocumentRequest> createDocumentRequests,
            long ClientId);
    }

    public class DocumentParameterHelper : IDocumentParameterHelper
    {
        private readonly IDocumentTypeRepository _documentTypeRepository;

        public DocumentParameterHelper(IDocumentTypeRepository documentTypeRepository)
        {
            _documentTypeRepository = documentTypeRepository;
        }

        public IEnumerable<CreateDocumentParameters> GetCreateDocumentParameters(
            IEnumerable<CreateDocumentRequest> createDocumentRequests,
            long ClientId)
        {
            return (from createDocumentRequest in createDocumentRequests
                    let documentType = _documentTypeRepository.GetById((long)createDocumentRequest.DocumentType)
                    select new CreateDocumentParameters
                               {
                                   ClientId = ClientId,
                                   DocumentLibraryId = createDocumentRequest.DocumentLibraryId,
                                   Filename = createDocumentRequest.Filename,
                                   Extension = createDocumentRequest.Extension,
                                   FilesizeByte = createDocumentRequest.FilesizeByte,
                                   Description = createDocumentRequest.Description,
                                   DocumentType = documentType,
                                   DocumentOriginType = createDocumentRequest.DocumentOriginType
                               }).ToList();
        }
    }
}