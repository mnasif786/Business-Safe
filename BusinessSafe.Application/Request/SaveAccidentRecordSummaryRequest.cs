using System;

namespace BusinessSafe.Application.Request
{
    public class SaveAccidentRecordSummaryRequest
    {
        public long CompanyId { get; set; }
        public long AccidentRecordId { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Reference { get; set; }
        public long JurisdictionId { get; set; }
    }
}