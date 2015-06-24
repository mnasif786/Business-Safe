using System;
using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.Documents.Factories;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Factories;
using BusinessSafe.WebSite.Extensions;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Factories
{
    public class ViewFurtherControlMeasureTaskViewModelFactory : IViewFurtherControlMeasureTaskViewModelFactory
    {
        private readonly IFurtherControlMeasureTaskService _furtherControlMeasureTaskService;
        private readonly IExistingDocumentsViewModelFactory _existingDocumentsViewModelFactory;
        private long _companyId;
        private long _furtherControlMeasureTaskId;

        public ViewFurtherControlMeasureTaskViewModelFactory(
            IFurtherControlMeasureTaskService furtherControlMeasureTaskService,
            IExistingDocumentsViewModelFactory existingDocumentsViewModelFactory)
        {
            _furtherControlMeasureTaskService = furtherControlMeasureTaskService;
            _existingDocumentsViewModelFactory = existingDocumentsViewModelFactory;
        }

        public IViewFurtherControlMeasureTaskViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public IViewFurtherControlMeasureTaskViewModelFactory WithFurtherControlMeasureTaskId(long furtherControlMeasureTaskId)
        {
            _furtherControlMeasureTaskId = furtherControlMeasureTaskId;
            return this;
        }

        public ViewFurtherControlMeasureTaskViewModel GetViewModel()
        {
            var furtherControlMeasureTask =
                _furtherControlMeasureTaskService.GetByIdIncludeDeleted(_furtherControlMeasureTaskId);

            return GetViewModel(furtherControlMeasureTask);
        }

        public ViewFurtherControlMeasureTaskViewModel GetViewModel(FurtherControlMeasureTaskDto furtherControlMeasureTask)
        {
            var viewModel = new ViewFurtherControlMeasureTaskViewModel
                       {
                           FurtherControlMeasureTaskId = furtherControlMeasureTask.Id,
                           Reference = furtherControlMeasureTask.Reference,
                           Title = furtherControlMeasureTask.Title,
                           TaskDescription = furtherControlMeasureTask.Description,
                           TaskStatusId = furtherControlMeasureTask.TaskStatusId,
                           TaskAssignedToId =
                               furtherControlMeasureTask.TaskAssignedTo != null
                                   ? furtherControlMeasureTask.TaskAssignedTo.Id
                                   : new Guid(),
                           TaskAssignedTo =
                               furtherControlMeasureTask.TaskAssignedTo != null
                                   ? furtherControlMeasureTask.TaskAssignedTo.FullName
                                   : "",
                           TaskCompletionDueDate = furtherControlMeasureTask.TaskCompletionDueDate,
                           IsReoccurring = furtherControlMeasureTask.IsReoccurring,
                           TaskReoccurringTypeId = (int)furtherControlMeasureTask.TaskReoccurringType,
                           TaskReoccurringType = furtherControlMeasureTask.TaskReoccurringType,
                           TaskReoccurringEndDate = furtherControlMeasureTask.TaskReoccurringEndDate,
                           TaskReoccurringTypes = new TaskReoccurringType().ToSelectList(),
                           TaskCompletedComments = furtherControlMeasureTask.TaskCompletedComments,
                           TaskCompletedDate = furtherControlMeasureTask.TaskCompletedDate.HasValue ? furtherControlMeasureTask.TaskCompletedDate.Value.ToLocalShortDateString() :  null,
                           TaskCompletedBy = furtherControlMeasureTask.TaskCompletedBy != null ? furtherControlMeasureTask.TaskCompletedBy.FullName : string.Empty,
                           DoNotSendTaskNotification = !furtherControlMeasureTask.SendTaskNotification,
                           DoNotSendTaskCompletedNotification =!furtherControlMeasureTask.SendTaskCompletedNotification,
                           DoNotSendTaskOverdueNotification = !furtherControlMeasureTask.SendTaskOverdueNotification,
                           ExistingDocuments = _existingDocumentsViewModelFactory
                                                            .WithCanDeleteDocuments(false)
                                                            .WithDefaultDocumentType(furtherControlMeasureTask.DefaultDocumentType)
                                                            .GetViewModel(furtherControlMeasureTask.Documents)
                       };

            return viewModel;
        }
    }
}