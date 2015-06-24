using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Request.Documents;

namespace BusinessSafe.Application.Request
{
    public class AssignActionTaskRequest
    {
        public long CompanyId { get; set; }
        public Guid UserId { get; set; }
        public long ActionId { get; set; }
        public Guid AssignedTo { get; set; }
        public DateTime? DueDate { get; set; }
        public bool SendTaskNotification { get; set; }
        public bool SendTaskCompletedNotification { get; set; }
        public bool SendTaskOverdueNotification { get; set; }
        public bool SendTaskDueTomorrowNotification { get; set; }
        public List<CreateDocumentRequest> Documents { get; set; }
        public string AreaOfNonCompliance { get; set; }
        public string ActionRequired { get; set; }
    }
}
