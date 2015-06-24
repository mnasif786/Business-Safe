using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Areas.Documents.ViewModels;
using BusinessSafe.WebSite.CustomValidators;
using BusinessSafe.WebSite.CustomValidators.FurtherControlMeasureTasks;

namespace BusinessSafe.WebSite.Areas.Responsibilities.ViewModels
{
    [ClassLevelRequired(RequiredProperty = "Title", ErrorMessage = "Title is required")]
    [ClassLevelRequired(RequiredProperty = "Description", ErrorMessage = "Description is required")]
    [ClassLevelRequired(RequiredProperty = "AssignedToId", ErrorMessage = "Task Assigned To is required")]
    [ClassLevelRequired(RequiredProperty = "AssignedTo", ErrorMessage = "Task Assigned To is required")]
    [ClassLevelRequired(RequiredProperty = "ResponsibilityTaskSite", ErrorMessage = "Site is required")]
    [ClassLevelRequired(RequiredProperty = "ResponsibilityTaskSiteId", ErrorMessage = "Site is required")]
    [DateNotInPastIfReoccurring(RequiredProperty = "ReoccurringStartDate", RequiredPropertyDisplayName = "First Due Date")]
    public class CreateUpdateResponsibilityTaskViewModel : IValidatableObject
    {
        public long CompanyId { get; set; }
        public long ResponsibilityId { get; set; }
        public long TaskId { get; set; }
        public int TaskStatusId { get; set; }

        [MaxLength(250, ErrorMessage = "The Title may only be 250 characters in length")]
        public string Title { get; set; }

        [MaxLength(500, ErrorMessage = "The Description may only be 500 characters in length")]
        public string Description { get; set; }

        public string CompletionDueDate { get; set; }

        public int TaskReoccurringTypeId { get; set; }
        public string TaskReoccurringType { get; set; }
        public string ReoccurringStartDate { get; set; }
        public DateTime? ReoccurringEndDate { get; set; }
        public bool IsRecurring { get; set; }
        public long? ResponsibilityTaskSiteId { get; set; }
        public string ResponsibilityTaskSite { get; set; }
        
        public Guid? AssignedToId { get; set; }
        public string AssignedTo { get; set; }

        public bool DoNotSendTaskAssignedNotification { get; set; }
        public bool DoNotSendTaskCompletedNotification { get; set; }
        public bool DoNotSendTaskOverdueNotification { get; set; }
        public bool DoNotSendTaskDueTomorrowNotification { get; set; }

        public ExistingDocumentsViewModel ExistingDocuments { get; set; }

        public long ResponsibilitySiteId { get; set; }
        
        public string ResponsibilitySite { get; set; }

        public bool NotMarkedAsNoLongerRequired()
        {
            return TaskStatusId != (int)Domain.Entities.TaskStatus.NoLongerRequired;
        }

        public CreateUpdateResponsibilityTaskViewModel()
        {
            ExistingDocuments = new ExistingDocumentsViewModel();
        }

        public static CreateUpdateResponsibilityTaskViewModel CreateFrom(long companyId,
            ResponsibilityDto responsibility,
            TaskDto task)
        {
            var model = new CreateUpdateResponsibilityTaskViewModel();
            model.CompanyId = companyId;
            model.ResponsibilityId = responsibility.Id;
            model.ResponsibilitySiteId = responsibility.Site != null ? responsibility.Site.Id : 0;
            model.ResponsibilitySite = responsibility.Site != null ? responsibility.Site.Name : string.Empty;
            model.TaskId = task != null ? task.Id : default(long);
            model.TaskStatusId = task != null ? task.TaskStatusId : default(int);
            model.Title = task != null ? task.Title : string.Empty;
            model.Description = task != null ? task.Description : string.Empty;
            model.IsRecurring = task != null && task.IsReoccurring;
            model.TaskReoccurringTypeId = task != null ? (int) task.TaskReoccurringType : default(int);
            model.TaskReoccurringType = task != null ? task.TaskReoccurringType.ToString() : string.Empty;
            model.CompletionDueDate = task != null ? task.TaskCompletionDueDate : string.Empty;
            model.ReoccurringStartDate = task != null ? task.TaskCompletionDueDate : string.Empty;
            model.ReoccurringEndDate = task != null ? task.TaskReoccurringEndDate : null;
            model.DoNotSendTaskAssignedNotification = task != null && !task.SendTaskCompletedNotification;
            model.DoNotSendTaskCompletedNotification = task != null && !task.SendTaskCompletedNotification;
            model.DoNotSendTaskOverdueNotification = task != null && !task.SendTaskOverdueNotification;
            model.DoNotSendTaskDueTomorrowNotification = task != null && !task.SendTaskDueTomorrowNotification;
            model.ResponsibilityTaskSiteId = task != null && task.Site != null ? task.Site.Id : default(long);
            model.ResponsibilityTaskSite = task != null && task.Site != null ? task.Site.Name : string.Empty;
            model.AssignedTo = task != null && task.TaskAssignedTo != null ? task.TaskAssignedTo.FullName : string.Empty;
            model.AssignedToId = task != null && task.TaskAssignedTo != null ? task.TaskAssignedTo.Id : new Guid();
            return model;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            if (IsRecurring)
            {
                // duplicated error message from DateNotInPastIfReoccurring validator
                //if (string.IsNullOrEmpty(ReoccurringStartDate))
                //{
                //    results.Add(new ValidationResult("Recurring Task First Due Date is required", new[] { "FirstDueDate" }));
                //}

                if (TaskReoccurringTypeId == 0)
                {
                    results.Add(new ValidationResult("Task Recurrence Frequency is required", new[] { "TaskReoccurringTypeId" }));
                }
            }
            else
            {
                if (string.IsNullOrEmpty(CompletionDueDate))
                {
                    results.Add(new ValidationResult("Completion Due Date is required", new[] { "TaskCompletionDueDate" }));
                }
                else
                {
                    var completionDate = DateTime.Parse(CompletionDueDate);
                    if (completionDate < DateTime.Today)
                    {
                        results.Add(new ValidationResult("Completion Due Date must be in the future", new[] { "TaskCompletionDueDate" }));
                    }
                }
            }
            return results;
        }


    }
}