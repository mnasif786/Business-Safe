using System;
using System.Collections.Generic;
using BusinessSafe.Application.Contracts.ActionPlan;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Request.Documents;
using BusinessSafe.WebSite.Areas.ActionPlans.ViewModels;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using System.Linq;
namespace BusinessSafe.WebSite.Areas.ActionPlans.Tasks
{
    public class AssignActionPlanTaskCommand : IAssignActionPlanTaskCommand
    {
        private readonly IActionService _actionService;
        private readonly IBusinessSafeSessionManager _businessSafeSessionManager;
        private long _companyId;
        private Guid _userId;
        private long _actionId;
        private Guid? _assignedTo;
        private string _dueDate;
        private bool _sendTaskNotification;
        private bool _sendTaskCompletedNotification;
        private bool _sendTaskOverdueNotification;
        private bool _sendTaskDueTomorrowNotification;
        private List<CreateDocumentRequest> _createDocumentRequests;
        private string _areaOfNonCompliance;
        private string _actionRequired;


        public AssignActionPlanTaskCommand(IActionService actionService, IBusinessSafeSessionManager businessSafeSessionManager )
        {
            _actionService = actionService;
            _businessSafeSessionManager = businessSafeSessionManager;
        }


        public IAssignActionPlanTaskCommand WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public IAssignActionPlanTaskCommand WithUserId(Guid userId)
        {
            _userId = userId;
            return this;
        }

        public IAssignActionPlanTaskCommand WithActionId(long actionId)
        {
            _actionId = actionId;
            return this;
        }

        public IAssignActionPlanTaskCommand WithAssignedTo(Guid? assignedTo)
        {
            _assignedTo = assignedTo;
            return this;
        }

        public IAssignActionPlanTaskCommand WithDueDate(string dueDate)
        {
            _dueDate = dueDate;
            return this;
        }

        public IAssignActionPlanTaskCommand WithSendTaskNotification(bool send)
        {
            _sendTaskNotification = send;
            return this;
        }

        public IAssignActionPlanTaskCommand WithSendTaskCompletedNotification(bool send)
        {
            _sendTaskCompletedNotification = send;
            return this;
        }

        public IAssignActionPlanTaskCommand WithSendTaskOverdueNotification(bool send)
        {
            _sendTaskOverdueNotification = send;
            return this;
        }

        public IAssignActionPlanTaskCommand WithSendTaskDueTomorrowNotification(bool send)
        {
            _sendTaskDueTomorrowNotification = send;
            return this;
        }
        
        public IAssignActionPlanTaskCommand WithAreaOfNonCompliance(string title)
        {
            _areaOfNonCompliance = title;
            return this;
        }

        public IAssignActionPlanTaskCommand WithActionRequired(string description)
        {
            _actionRequired = description;
            return this;
        }

        public IAssignActionPlanTaskCommand WithDocuments(List<CreateDocumentRequest> createDocumentRequests)
        {
            _createDocumentRequests = createDocumentRequests;
            return this;
        }

        public virtual void Execute()
        {
            bool sendEmail = _sendTaskNotification;

            var task = _actionService.GetByIdAndCompanyId(_actionId, _companyId).ActionTasks.FirstOrDefault();
            if (task != null)
            {
                sendEmail = _assignedTo.HasValue && _assignedTo.Value != task.TaskAssignedTo.Id && _sendTaskNotification;
            }

            var dueDate = (DateTime?) null;
            DateTime tempDate;

            if (DateTime.TryParse(_dueDate, out tempDate))
            {
                dueDate = tempDate;
            }

            var request = new AssignActionTaskRequest
                              {
                                  CompanyId = _companyId,
                                  UserId = _userId,
                                  ActionId = _actionId,
                                  AssignedTo = _assignedTo.HasValue ? _assignedTo.Value : Guid.Empty,
                                  DueDate = dueDate,
                                  SendTaskNotification = _sendTaskNotification,
                                  SendTaskCompletedNotification = _sendTaskCompletedNotification,
                                  SendTaskOverdueNotification = _sendTaskOverdueNotification,
                                  SendTaskDueTomorrowNotification = _sendTaskDueTomorrowNotification,
                                  Documents = _createDocumentRequests,
                                  AreaOfNonCompliance = _areaOfNonCompliance,
                                  ActionRequired = _actionRequired
                              };

            using (var session = _businessSafeSessionManager.Session)
            {
                _actionService.AssignActionTask(request);
                _businessSafeSessionManager.CloseSession();
            }

            if (sendEmail)
            {
                using (var session = _businessSafeSessionManager.Session)
                {
                    _actionService.SendTaskAssignedEmail(request.ActionId, request.CompanyId);
                    _businessSafeSessionManager.CloseSession();
                }
            }
        }
    }
}