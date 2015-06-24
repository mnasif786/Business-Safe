using System;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Request
{
    public class CreateAddedDocumentParameters
    {
        public long ClientId;
        public long DocumentLibraryId;
        public string Filename;
        public string Extension;
        public long FilesizeByte;
        public string Description;
        public string Title;
        public DocumentType DocumentType;
        public SiteStructureElement Site;
        public UserForAuditing CreatedBy;  
        public DateTime? CreatedOn;
    }
}