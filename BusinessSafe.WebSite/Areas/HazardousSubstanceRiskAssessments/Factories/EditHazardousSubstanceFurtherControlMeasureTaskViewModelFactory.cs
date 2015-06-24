using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.Documents.Factories;
using BusinessSafe.WebSite.Extensions;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Factories
{
    public class EditHazardousSubstanceFurtherControlMeasureTaskViewModelFactory : IEditHazardousSubstanceFurtherControlMeasureTaskViewModelFactory
    {
        private readonly IFurtherControlMeasureTaskService _furtherControlMeasureTaskService;
        private readonly IExistingDocumentsViewModelFactory _existingDocumentsViewModelFactory;
        private long _furtherControlMeasureTaskId;
        private long _companyId;
        private bool _canDeleteDocuments;

        public EditHazardousSubstanceFurtherControlMeasureTaskViewModelFactory(
            IFurtherControlMeasureTaskService furtherControlMeasureTaskService,
            IExistingDocumentsViewModelFactory existingDocumentsViewModelFactory)
        {
            _furtherControlMeasureTaskService = furtherControlMeasureTaskService;
            _existingDocumentsViewModelFactory = existingDocumentsViewModelFactory;
        }

        public AddEditFurtherControlMeasureTaskViewModel GetViewModel()
        {

            var furtherActionTaskDto = _furtherControlMeasureTaskService.GetByIdAndCompanyId(_furtherControlMeasureTaskId, _companyId);

            var viewModel = new AddEditFurtherControlMeasureTaskViewModel()
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
                                    DoNotSendTaskDueTomorrowNotification = !furtherActionTaskDto.SendTaskDueTomorrowNotification,
                                    ExistingDocuments = _existingDocumentsViewModelFactory
                                        .WithCanDeleteDocuments(_canDeleteDocuments)
                                        .WithDefaultDocumentType(furtherActionTaskDto.DefaultDocumentType)
                                        .GetViewModel(furtherActionTaskDto.Documents),
                                    IsRecurring = furtherActionTaskDto.IsReoccurring
                                };

            return viewModel;
        }

        public IEditHazardousSubstanceFurtherControlMeasureTaskViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public IEditHazardousSubstanceFurtherControlMeasureTaskViewModelFactory WithFurtherControlMeasureTaskId(long furtherControlMeasureTaskId)
        {
            _furtherControlMeasureTaskId = furtherControlMeasureTaskId;
            return this;
        }

        public IEditHazardousSubstanceFurtherControlMeasureTaskViewModelFactory WithCanDeleteDocuments(bool canDeleteDocuments)
        {
            _canDeleteDocuments = canDeleteDocuments;
            return this;
        }
    }
}