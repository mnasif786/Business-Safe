using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Contracts.ActionPlan;
using BusinessSafe.Application.Custom_Exceptions;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Helpers;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using BusinessSafe.Messages.Emails.Commands;
using NServiceBus;

namespace BusinessSafe.Application.Implementations.ActionPlan
{
    public class ActionTaskService :  IActionTaskService
    {

        private readonly IActionTaskRepository _actionTaskRepository;

        private readonly IDocumentParameterHelper _documentParameterHelper;
        private readonly IUserForAuditingRepository _userForAuditingRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBus _bus;
        private readonly IPeninsulaLog _log;
        private readonly IActionRepository _actionRepository;
        
        public ActionTaskService(   IActionTaskRepository actionTaskRepository, 
                                    IDocumentParameterHelper documentParameterHelper,
                                    IUserForAuditingRepository userForAuditingRepository, 
                                    IUserRepository userRepository,
                                    IBus bus, 
                                    IPeninsulaLog log, 
                                    IActionRepository actionRepository)
        {
            _actionTaskRepository = actionTaskRepository;
            _documentParameterHelper = documentParameterHelper;
            _userForAuditingRepository = userForAuditingRepository;
            _userRepository = userRepository;
            _bus = bus;
            _log = log;
            _actionRepository = actionRepository;
        }

        
        public DataTransferObjects.ActionTaskDto GetByIdAndCompanyId(long actionTaskId, long companyId)
        {
            _log.Add(new object[] { actionTaskId });
            var actionTask = _actionTaskRepository.GetByIdAndCompanyId(actionTaskId, companyId);

            if (actionTask == null)
            {
                throw LogAndReturnTaskNotFoundException(actionTaskId, companyId);
            }

            return new ActionTaskDtoMapper().MapWithAssignedTo(actionTask) as ActionTaskDto;
        }

        public void Complete(Request.CompleteActionTaskRequest request)
        {
            _log.Add(request);
            var userForAuditing = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
            var user = _userRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
            var actionTask = _actionTaskRepository.GetByIdAndCompanyId(request.ActionTaskId, request.CompanyId);

            if (actionTask == null)
            {
                throw LogAndReturnTaskNotFoundException(request.ActionTaskId, request.CompanyId);
            }

            var createDocumentParameterObjects = _documentParameterHelper.GetCreateDocumentParameters(request.CreateDocumentRequests, request.CompanyId);

            actionTask.Complete(
                request.CompletedComments,
                createDocumentParameterObjects,
                request.DocumentLibraryIdsToDelete,
                userForAuditing,
                user,
                request.CompletedDate);

            _actionTaskRepository.Update(actionTask);
        }

        
        public void SendTaskCompletedNotificationEmail(long taskId, long companyId)
        {
            _log.Add(new object[] { taskId, companyId });

            try
            {
                var task = _actionTaskRepository.GetByIdAndCompanyId(taskId, companyId);
                
                if (task == null)
                {
                    throw LogAndReturnTaskNotFoundException(taskId, companyId);
                }

                if (task.IsTaskCompletedNotificationRequired())
                {
                    _bus.Send(new SendActionTaskCompletedEmail {TaskGuid = task.TaskGuid});
                }
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }

        }

        private ActionTaskNotFoundException LogAndReturnTaskNotFoundException(long taskId, long companyId)
        {
            var e = new ActionTaskNotFoundException(taskId, companyId);
            _log.Add(e);
            return e;
        }

    }
}
