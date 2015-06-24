using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.Documents.Factories;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels;
using BusinessSafe.WebSite.Extensions;

namespace BusinessSafe.WebSite.Areas.FireRiskAssessments.Factories
{
    public class EditFireRiskAssessmentFurtherControlMeasureTaskViewModelFactory : IEditFireRiskAssessmentFurtherControlMeasureTaskViewModelFactory
    {
        private IFurtherControlMeasureTaskService _furtherControlMeasureTaskService;
        private IExistingDocumentsViewModelFactory _existingDocumentsViewModelFactory;
        private long _companyId;
        private long _furtherControlMeasureTaskId;
        private bool _canDeleteDocuments;

        public EditFireRiskAssessmentFurtherControlMeasureTaskViewModelFactory(
            IFurtherControlMeasureTaskService furtherControlMeasureTaskService,
            IExistingDocumentsViewModelFactory existingDocumentsViewModelFactory)
        {
            _furtherControlMeasureTaskService = furtherControlMeasureTaskService;
            _existingDocumentsViewModelFactory = existingDocumentsViewModelFactory;
        }

        public IEditFireRiskAssessmentFurtherControlMeasureTaskViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public IEditFireRiskAssessmentFurtherControlMeasureTaskViewModelFactory WithFurtherControlMeasureTaskId(long furtherControlMeasureTaskId)
        {
            _furtherControlMeasureTaskId = furtherControlMeasureTaskId;
            return this;
        }

        public IEditFireRiskAssessmentFurtherControlMeasureTaskViewModelFactory WithCanDeleteDocuments(bool canDeleteDocuments)
        {
            _canDeleteDocuments = canDeleteDocuments;
            return this;
        }

        public AddEditFireRiskAssessmentFurtherControlMeasureTaskViewModel GetViewModel()
        {
            var furtherActionTaskDto = _furtherControlMeasureTaskService.GetByIdAndCompanyId(_furtherControlMeasureTaskId, _companyId);

            var viewModel = new AddEditFireRiskAssessmentFurtherControlMeasureTaskViewModel()
            {
                FurtherControlMeasureTaskId = furtherActionTaskDto.Id,
                RiskAssessmentTitle = furtherActionTaskDto.RiskAssessment.Title,
                Title = furtherActionTaskDto.Title,
                Reference = furtherActionTaskDto.Reference,
                Description = furtherActionTaskDto.Description,
                TaskStatusId = furtherActionTaskDto.TaskStatusId,
                TaskStatus = furtherActionTaskDto.TaskStatusString,
                CompanyId = _companyId,
                TaskAssignedToId = furtherActionTaskDto.TaskAssignedTo.Id,
                TaskAssignedTo = furtherActionTaskDto.TaskAssignedTo.FullName,
                TaskCompletionDueDate = furtherActionTaskDto.TaskCompletionDueDate,
                CompletedComments = furtherActionTaskDto.TaskCompletedComments,
                TaskReoccurringTypeId = (int)furtherActionTaskDto.TaskReoccurringType,
                TaskReoccurringType = furtherActionTaskDto.TaskReoccurringType,
                TaskReoccurringEndDate = furtherActionTaskDto.TaskReoccurringEndDate,
                TaskReoccurringTypes = new TaskReoccurringType().ToSelectList(),
                DoNotSendTaskNotification = !furtherActionTaskDto.SendTaskNotification,
                DoNotSendTaskCompletedNotification = !furtherActionTaskDto.SendTaskCompletedNotification,
                DoNotSendTaskOverdueNotification = !furtherActionTaskDto.SendTaskOverdueNotification,
                
                ExistingDocuments = _existingDocumentsViewModelFactory
                    .WithCanDeleteDocuments(_canDeleteDocuments)
                    .WithDefaultDocumentType(furtherActionTaskDto.DefaultDocumentType)
                    .GetViewModel(furtherActionTaskDto.Documents),
                IsRecurring = furtherActionTaskDto.IsReoccurring
            };

            return viewModel;
        }
    }
}