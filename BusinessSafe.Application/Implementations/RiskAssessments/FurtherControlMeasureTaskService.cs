using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Helpers;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.ParameterClasses;
using BusinessSafe.Domain.RepositoryContracts;
using BusinessSafe.Messages.Emails.Commands;
using NServiceBus;


namespace BusinessSafe.Application.Implementations.RiskAssessments
{
    public class FurtherControlMeasureTaskService : IFurtherControlMeasureTaskService
    {
        private readonly IUserForAuditingRepository _userForAuditingRepository;
        private readonly IPeninsulaLog _log;
        private readonly IDocumentParameterHelper _documentParameterHelper;
        private readonly IFurtherControlMeasureTasksRepository _taskRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMultiHazardRiskAssessmentHazardRepository _multiHazardRiskAssessmentHazardRepository;
        private readonly ITaskCategoryRepository _responsibilityTaskCategoryRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBus _bus;
        private readonly IDocumentTypeRepository _documentTypeRepository;

        public FurtherControlMeasureTaskService(
            IUserForAuditingRepository userForAuditingRepository,
            IFurtherControlMeasureTasksRepository taskRepository,
            IEmployeeRepository employeeRepository,
            IPeninsulaLog log,
            IDocumentParameterHelper documentParameterHelper,
            IMultiHazardRiskAssessmentHazardRepository multiHazardRiskAssessmentHazardRepository,
            ITaskCategoryRepository responsibilityTaskCategoryRepository, IUserRepository userRepository, IBus bus
            , IDocumentTypeRepository documentTypeRepository)
        {
            _userForAuditingRepository = userForAuditingRepository;
            _taskRepository = taskRepository;
            _employeeRepository = employeeRepository;
            _log = log;
            _documentParameterHelper = documentParameterHelper;
            _multiHazardRiskAssessmentHazardRepository = multiHazardRiskAssessmentHazardRepository;
            _responsibilityTaskCategoryRepository = responsibilityTaskCategoryRepository;
            _userRepository = userRepository;
            _bus = bus;
            _documentTypeRepository = documentTypeRepository;
        }

        public void CompleteFurtherControlMeasureTask(CompleteTaskRequest request)
        {
            _log.Add(request);
            var userForAuditing = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId); 
            var user = _userRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
            var furtherControlMeasureTask = _taskRepository.GetByIdAndCompanyId(request.FurtherControlMeasureTaskId, request.CompanyId);
            var createDocumentParameterObjects = _documentParameterHelper.GetCreateDocumentParameters(request.CreateDocumentRequests, request.CompanyId);

            furtherControlMeasureTask.Complete(request.CompletedComments, createDocumentParameterObjects
                , request.DocumentLibraryIdsToDelete, userForAuditing, user, request.CompletedDate);

            _taskRepository.SaveOrUpdate(furtherControlMeasureTask);
        }

        public FurtherControlMeasureTaskDto GetByIdAndCompanyId(long id, long companyId)
        {
            var furtherControlMeasureTask = _taskRepository.GetById(id);
            return (FurtherControlMeasureTaskDto)new TaskDtoMapper().MapWithAssignedToAndHazard(furtherControlMeasureTask);
        }

        public FurtherControlMeasureTaskDto GetByIdIncludeDeleted(long id)
        {
            var furtherControlMeasureTask = _taskRepository.GetByIdIncludeDeleted(id);
            return (FurtherControlMeasureTaskDto)new TaskDtoMapper().MapWithAssignedToAndHazard(furtherControlMeasureTask);
        }


        public void Update(UpdateFurtherControlMeasureTaskRequest request)
        {
            _log.Add(request);

            var furtherControlMeasureTask = _taskRepository.GetById(request.Id);
            Employee assignedTo = null;

            if (request.TaskAssignedToId != default(Guid))
                assignedTo = _employeeRepository.GetByIdAndCompanyId(request.TaskAssignedToId, request.CompanyId);

            var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
            var createDocumentParameterObjects =
                _documentParameterHelper.GetCreateDocumentParameters(request.CreateDocumentRequests,
                                                                    request.CompanyId);

            furtherControlMeasureTask.Update(request.Reference,
                                             request.Title,
                                             request.Description,
                                             request.TaskCompletionDueDate,
                                             request.TaskStatus,
                                             createDocumentParameterObjects,
                                             request.DocumentLibraryIdsToDelete,
                                             request.TaskReoccurringTypeId,
                                             request.TaskReoccurringEndDate,
                                             assignedTo,
                                             user,
                                             request.SendTaskCompletedNotification,
                                             request.SendTaskNotification,
                                             request.SendTaskOverdueNotification,
                                             request.SendTaskDueTomorrowNotification);

            _taskRepository.SaveOrUpdate(furtherControlMeasureTask);

            // flush so when bus checks the task to see if have to send notification, it is up to date
            _taskRepository.Flush();

        }

        public MultiHazardRiskAssessmentFurtherControlMeasureTaskDto AddFurtherControlMeasureTask(SaveFurtherControlMeasureTaskRequest request)
        {
            _log.Add(request);

            var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
            var hazard = _multiHazardRiskAssessmentHazardRepository.GetById(request.RiskAssessmentHazardId);
            var furtherActionTask = CreateRiskAssessmentFurtherActionTask(request, user);
            hazard.AddFurtherActionTask(furtherActionTask, user);

            _multiHazardRiskAssessmentHazardRepository.SaveOrUpdate(hazard);
            _multiHazardRiskAssessmentHazardRepository.Flush();

            var furtherActionTaskDto = new MultiHazardRiskAssessmentFurtherControlMeasureTaskDtoMapper().MapWithAssignedToAndRiskAssessmentHazard(furtherActionTask);
            return furtherActionTaskDto;

        }

        private MultiHazardRiskAssessmentFurtherControlMeasureTask CreateRiskAssessmentFurtherActionTask(SaveFurtherControlMeasureTaskRequest request, UserForAuditing user)
        {
            var assignedTo = GetFurtherActionTaskAssignedTo(request);
            var createDocumentParameterObjects = _documentParameterHelper.GetCreateDocumentParameters(request.CreateDocumentRequests, request.CompanyId);
            var furtherControlMeasureTaskCategory = GetFurtherControlMeasureTaskCategory(request);

            return MultiHazardRiskAssessmentFurtherControlMeasureTask.Create(
                request.Reference,
                request.Title,
                request.Description,
                request.TaskCompletionDueDate,
                request.TaskStatus,
                assignedTo,
                user,
                createDocumentParameterObjects,
                furtherControlMeasureTaskCategory,
                request.TaskReoccurringTypeId,
                request.TaskReoccurringEndDate,
                request.SendTaskNotification,
                request.SendTaskCompletedNotification,
                request.SendTaskOverdueNotification,
                request.SendTaskDueTomorrowNotification,
                request.TaskGuid);
        }

        private TaskCategory GetFurtherControlMeasureTaskCategory(SaveFurtherControlMeasureTaskRequest request)
        {
            return _responsibilityTaskCategoryRepository.GetFutherControlMeasureTaskCategoryById(request.FurtherControlMeasureTaskCategoryId);
        }

        private Employee GetFurtherActionTaskAssignedTo(SaveFurtherControlMeasureTaskRequest request)
        {
            if (request.TaskAssignedToId == Guid.Empty) return null;
            return _employeeRepository.GetByIdAndCompanyId(request.TaskAssignedToId, request.CompanyId);
        }

        /// <summary>
        /// Will send the SendTaskCompletedEmail to the bus for processing if task completed notification is required and risk assessor has an email address
        /// </summary>
        /// <param name="request"></param>
        public void SendTaskCompletedEmail(CompleteTaskRequest request)
        {
            var task = (Task) _taskRepository.GetByIdAndCompanyId(request.FurtherControlMeasureTaskId,
                                                                             request.CompanyId);
            if (task.IsTaskCompletedNotificationRequired())
            {
                _bus.Send(new SendTaskCompletedEmail
                {
                    TaskReference = task.Reference,
                    Title = task.Title,
                    Description = task.Description,
                    RiskAssessorName = task.RiskAssessment.RiskAssessor.Employee.FullName,
                    RiskAssessorEmail = task.RiskAssessment.RiskAssessor.Employee.MainContactDetails != null ? task.RiskAssessment.RiskAssessor.Employee.MainContactDetails.Email : null,
                    TaskAssignedTo = task.TaskAssignedTo != null ? task.TaskAssignedTo.FullName : string.Empty
                });
            }

        }

        public IEnumerable<TaskDocumentDto> AddDocumentsToTask(AddDocumentsToTaskRequest request)
        {
            var furtherControlMeasureTask = _taskRepository.GetByIdAndCompanyId(request.TaskId, request.CompanyId);
            var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
            var documentType = _documentTypeRepository.GetById((long) furtherControlMeasureTask.DefaultDocumentType);

            var docParameters = request.DocumentLibraryIds.Select(docLibFile => new CreateDocumentParameters()
                                                                                    {
                                                                                        ClientId = request.CompanyId,
                                                                                        CreatedBy = user,
                                                                                        CreatedOn = DateTime.Now,
                                                                                        Description = docLibFile.Description
                                                                                        ,
                                                                                        DocumentLibraryId = docLibFile.Id,
                                                                                        DocumentOriginType = DocumentOriginType.TaskCompleted,
                                                                                        DocumentType = documentType,
                                                                                        Filename = docLibFile.Filename,
                                                                                        FilesizeByte = 0
                                                                                    }).ToList();

            furtherControlMeasureTask.AddDocumentsToTask(docParameters, user);
            _taskRepository.SaveOrUpdate(furtherControlMeasureTask);

            //flush so that the new Task documents are given ids
            _taskRepository.Flush();

            var selectAddedFiles = furtherControlMeasureTask.Documents
                .Where(d => docParameters.Select(dp => dp.DocumentLibraryId).Contains(d.DocumentLibraryId));

            return new TaskDocumentDtoMapper().Map(selectAddedFiles);
        }


    }
}