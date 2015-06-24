using System;

namespace BusinessSafe.Application.Request
{
    public class AccidentRecordOverviewRequest
    {
        public long AccidentRecordId { get; set; }
        public long CompanyId { get; set; }
        public Guid UserId { get; set; }
        public string Description { get; set; }
        public bool DoNotSendEmailNotification { get; set; }
    }
}
