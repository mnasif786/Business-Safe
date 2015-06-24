using System;
using System.Collections.Generic;

using BusinessSafe.Application.Request.Documents;

namespace BusinessSafe.Application.Request
{
    public class CompleteTaskRequest
    {
        public long CompanyId { get; set; }
        public long FurtherControlMeasureTaskId { get; set; }
        public string CompletedComments { get; set; }
        public Guid UserId { get; set; }
        public List<CreateDocumentRequest> CreateDocumentRequests { get; set; }
        public List<long> DocumentLibraryIdsToDelete { get; set; }
        public DateTimeOffset CompletedDate { get; set; }

        public CompleteTaskRequest()
        {
            CreateDocumentRequests = new List<CreateDocumentRequest>();
            DocumentLibraryIdsToDelete = new List<long>();            
        }    
    }
}