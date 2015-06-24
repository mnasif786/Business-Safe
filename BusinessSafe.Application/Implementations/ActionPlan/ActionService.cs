using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Contracts.ActionPlan;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Helpers;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.ParameterClasses;
using BusinessSafe.Domain.RepositoryContracts;
using BusinessSafe.Messages.Commands;
using BusinessSafe.Messages.Events;
using Action = BusinessSafe.Domain.Entities.Action;
using NServiceBus;

namespace BusinessSafe.Application.Implementations.ActionPlan
{
    public class ActionService : IActionService
    {
        private readonly ITasksRepository _tasksRepository;
        private readonly IActionRepository _actionRepository;
        private readonly IUserForAuditingRepository _userForAuditingRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ISiteRepository _siteRepository;
        private readonly ITaskCategoryRepository _taskCategoryRepository;
        private readonly IPeninsulaLog _log;
        private readonly IBus _bus;
        private readonly IDocumentParameterHelper _documentParameterHelper;

        public ActionService(IActionRepository actionRepository, IUserForAuditingRepository userRepository,
                             IEmployeeRepository employeeRepository, ISiteRepository siteRepository,
                             ITaskCategoryRepository taskCategoryRepository, IPeninsulaLog log, IBus bus, ITasksRepository tasksRepository, IDocumentParameterHelper documentParameterHelper)
        {
            _actionRepository = actionRepository;
            _userForAuditingRepository = userRepository;
            _employeeRepository = employeeRepository;
            _siteRepository = siteRepository;
            _taskCategoryRepository = taskCategoryRepository;
            _log = log;
            _bus = bus;
            _tasksRepository = tasksRepository;
            _documentParameterHelper = documentParameterHelper;
        }

        public IEnumerable<ActionDto> Search(SearchActionRequest request)
        {
            var actions = _actionRepository.GetAll();
            return new ActionDtoMapper().WithStatus().Map(actions);
        }

        public void AssignActionTask(AssignActionTaskRequest request)
        {
            _log.Add(request);

            try
            {
                var action = _actionRepository.GetById(request.ActionId);
                var user = _userForAuditingRepository.GetById(request.UserId);
                var employee = _employeeRepository.GetById(request.AssignedTo);

                Site site = null;
                if (action.ActionPlan.Site != null)
                {
                    site = _siteRepository.GetById(action.ActionPlan.Site.Id);
                }
                var taskCategory = _taskCategoryRepository.GetActionTaskCategory();

                action.AssignedTo = employee;

                if (!string.IsNullOrEmpty(request.AreaOfNonCompliance))
                    action.AreaOfNonCompliance = request.AreaOfNonCompliance;
                if (!string.IsNullOrEmpty(request.ActionRequired))
                    action.ActionRequired = request.ActionRequired;

                UpdateActionTask(request, employee, user, taskCategory, action, site);
                
                _actionRepository.SaveOrUpdate(action);
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }
        
        private void UpdateActionTask(AssignActionTaskRequest request, Employee employee, UserForAuditing user, TaskCategory taskCategory, Action action, Site site)
        {
            var createDocumentParameterObjects = request.Documents == null ? null : _documentParameterHelper.GetCreateDocumentParameters(request.Documents, request.CompanyId);

            if (action.ActionTasks.Any())
            {
                var task = action.ActionTasks.FirstOrDefault();
                task.Update(action.AreaOfNonCompliance,
                    action.ActionRequired,
                    request.DueDate,
                    TaskStatus.Outstanding,
                    employee,
                    user,
                    createDocumentParameterObjects, 
                    taskCategory,
                    (int) TaskReoccurringType.None,
                                        null,
                    request.SendTaskNotification,
                    request.SendTaskCompletedNotification,
                    request.SendTaskOverdueNotification,
                    request.SendTaskDueTomorrowNotification,
                    task.TaskGuid,
                    site
                    );
            }
            else
            {
                var task = new ActionTask();

                task = ActionTask.Create(
                    action.Reference,
                    action.AreaOfNonCompliance,
                    action.ActionRequired,
                    request.DueDate,
                    TaskStatus.Outstanding,
                    employee,
                    user,
                    createDocumentParameterObjects,
                    taskCategory,
                    (int) TaskReoccurringType.None,
                    null,
                    request.SendTaskNotification,
                    request.SendTaskCompletedNotification,
                    request.SendTaskOverdueNotification,
                    request.SendTaskDueTomorrowNotification,
                    Guid.NewGuid(),
                    site,
                    action
                    );
            }
        }

        public int Count(SearchActionRequest request)
        {
            return _actionRepository.Count(request.ActionPlanId);
        }

        public void SendTaskAssignedEmail(long actionId, long companyId)
        {
            var action = _actionRepository.GetById(actionId);

            var task = _tasksRepository.GetByIdAndCompanyId(action.ActionTasks.First().Id, companyId);

            if (task == null)
            {
                throw LogAndReturnTaskNotFoundException(action.ActionTasks.First().Id,companyId);
            }
            
            _bus.Publish(new TaskAssigned {TaskGuid = task.TaskGuid});
            
        }

        public ActionDto GetByIdAndCompanyId(long actionId, long companyId)
        {
            _log.Add(new object[] { actionId, companyId });
            try
            {

                var action = _actionRepository.GetByIdAndCompanyId(actionId, companyId);
                return new ActionDtoMapper().WithTasks().Map(action);
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        private NullReferenceException LogAndReturnTaskNotFoundException(long taskId, long companyId)
        {
            var e = new NullReferenceException(string.Format("Could not find Action Task with id {0}, belonging to company {1}", taskId, companyId));
            _log.Add(e);
            return e;
        }
    }
}
