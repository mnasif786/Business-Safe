using System.Collections.Generic;
using BusinessSafe.Application.Request.Documents;

namespace BusinessSafe.WebSite.ViewModels
{
    public class DocumentsToSaveViewModel
    {
        public List<CreateDocumentRequest> CreateDocumentRequests { get; set; }
        public List<long> DeleteDocumentRequests { get; set; }

        public DocumentsToSaveViewModel()
        {
            CreateDocumentRequests = new List<CreateDocumentRequest>();
            DeleteDocumentRequests = new List<long>();
        }
    }
}