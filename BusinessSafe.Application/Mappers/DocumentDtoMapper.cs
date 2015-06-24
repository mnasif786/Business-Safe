using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class DocumentDtoMapper
    {
        public DocumentDto Map(Document entity)
        {
            DocumentDto dto;
            var furtherControlMeasureDocument = entity as TaskDocument;
            var addedDocument = entity as AddedDocument;
            var accidentRecordDocument = entity as AccidentRecordDocument;

            if(furtherControlMeasureDocument != null)
            {
                dto = new TaskDocumentDto();
                var furtherControlMeasureDocumentDto = dto as TaskDocumentDto;
                furtherControlMeasureDocumentDto.DocumentOriginType = furtherControlMeasureDocument.DocumentOriginType;
            }
            else if(addedDocument != null)
            {
                dto = new AddedDocumentDto();
                var addedDocumentDto = dto as AddedDocumentDto;
                addedDocumentDto.SiteId = addedDocument.Site != null ? addedDocument.Site.Id : default(long);
                addedDocumentDto.SiteName = addedDocument.Site != null ? addedDocument.Site.Name : "All";
            }
            else if(accidentRecordDocument != null)
            {
                dto = new AccidentRecordDocumentDto();
                var accidentRecordDocumentDto = dto as AccidentRecordDocumentDto;
                accidentRecordDocumentDto.AccidentRecord = new AccidentRecordDtoMapper().Map(accidentRecordDocument.AccidentRecord);
            }
            else
            {
                dto = new DocumentDto();
            }

            dto.Id = entity.Id;
            dto.DocumentLibraryId = entity.DocumentLibraryId;
            dto.Filename = entity.Filename;
            dto.Extension = entity.Extension;
            dto.FilesizeByte = entity.FilesizeByte;
            dto.Description = entity.Description;
            dto.Title = entity.Title;
            dto.CreatedOn = entity.CreatedOn;
            dto.CreatedByName = entity.CreatedBy != null ? new AuditedUserDtoMapper().Map(entity.CreatedBy).Name: string.Empty;
            dto.CreatedBy = entity.CreatedBy != null ? new AuditedUserDtoMapper().Map(entity.CreatedBy) : null;
            dto.LastModifiedOn = entity.LastModifiedOn;
            dto.Deleted = entity.Deleted;
            dto.DocumentReference = entity.DocumentReference;
            dto.SiteReference = entity.SiteReference;
            
            if(entity.DocumentType != null)
                dto.DocumentType = new DocumentTypeDtoMapper().Map(entity.DocumentType);

            return dto;
        }

        public IEnumerable<DocumentDto> Map(IEnumerable<Document> entities)
        {
            return entities.Select(Map);
        }
    }
}
