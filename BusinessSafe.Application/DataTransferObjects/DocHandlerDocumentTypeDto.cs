using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.DataTransferObjects
{
    public class DocHandlerDocumentTypeDto
    {
        public long Id { get; set; }
        public DocHandlerDocumentTypeGroup DocHandlerDocumentTypeGroup { get; set; }
    }
}
