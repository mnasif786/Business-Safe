using System.Linq;
using BusinessSafe.Application.Contracts.FireRiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Helpers;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Application.Request.FireRiskAssessments;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using NServiceBus;


namespace BusinessSafe.Application.Implementations.FireRiskAssessments
{
    public class FireRiskAssessmentFurtherControlMeasureTaskService : IFireRiskAssessmentFurtherControlMeasureTaskService
    {
        private readonly IUserForAuditingRepository _userForAuditingRepository;
        private readonly ISignificantFindingRepository _significantFindingRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDocumentParameterHelper _documentParameterHelper;
        private readonly ITaskCategoryRepository _taskCategoryRepository;
        private readonly IFireAnswerRepository _fireAnswerRepository;
        private readonly IBus _bus;

        public FireRiskAssessmentFurtherControlMeasureTaskService(
            IUserForAuditingRepository userForAuditingRepository,
            ISignificantFindingRepository significantFindingRepository,
            IEmployeeRepository employeeRepository,
            IDocumentParameterHelper documentParameterHelper,
            ITaskCategoryRepository taskCategoryRepository,
            IFireAnswerRepository fireAnswerRepository,
            IBus bus
            )
        {
            _userForAuditingRepository = userForAuditingRepository;
            _significantFindingRepository = significantFindingRepository;
            _employeeRepository = employeeRepository;
            _documentParameterHelper = documentParameterHelper;
            _taskCategoryRepository = taskCategoryRepository;
            _fireAnswerRepository = fireAnswerRepository;
            _bus = bus;
        }

        public FireRiskAssessmentFurtherControlMeasureTaskDto AddFurtherControlMeasureTask(SaveFurtherControlMeasureTaskRequest request)
        {
            var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);

            var significantFinding = _significantFindingRepository.GetById(request.SignificantFindingId);
            var employee = _employeeRepository.GetByIdAndCompanyId(request.TaskAssignedToId, request.CompanyId);
            var createDocumentParameterses = _documentParameterHelper.GetCreateDocumentParameters(request.CreateDocumentRequests, request.CompanyId);
            var taskCategory = _taskCategoryRepository.GetFireRiskAssessmentTaskCategory();

            var task = FireRiskAssessmentFurtherControlMeasureTask.Create(
                request.Reference,
                request.Title,
                request.Description,
                request.TaskCompletionDueDate,
                request.TaskStatus,
                employee,
                user,
                createDocumentParameterses,
                taskCategory,
                request.TaskReoccurringTypeId,
                request.TaskReoccurringEndDate,
                significantFinding,
                request.SendTaskNotification,
                request.SendTaskCompletedNotification,
                request.SendTaskOverdueNotification,
                request.SendTaskDueTomorrowNotification,
                request.TaskGuid
                );

            significantFinding.AddFurtherControlMeasureTask(task, user);
            _significantFindingRepository.SaveOrUpdate(significantFinding);

            return new FireRiskAssessmentFurtherControlMeasureTaskDtoMapper().MapWithAssignedToAndSignificantFinding(task);
        }

        //public virtual FireRiskAssessmentFurtherControlMeasureTaskDto Map(FireRiskAssessmentFurtherControlMeasureTask task)
        //{
        //    return new TaskDtoMapper().Map(task) as FireRiskAssessmentFurtherControlMeasureTaskDto;
        //}

        public GetFurtherControlMeasureTaskCountsForAnswerResponse GetFurtherControlMeasureTaskCountsForAnswer(GetFurtherControlMeasureTaskCountsForAnswerRequest request)
        {
            var answer = _fireAnswerRepository.GetByChecklistIdAndQuestionId(request.FireChecklistId, request.FireQuestionId);

            if (HasNoFurtherControlMeasureTasks(answer))
            {
                return new GetFurtherControlMeasureTaskCountsForAnswerResponse()
                           {
                               TotalFurtherControlMeasureTaskCount = 0,
                               TotalCompletedFurtherControlMeasureTaskCount = 0
                           };
            }

            return new GetFurtherControlMeasureTaskCountsForAnswerResponse()
                       {
                           TotalFurtherControlMeasureTaskCount = answer.SignificantFinding.FurtherControlMeasureTasks.Count(),
                           TotalCompletedFurtherControlMeasureTaskCount = answer.SignificantFinding.FurtherControlMeasureTasks.Count(x => x.TaskStatus == TaskStatus.Completed)
                       };
        }

        private bool HasNoFurtherControlMeasureTasks(FireAnswer answer)
        {
            return answer == null || answer.SignificantFinding == null || answer.SignificantFinding.FurtherControlMeasureTasks.Any(x => x.Deleted == false) == false;
        }
    }
}