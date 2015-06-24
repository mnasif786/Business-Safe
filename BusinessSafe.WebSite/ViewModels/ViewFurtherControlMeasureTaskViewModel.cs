using System;
using System.Web.Mvc;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.Documents.ViewModels;

namespace BusinessSafe.WebSite.ViewModels
{
    public class ViewFurtherControlMeasureTaskViewModel
    {
        public long FurtherControlMeasureTaskId { get; set; }
        public string Reference { get; set; }
        public string Title { get; set; }
        public string TaskDescription { get; set; }
        public int TaskStatusId { get; set; }
        public string TaskAssignedTo { get; set; }
        public Guid TaskAssignedToId { get; set; }
        public string TaskCompletionDueDate { get; set; }
        public bool IsReoccurring { get; set; }
        public long TaskReoccurringTypeId { get; set; }
        public SelectList TaskReoccurringTypes { get; set; }
        public TaskReoccurringType TaskReoccurringType { get; set; }
        public DateTime? TaskReoccurringEndDate { get; set; }
        public bool DoNotSendTaskNotification { get; set; }
        public bool DoNotSendTaskCompletedNotification { get; set; }
        public bool DoNotSendTaskOverdueNotification { get; set; }
        public ExistingDocumentsViewModel ExistingDocuments { get; set; }
        public string TaskCompletedDate { get; set; }
        public string TaskCompletedComments { get; set; }
        public string TaskCompletedBy { get; set; }
    }
}