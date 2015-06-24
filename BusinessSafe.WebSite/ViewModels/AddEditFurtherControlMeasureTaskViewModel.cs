using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.Documents.ViewModels;
using BusinessSafe.WebSite.CustomValidators;
using BusinessSafe.WebSite.CustomValidators.FurtherControlMeasureTasks;

namespace BusinessSafe.WebSite.ViewModels
{
    [ClassLevelRequiredAttribute(RequiredProperty = "Title", ErrorMessage = "Title is required")]
    [ClassLevelRequiredAttribute(RequiredProperty = "Description", ErrorMessage = "Description is required")]
    [ClassLevelRequiredAttribute(RequiredProperty = "TaskAssignedToId", ErrorMessage = "Task Assigned To is required")]
    [ClassLevelRequiredAttribute(RequiredProperty = "TaskAssignedTo", ErrorMessage = "Task Assigned To is required")]
    [DateNotInPastIfReoccurring(RequiredProperty = "FirstDueDate", RequiredPropertyDisplayName = "First Due Date")]
    [Bind(Exclude = "TaskReoccurringType")]
    public class AddEditFurtherControlMeasureTaskViewModel : IValidatableObject
    {
        public AddEditFurtherControlMeasureTaskViewModel()
        {
            // Default all email notifications to true
            DoNotSendTaskNotification = false;
            DoNotSendTaskCompletedNotification = false;
            DoNotSendTaskOverdueNotification = false;
        }

        public long FurtherControlMeasureTaskId { get; set; }
        public long CompanyId { get; set; }
        public string RiskAssessmentTitle { get; set; }
        public string Reference { get; set; }
        [StringLength(50)]
        public string Title { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public Guid? TaskAssignedToId { get; set; }
        public string TaskAssignedTo { get; set; }
        public string TaskCompletionDueDate { get; set; }
        public int TaskStatusId { get; set; }
        public string TaskStatus { get; set; }
        [StringLength(250)]
        public string CompletedComments { get; set; }
        public ExistingDocumentsViewModel ExistingDocuments { get; set; }
        public int TaskReoccurringTypeId { get; set; }
        public TaskReoccurringType TaskReoccurringType { get; set; }
        public DateTime? TaskReoccurringEndDate { get; set; }
        public string FirstDueDate { get; set; }
        public bool IsRecurring { get; set; }
        public SelectList TaskReoccurringTypes { get; set; }
        public bool DoNotSendTaskNotification { get; set; }
        public bool DoNotSendTaskCompletedNotification { get; set; }
        public bool DoNotSendTaskOverdueNotification { get; set; }
        public bool DoNotSendTaskDueTomorrowNotification { get; set; }

        public bool NotMarkedAsNoLongerRequired()
        {
            return TaskStatusId != (int)Domain.Entities.TaskStatus.NoLongerRequired;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            if (IsRecurring)
            {
                if(TaskReoccurringTypeId == 0)
                {
                    results.Add(new ValidationResult("Task Recurrence Frequency is required", new[] { "TaskReoccurringTypeId" }));
                }
            }
            else
            {
                if (string.IsNullOrEmpty(TaskCompletionDueDate))
                {
                    results.Add(new ValidationResult("Completion Due Date is required", new[] { "TaskCompletionDueDate" }));
                }
                else
                {
                    var completionDate = DateTime.Parse(TaskCompletionDueDate);
                    if(completionDate < DateTime.Today)
                    {
                        results.Add(new ValidationResult("Completion Due Date must be in the future", new[] { "TaskCompletionDueDate" }));
                    }
                }
            }
            return results;
        }
    }
}