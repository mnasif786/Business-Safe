using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.Documents.ViewModels;

namespace BusinessSafe.WebSite.Areas.ActionPlans.ViewModels
{
    public class ViewActionTaskViewModel
    {
        public long CompanyId { get; set; }
        public long ActionTaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsRecurring { get; set; }
        public TaskReoccurringType TaskReoccurringType { get; set; }
        public int TaskReoccurringTypeId { get; set; }
        public string ReoccurringStartDate { get; set; }
        public DateTime? ReoccurringEndDate { get; set; }
        public string CompletionDueDate { get; set; }
        public string ActionTaskSite { get; set; }
        public long? ActionTaskSiteId { get; set; }
        public string AssignedTo { get; set; }
        public Guid? AssignedToId { get; set; }
        public bool DoNotSendTaskAssignedNotification { get; set; }
        public bool DoNotSendTaskCompletedNotification { get; set; }
        public bool DoNotSendTaskOverdueNotification { get; set; }
        public bool DoNotSendTaskDueTomorrowNotification { get; set; }
        public int TaskStatusId { get; set; }
        public string TaskCompletedDate { get; set; }
        public string TaskCompletedComments { get; set; }
        public ActionSummaryViewModel ActionSummary { get; set; }

        public ExistingDocumentsViewModel ExistingDocuments { get; set; }
      
    }
}