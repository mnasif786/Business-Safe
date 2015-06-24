using System;
using BusinessSafe.Application.Request;

//paul's test change

namespace BusinessSafe.Domain.Entities
{
    public class AddedDocument : Document
    {
        public virtual SiteStructureElement Site { get; set; }

        public static AddedDocument Create(CreateAddedDocumentParameters createAddedDocumentRequest)
        {
            return new AddedDocument
                       {
                           ClientId = createAddedDocumentRequest.ClientId,
                           DocumentLibraryId = createAddedDocumentRequest.DocumentLibraryId,
                           Filename = createAddedDocumentRequest.Filename,
                           Extension = createAddedDocumentRequest.Extension,
                           FilesizeByte = createAddedDocumentRequest.FilesizeByte,
                           Title = createAddedDocumentRequest.Title,
                           Description = createAddedDocumentRequest.Description,
                           DocumentType = createAddedDocumentRequest.DocumentType,
                           Site = createAddedDocumentRequest.Site,
                           CreatedBy = createAddedDocumentRequest.CreatedBy,
                           CreatedOn = createAddedDocumentRequest.CreatedOn,
                           Deleted = false
                       };
        }
        
        public override string DocumentReference
        {
            get { return string.Empty; }
        }

        public override string SiteReference
        {
            get
            {
                return Site == null ? "All" : Site.Name;
            }
        }
    }
}