using System;
using System.Collections.Generic;
using BusinessSafe.Application.Contracts.ActionPlan;
using BusinessSafe.Application.Contracts.TaskList;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Implementations.TaskList;
using BusinessSafe.Data.CustomExceptions;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.ActionPlans.ViewModels;
using System.Linq;
using BusinessSafe.WebSite.Areas.Documents.Factories;

namespace BusinessSafe.WebSite.Areas.ActionPlans.Factories
{
    public class ReassignActionTaskViewModelFactory : IReassignActionTaskViewModelFactory
    {
        private long _companyId;
        private long _actionId;
        private IActionService _actionService;
        private IExistingDocumentsViewModelFactory _existingDocumentsViewModelFactory;
        private IActionTaskService _actionTaskService;
        
        private long _actionplanId;
        private long _taskId;

        public ReassignActionTaskViewModelFactory(IActionService actionService, IExistingDocumentsViewModelFactory existingDocumentsViewModelFactory, IActionTaskService actionTaskService)
        {
            _actionTaskService = actionTaskService;
            _actionService = actionService;
            _existingDocumentsViewModelFactory = existingDocumentsViewModelFactory;
        }

        public IReassignActionTaskViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public IReassignActionTaskViewModelFactory WithActionPlanId(long actionplanId)
        {
            _actionplanId = actionplanId;
            return this;
        }

        public IReassignActionTaskViewModelFactory WithActionId(long actionId)
        {
            _actionId = actionId;
            return this;
        }

        public IReassignActionTaskViewModelFactory WithTaskId(long taskId)
        {
            _taskId = taskId;
            return this;
        }
        
        public ReassignActionTaskViewModel GetViewModel()
        {
            var action = _actionService.GetByIdAndCompanyId(_actionId, _companyId);
            var task = action.ActionTasks.FirstOrDefault();

            if (task == null)
            {
                throw new TaskNotFoundException(default(long));
            }

            var model = new ReassignActionTaskViewModel
            {
                CompanyId = _companyId,
                ActionPlanId = _actionplanId,
                ActionId = action.Id,
                Title = task.Title,
                Description = task.Description,
                GuidanceNotes = action.GuidanceNote,
                DueDate = task.TaskCompletionDueDate,
                ActionTaskAssignedToId = task.TaskAssignedTo.Id,
                ActionTaskAssignedTo = task.TaskAssignedTo.FullName,
                DoNotSendTaskCompletedNotification = !task.SendTaskCompletedNotification,
                DoNotSendTaskOverdueNotification = !task.SendTaskOverdueNotification,
                DoNotSendTaskAssignedNotification = !task.SendTaskNotification,
                DoNotSendTaskDueTomorrowNotification = !task.SendTaskDueTomorrowNotification
            };

            model.ExistingDocuments = _existingDocumentsViewModelFactory
                    .WithCanDeleteDocuments(false)
                    .WithCanEditDocumentType(true)
                    .WithDefaultDocumentType(DocumentTypeEnum.Action)
                    .WithDocumentOriginType(DocumentOriginType.TaskUpdated)
                    .GetViewModel(task.Documents ?? new List<TaskDocumentDto>());

            return model;
        }

        public ReassignActionTaskViewModel GetViewModelByTask()
        {
            try
            {
                var task = _actionTaskService.GetByIdAndCompanyId(_taskId, _companyId);
            
                var model = new ReassignActionTaskViewModel
                {
                    CompanyId = _companyId,
                    ActionPlanId = task.Action.ActionPlan.Id,
                    ActionId = task.Action.Id,
                    Title = task.Title,
                    Description = task.Description,
                    GuidanceNotes = task.Action.GuidanceNote,
                    DueDate = task.TaskCompletionDueDate,
                    ActionTaskAssignedToId = task.TaskAssignedTo.Id,
                    ActionTaskAssignedTo = task.TaskAssignedTo.FullName,
                    DoNotSendTaskCompletedNotification = !task.SendTaskCompletedNotification,
                    DoNotSendTaskOverdueNotification = !task.SendTaskOverdueNotification,
                    DoNotSendTaskAssignedNotification = !task.SendTaskNotification,
                    DoNotSendTaskDueTomorrowNotification = !task.SendTaskDueTomorrowNotification
                };

                model.ExistingDocuments = _existingDocumentsViewModelFactory
                    .WithCanDeleteDocuments(false)
                    .WithCanEditDocumentType(true)
                    .WithDefaultDocumentType(DocumentTypeEnum.Action)
                    .WithDocumentOriginType(DocumentOriginType.TaskUpdated)
                    .GetViewModel(task.Documents ?? new List<TaskDocumentDto>());

                return model;
            }
            catch (Exception)
            {
                 throw new TaskNotFoundException(default(long));
            }
        }
    }
}