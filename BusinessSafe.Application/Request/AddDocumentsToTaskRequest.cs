using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessSafe.Application.Request
{
    public class AddDocumentsToTaskRequest
    {
        public long TaskId { get; set; }
        public long CompanyId { get; set; }
        public Guid UserId { get; set; }
        public List<DocumentLibraryFile> DocumentLibraryIds { get; set; }
    }

}
