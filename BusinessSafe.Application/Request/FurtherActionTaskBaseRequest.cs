using System;

namespace BusinessSafe.Application.Request
{
    public abstract class FurtherActionTaskBaseRequest
    {
        public long TaskId { get; set; }
        public long CompanyId { get; set; }
        public Guid UserId { get; set; }
    }
}