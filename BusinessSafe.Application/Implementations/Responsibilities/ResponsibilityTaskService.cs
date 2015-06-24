using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.Responsibilities;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Helpers;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Application.Request.Responsibilities;
using BusinessSafe.Application.Tests.Implementations.ResponsibilitiesServiceTests;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using BusinessSafe.Messages.Emails.Commands;
using NServiceBus;

namespace BusinessSafe.Application.Implementations.Responsibilities
{
    public class ResponsibilityTaskService : IResponsibilityTaskService
    {
        private readonly IResponsibilityTaskRepository _responsibilityTaskRepository;
        private readonly IDocumentParameterHelper _documentParameterHelper;
        private readonly IUserForAuditingRepository _userForAuditingRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBus _bus;
        private readonly IPeninsulaLog _log;
        private readonly IResponsibilityRepository _responsibilityRepository;

        public ResponsibilityTaskService(IResponsibilityTaskRepository responsibilityTaskRepository, IDocumentParameterHelper documentParameterHelper
            , IUserForAuditingRepository userForAuditingRepository, IUserRepository userRepository
            , IBus bus, IPeninsulaLog log, IResponsibilityRepository responsibilityRepository)
        {
            _responsibilityTaskRepository = responsibilityTaskRepository;
            _documentParameterHelper = documentParameterHelper;
            _userForAuditingRepository = userForAuditingRepository;
            _userRepository = userRepository;
            _bus = bus;
            _log = log;
            _responsibilityRepository = responsibilityRepository;
        }

        public ResponsibilityTaskDto GetByIdAndCompanyId(long responsibilityTaskId, long companyId)
        {
            var responsibilityTask = _responsibilityTaskRepository.GetByIdAndCompanyId(responsibilityTaskId, companyId);

            if(responsibilityTask == null)
            {
                throw LogAndReturnTaskNotFoundException(responsibilityTaskId, companyId);
            }

            return new TaskDtoMapper().MapWithAssignedTo(responsibilityTask) as ResponsibilityTaskDto;
        }

        public void Complete(CompleteResponsibilityTaskRequest request)
        {
            _log.Add(request);
            var userForAuditing = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
            var user = _userRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
            var responsibilityTask = _responsibilityTaskRepository.GetByIdAndCompanyId(request.ResponsibilityTaskId, request.CompanyId);


            if (responsibilityTask == null)
            {
                throw LogAndReturnTaskNotFoundException(request.ResponsibilityTaskId, request.CompanyId);
            }

            var createDocumentParameterObjects = _documentParameterHelper.GetCreateDocumentParameters(request.CreateDocumentRequests, request.CompanyId);

            responsibilityTask.Complete(
                request.CompletedComments, 
                createDocumentParameterObjects,
                request.DocumentLibraryIdsToDelete, 
                userForAuditing, 
                user, 
                request.CompletedDate);

            _responsibilityTaskRepository.Update(responsibilityTask);
        }

        public void SendTaskCompletedNotificationEmail(CompleteResponsibilityTaskRequest request)
        {
            var task = _responsibilityTaskRepository.GetByIdAndCompanyId(request.ResponsibilityTaskId, request.CompanyId);

            if (task == null)
            {
                throw LogAndReturnTaskNotFoundException(request.ResponsibilityTaskId, request.CompanyId);
            }
            if (task.IsTaskCompletedNotificationRequired())
            {
                _bus.Send(new SendResponsibilityTaskCompletedEmail
                {
                    TaskReference = task.Reference,
                    Title = task.Title,
                    Description = task.Description,
                    ResponsibilityOwnerName = task.Responsibility.Owner.FullName,
                    ResponsibilityOwnerEmail = task.Responsibility.Owner.GetEmail() ?? string.Empty,
                    TaskAssignedTo = task.TaskAssignedTo != null ? task.TaskAssignedTo.FullName : string.Empty,
                    CompletedBy = task.TaskCompletedBy.Employee.FullName
                });
            }
        }

        private ResponsibilityTaskNotFoundException LogAndReturnTaskNotFoundException(long taskId, long companyId)
        {
            var e = new ResponsibilityTaskNotFoundException(taskId, companyId);
            _log.Add(e);
            return e;
        }

       
    }
}
