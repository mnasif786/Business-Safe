using System;

namespace BusinessSafe.Application.Request.Documents
{
    public class MarkDocumentAsDeletedRequest
    {
        public long DocumentId { get; set; }
        public Guid UserId { get; set; }
        public long CompanyId { get; set; }
    }
}