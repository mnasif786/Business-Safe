using System;
using System.Collections.Generic;
using BusinessSafe.Application.Request.Documents;
using BusinessSafe.WebSite.Areas.ActionPlans.ViewModels;

namespace BusinessSafe.WebSite.Areas.ActionPlans.Tasks
{
    public interface IAssignActionPlanTaskCommand
    {
        IAssignActionPlanTaskCommand WithCompanyId(long companyId);
        IAssignActionPlanTaskCommand WithUserId(Guid userId);
        IAssignActionPlanTaskCommand WithActionId(long actionId);
        IAssignActionPlanTaskCommand WithAssignedTo(Guid? assignedTo);
        IAssignActionPlanTaskCommand WithDueDate(string dueDate);
        IAssignActionPlanTaskCommand WithSendTaskNotification(bool send);
        IAssignActionPlanTaskCommand WithSendTaskCompletedNotification(bool send);
        IAssignActionPlanTaskCommand WithSendTaskOverdueNotification(bool send);
        IAssignActionPlanTaskCommand WithSendTaskDueTomorrowNotification(bool send);
        IAssignActionPlanTaskCommand WithAreaOfNonCompliance(string title);
        IAssignActionPlanTaskCommand WithActionRequired(string description);
        IAssignActionPlanTaskCommand WithDocuments(List<CreateDocumentRequest> createDocumentRequests);
        void Execute();
    }
}