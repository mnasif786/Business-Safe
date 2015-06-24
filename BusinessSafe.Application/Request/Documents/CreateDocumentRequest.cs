using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Request.Documents
{
    public class CreateDocumentRequest
    {
        public long ClientId { get; set; }
        public string Title { get; set; }
        public long SiteId { get; set; }
        public long DocumentLibraryId { get; set; }
        public string Filename { get; set; }
        public string Extension { get; set; }
        public long FilesizeByte { get; set; }
        public string Description { get; set; }
        public DocumentTypeEnum DocumentType { get; set; }
        public DocumentOriginType DocumentOriginType { get; set; }
    }
}