using System;
using BusinessSafe.Domain.Entities;
using System.Collections.Generic;

namespace BusinessSafe.Application.DataTransferObjects
{
    public class TaskDto
    {
        public TaskDto()
        {
            Documents = new List<TaskDocumentDto>();
        }
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Reference { get; set; }
        public EmployeeDto TaskAssignedTo { get; set; }
        public SiteStructureElementDto Site { get; set; }
        public string TaskCompletionDueDate { get; set; }
        public TaskStatus TaskStatus { get; set; }
        public string TaskStatusString { get; set; }
        public DerivedTaskStatusForDisplay DerivedDisplayStatus { get; set; }
        public string TaskCompletedComments { get; set; }
        public virtual DateTimeOffset? TaskCompletedDate { get; set; }    //todo: ptd make sure the mapper maps this.
        public TaskCategoryDto TaskCategory { get; set; }
        public int TaskStatusId { get; set; }   //todo: ptd ensure this is not used anywhere then remove - we already have task status.
        public string CreatedDate { get; set; }
        public bool Deleted { get; set; }
        public IEnumerable<TaskDocumentDto> Documents { get; set; }
        public TaskReoccurringType TaskReoccurringType { get; set; }
        public DateTime? TaskReoccurringEndDate { get; set; }
        public bool IsReoccurring { get; set; }
        public RiskAssessmentDto RiskAssessment { get; set; }
        public DocumentTypeEnum DefaultDocumentType { get; set; }
        public bool SendTaskNotification { get; set; }
        public bool SendTaskCompletedNotification { get; set; }
        public bool SendTaskOverdueNotification { get; set; }
        public bool SendTaskDueTomorrowNotification { get; set; }
        public Guid TaskGuid { get; set; }
        public UserEmployeeDto TaskCompletedBy { get; set; }
    }
}