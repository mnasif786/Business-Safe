using System;
using System.Collections.Generic;

using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.ClientDocumentService;

namespace BusinessSafe.WebSite.Areas.Documents.ViewModels
{
    public class DocumentViewModel
    {
        public long Id { get; set; }
        public string DocumentType { get; set; }
        public string DocumentSubType { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DateUploaded { get; set; }
        public bool CanDelete { get; set; }
        public string DocumentReference { get; set; }
        public string SiteReference { get; set; }
        public string UploadedBy { get; set; }
        public long DocumentLibraryId { get; set; }

        public static DocumentViewModel CreateFrom(DocumentDto documentDto)
        {
            return new DocumentViewModel()
            {
                Id = documentDto.Id,
                Description = documentDto.Description,
                Title = documentDto.Title,
                DocumentType = documentDto.DocumentType != null ? documentDto.DocumentType.Name: "",
                CanDelete = documentDto is AddedDocumentDto,
                UploadedBy = documentDto.CreatedByName,
                DateUploaded = documentDto.CreatedOn,
                SiteReference = documentDto.SiteReference,
                DocumentReference = documentDto.DocumentReference,
                DocumentLibraryId = documentDto.DocumentLibraryId
            };
        }

        public static DocumentViewModel CreateFrom(ClientDocumentDto clientDocumentDto, IDictionary<long, string> siteReferences)
        {
            return new DocumentViewModel()
            {
                Id = clientDocumentDto.Id.HasValue ? clientDocumentDto.Id.Value : default(long),
                DocumentType = clientDocumentDto.DocumentType != null ? clientDocumentDto.DocumentType.Title : "",
                DocumentSubType = clientDocumentDto.DocumentSubType != null ? clientDocumentDto.DocumentSubType.Title : "",
                Title = clientDocumentDto.Title,
                Description = clientDocumentDto.Description,
                DateUploaded = clientDocumentDto.CreatedOn,
                DocumentLibraryId = clientDocumentDto.DocumentLibraryId.GetValueOrDefault(),
                SiteReference = siteReferences.ContainsKey(clientDocumentDto.SiteId.GetValueOrDefault())? siteReferences[clientDocumentDto.SiteId.GetValueOrDefault()]: ""
            };
        }

        
    }
}