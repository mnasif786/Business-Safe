using System;

namespace BusinessSafe.Application.Request
{
    public class ReassignTaskToEmployeeRequest: FurtherActionTaskBaseRequest
    {
        public Guid ReassignTaskToId { get; set; }
        public Guid TaskGuid { get; set; }
    }
}