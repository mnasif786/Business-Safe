using BusinessSafe.Application.Request.Responsibilities;
using BusinessSafe.Application.DataTransferObjects;
using System.Collections.Generic;

namespace BusinessSafe.Application.Contracts.Responsibilities
{
    public interface IResponsibilityTaskService
    {
        ResponsibilityTaskDto GetByIdAndCompanyId(long responsibilityTaskId, long companyId);
        void Complete(CompleteResponsibilityTaskRequest request);
        void SendTaskCompletedNotificationEmail(CompleteResponsibilityTaskRequest request);
    }
}
