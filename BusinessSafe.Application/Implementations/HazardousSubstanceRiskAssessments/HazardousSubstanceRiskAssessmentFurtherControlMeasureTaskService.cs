using BusinessSafe.Application.Contracts.HazardousSubstanceRiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Helpers;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using BusinessSafe.Application.Request.HazardousSubstanceRiskAssessments;
using NServiceBus;
using BusinessSafe.Messages.Events;

namespace BusinessSafe.Application.Implementations.HazardousSubstanceRiskAssessments
{
    public class HazardousSubstanceRiskAssessmentFurtherControlMeasureTaskService : IHazardousSubstanceRiskAssessmentFurtherControlMeasureTaskService
    {
        private readonly IHazardousSubstanceRiskAssessmentRepository _riskAssessmentRepository;
        private readonly IUserForAuditingRepository _userForAuditingRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITaskCategoryRepository _responsibilityTaskCategoryRepository;
        private readonly IPeninsulaLog _log;
        private readonly IDocumentParameterHelper _documentParameterHelper;
        

        public HazardousSubstanceRiskAssessmentFurtherControlMeasureTaskService(
            IHazardousSubstanceRiskAssessmentRepository riskAssessmentRepository,
            IUserForAuditingRepository userForAuditingRepository,
            IEmployeeRepository employeeRepository,
            ITaskCategoryRepository responsibilityTaskCategoryRepository,
            IPeninsulaLog log,
            IDocumentParameterHelper documentParameterHelper
        
            )
        {
            _riskAssessmentRepository = riskAssessmentRepository;
            _userForAuditingRepository = userForAuditingRepository;
            _employeeRepository = employeeRepository;
            _responsibilityTaskCategoryRepository = responsibilityTaskCategoryRepository;
            _log = log;
            _documentParameterHelper = documentParameterHelper;
        
        }

        public HazardousSubstanceRiskAssessmentFurtherControlMeasureTaskDto AddFurtherControlMeasureTask(SaveFurtherControlMeasureTaskRequest request)
        {
            _log.Add(request);

            var creatingUser = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
            var riskAssessment = _riskAssessmentRepository.GetByIdAndCompanyId(request.RiskAssessmentId, request.CompanyId);
            
            var furtherControlMeasureTask = HazardousSubstanceRiskAssessmentFurtherControlMeasureTask.Create(
                request.Reference,
                request.Title,
                request.Description,
                request.TaskCompletionDueDate,
                request.TaskStatus,
                _employeeRepository.GetByIdAndCompanyId(request.TaskAssignedToId, request.CompanyId),
                creatingUser,
                _documentParameterHelper.GetCreateDocumentParameters(request.CreateDocumentRequests, request.CompanyId),
                _responsibilityTaskCategoryRepository.GetHazardousSubstanceRiskAssessmentTaskCategory(),
                request.TaskReoccurringTypeId,
                request.TaskReoccurringEndDate,
                request.SendTaskNotification,
                request.SendTaskCompletedNotification,
                request.SendTaskOverdueNotification,
                request.SendTaskDueTomorrowNotification,
                request.TaskGuid
            );
            
            furtherControlMeasureTask.HazardousSubstanceRiskAssessment = riskAssessment;
            riskAssessment.AddFurtherControlMeasureTask(furtherControlMeasureTask, creatingUser);
            _riskAssessmentRepository.SaveOrUpdate(riskAssessment);
            _riskAssessmentRepository.Flush();
            var dto = new HazardousSubstanceRiskAssessmentFurtherControlMeasureTaskDtoMapper().MapWithAssignedToAndRiskAssessment(furtherControlMeasureTask);
            return dto;
        }
        
    }
}