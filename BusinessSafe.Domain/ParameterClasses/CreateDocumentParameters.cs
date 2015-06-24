using System;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.ParameterClasses
{
    public class CreateDocumentParameters
    {
        public long ClientId;
        public long DocumentLibraryId { get; set; }
        public string Filename { get; set; }
        public string Extension { get; set; }
        public long FilesizeByte { get; set; }
        public string Description { get; set; }
        public DocumentType DocumentType { get; set; }
        public DateTime? CreatedOn { get; set; }
        public UserForAuditing CreatedBy { get; set; }
        public DocumentOriginType DocumentOriginType { get; set; }
    }
}