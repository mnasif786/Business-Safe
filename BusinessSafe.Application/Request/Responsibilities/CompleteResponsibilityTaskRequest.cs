using System;
using System.Collections.Generic;
using BusinessSafe.Application.Request.Documents;

namespace BusinessSafe.Application.Request.Responsibilities
{
    public class CompleteResponsibilityTaskRequest
    {
        public long CompanyId { get; set; }
        public long ResponsibilityTaskId { get; set; }
        public string CompletedComments { get; set; }
        public Guid UserId { get; set; }
        public List<CreateDocumentRequest> CreateDocumentRequests { get; set; }
        public List<long> DocumentLibraryIdsToDelete { get; set; }
        public DateTimeOffset CompletedDate { get; set; }
    }
}
