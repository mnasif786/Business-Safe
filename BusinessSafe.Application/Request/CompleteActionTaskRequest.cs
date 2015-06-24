using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Request.Documents;

namespace BusinessSafe.Application.Request
{
    public class CompleteActionTaskRequest
    {
        public long CompanyId { get; set; }
        public long ActionTaskId { get; set; }
        public string CompletedComments { get; set; }
        public Guid UserId { get; set; }
        public List<CreateDocumentRequest> CreateDocumentRequests { get; set; }
        public List<long> DocumentLibraryIdsToDelete { get; set; }
        public DateTimeOffset CompletedDate { get; set; }
    }
}
