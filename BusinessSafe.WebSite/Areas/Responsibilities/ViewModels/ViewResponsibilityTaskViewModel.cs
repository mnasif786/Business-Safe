using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Areas.Documents.Factories;
using BusinessSafe.WebSite.Areas.Documents.ViewModels;
using BusinessSafe.WebSite.CustomValidators;
using BusinessSafe.WebSite.CustomValidators.FurtherControlMeasureTasks;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.WebSite.Areas.Responsibilities.ViewModels
{
    public class ViewResponsibilityTaskViewModel 
    {
        public long CompanyId { get; set; }
        public long ResponsibilityTaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsRecurring { get; set; }
        public TaskReoccurringType TaskReoccurringType { get; set; }
        public int TaskReoccurringTypeId { get; set; }
        public string ReoccurringStartDate { get; set; }
        public DateTime? ReoccurringEndDate { get; set; }
        public string CompletionDueDate { get; set; }
        public string ResponsibilityTaskSite { get; set; }
        public long? ResponsibilityTaskSiteId { get; set; }
        public string AssignedTo { get; set; }
        public Guid? AssignedToId { get; set; }
        public bool DoNotSendTaskAssignedNotification { get; set; }
        public bool DoNotSendTaskCompletedNotification { get; set; }
        public bool DoNotSendTaskOverdueNotification { get; set; }
        public int TaskStatusId { get; set; }
        public string TaskCompletedDate { get; set; }
        public string TaskCompletedComments { get; set; }
        public ResponsibilitySummaryViewModel ResponsibilitySummary { get; set; }
        public ExistingDocumentsViewModel ExistingDocuments { get; set; }

        public static ViewResponsibilityTaskViewModel CreateFrom(ResponsibilityTaskDto responsibilityTask)
        {
            var viewModel = new ViewResponsibilityTaskViewModel();
            viewModel.CompanyId = responsibilityTask.Responsibility.CompanyId;
            viewModel.ResponsibilityTaskId = responsibilityTask.Id;
            viewModel.Title = responsibilityTask.Title;
            viewModel.Description = responsibilityTask.Description;
            viewModel.IsRecurring = responsibilityTask.IsReoccurring;
            viewModel.TaskReoccurringType = responsibilityTask.TaskReoccurringType;
            viewModel.TaskReoccurringTypeId = (int)responsibilityTask.TaskReoccurringType;
            viewModel.ReoccurringStartDate = responsibilityTask.TaskCompletionDueDate;
            viewModel.ReoccurringEndDate = responsibilityTask.TaskReoccurringEndDate;
            viewModel.CompletionDueDate = responsibilityTask.TaskCompletionDueDate;

            viewModel.ResponsibilityTaskSite = responsibilityTask.Site != null
                                                   ? responsibilityTask.Site.Name
                                                   : string.Empty;

            viewModel.ResponsibilityTaskSiteId = responsibilityTask.Site != null
                                                     ? responsibilityTask.Site.Id
                                                     : default(long);

            viewModel.AssignedTo = responsibilityTask.TaskAssignedTo != null
                                       ? responsibilityTask.TaskAssignedTo.FullName
                                       : string.Empty;

            viewModel.AssignedToId = responsibilityTask.TaskAssignedTo != null
                                         ? responsibilityTask.TaskAssignedTo.Id
                                         : Guid.Empty;

            viewModel.DoNotSendTaskAssignedNotification = !responsibilityTask.SendTaskCompletedNotification;
            viewModel.DoNotSendTaskCompletedNotification = !responsibilityTask.SendTaskCompletedNotification;
            viewModel.DoNotSendTaskOverdueNotification = !responsibilityTask.SendTaskOverdueNotification;
            viewModel.TaskStatusId = responsibilityTask.TaskStatusId;

            viewModel.TaskCompletedDate =
                (string)
                (responsibilityTask.TaskCompletedDate.HasValue
                     ? responsibilityTask.TaskCompletedDate.GetValueOrDefault().ToLocalTime().ToString("g")
                     : null);

            viewModel.TaskCompletedComments = responsibilityTask.TaskCompletedComments;
            viewModel.ExistingDocuments = ExistingDocumentsViewModel.CreateFrom(responsibilityTask.Documents);
            viewModel.ExistingDocuments.CanDeleteDocuments = false;
            viewModel.ExistingDocuments.DocumentTypeId = (int)DocumentTypeEnum.Responsibility;

            viewModel.ResponsibilitySummary = new ResponsibilitySummaryViewModel
            {
                Id = responsibilityTask.Responsibility.Id,
                Title = responsibilityTask.Responsibility.Title,
                Description = responsibilityTask.Responsibility.Description
            };

            return viewModel;
        }
    }
}