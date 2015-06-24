
using BusinessSafe.Application.Request.Documents;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.Documents.ViewModels;

namespace BusinessSafe.WebSite.Factories
{
    public class CreateDocumentRequestBuilder
    {
        private long _companyId;
        private long _documentLibraryId;
        private DocumentOriginType _documentOriginType;
        private DocumentTypeEnum _documentType;
        private string _filename;
        private long _siteId;

        public CreateDocumentRequestBuilder WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public CreateDocumentRequestBuilder WithDocumentLibraryId(long documentLibraryId)
        {
            _documentLibraryId = documentLibraryId;
            return this;
        }

        public CreateDocumentRequestBuilder WithDocumentOriginTypeId(DocumentOriginType documentOriginType)
        {
            _documentOriginType = documentOriginType;
            return this;
        }

        public CreateDocumentRequestBuilder WithDocumentType(DocumentTypeEnum documentType)
        {
            _documentType = documentType;
            return this;
        }

        public CreateDocumentRequestBuilder WithFilename(string filename)
        {
            _filename = filename;
            return this;
        }

        public CreateDocumentRequestBuilder WithSiteId(long siteId)
        {
            _siteId = siteId;
            return this;
        }

        public CreateDocumentRequest Build()
        {
            return new CreateDocumentRequest
            {
                ClientId = _companyId,
                DocumentLibraryId = _documentLibraryId,
                DocumentOriginType = _documentOriginType,
                DocumentType = _documentType,
                Filename = _filename,
                SiteId = _siteId
            };
        }
    }
}