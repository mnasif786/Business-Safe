using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.DataTransferObjects
{
    public class TaskDocumentDto : DocumentDto
    {
        public DocumentOriginType DocumentOriginType { get; set; }
    }
}
