using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;

namespace BusinessSafe.Application.Contracts.ActionPlan
{
    public interface IActionTaskService
    {
        ActionTaskDto GetByIdAndCompanyId(long actionTaskId, long companyId);
        void Complete(CompleteActionTaskRequest request);
        void SendTaskCompletedNotificationEmail(long taskId, long companyId);
    }
}
