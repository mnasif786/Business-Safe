using System;

namespace BusinessSafe.Application.DataTransferObjects
{
    public class DocumentDto
    {
        public long Id { get; set; }
        public long DocumentLibraryId { get; set; }
        public string Filename { get; set; }
        public string Extension { get; set; }
        public long FilesizeByte { get; set; }
        public string Description { get; set; }
        public DocumentTypeDto DocumentType { get; set; }
        public string Title { get; set; }
        public AuditedUserDto CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public UserDto LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public bool Deleted { get; set; }
        public string DocumentReference { get; set; }
        public string SiteReference { get; set; }
    }
}