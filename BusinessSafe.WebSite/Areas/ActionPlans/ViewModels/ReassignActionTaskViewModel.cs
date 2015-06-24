using System;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.Documents.ViewModels;

namespace BusinessSafe.WebSite.Areas.ActionPlans.ViewModels
{
    public class ReassignActionTaskViewModel
    {
        public long CompanyId { get; set; }
        public long ActionPlanId { get; set; }
        public long ActionId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string GuidanceNotes { get; set;  }
        public string DueDate { get; set; }
        public Guid? ActionTaskAssignedToId { get; set; }
        public string ActionTaskAssignedTo { get; set; }

		public bool DoNotSendTaskAssignedNotification { get; set; }
        public bool DoNotSendTaskCompletedNotification { get; set; }
        public bool DoNotSendTaskOverdueNotification { get; set; }
        public bool DoNotSendTaskDueTomorrowNotification { get; set; }
		
        public ExistingDocumentsViewModel ExistingDocuments { get; set; }

        public  ReassignActionTaskViewModel()
        {
            ExistingDocuments = new ExistingDocumentsViewModel()
                                    {
                                        DocumentTypeId = (int)DocumentTypeEnum.Action,
                                        DocumentOriginTypeId = (int)DocumentOriginType.TaskUpdated
                                    };
        }
    }
}