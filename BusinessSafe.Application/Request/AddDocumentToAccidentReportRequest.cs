using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessSafe.Application.Request
{
    public class AddDocumentToAccidentReportRequest
    {
        public long AccidentRecordId { get; set; }
        public long CompanyId { get; set; }
        public Guid UserId { get; set; }
        public List<DocumentLibraryFile> DocumentLibraryIds { get; set; }
    }

    public class DocumentLibraryFile
    {
        public long Id { get; set; }
        /// <summary>
        /// Filename must include the file extension e.g. Test.jpg
        /// </summary>
        public string Filename { get; set; }

        /// <summary>
        /// This is nullable
        /// </summary>
        public string Description { get; set; }
    }
}
