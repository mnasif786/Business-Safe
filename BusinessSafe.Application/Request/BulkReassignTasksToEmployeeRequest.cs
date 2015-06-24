using System.Collections.Generic;

namespace BusinessSafe.Application.Request
{
    public class BulkReassignTasksToEmployeeRequest
    {
        public IList<ReassignTaskToEmployeeRequest> ReassignRequests { get; set; }
    }
}