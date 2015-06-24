using System;
using System.Collections.Generic;
using BusinessSafe.Application.Request.Documents;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Request
{
    public class SaveFurtherControlMeasureTaskRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Reference { get; set; }
        public long CompanyId { get; set; }
        public long RiskAssessmentId { get; set; }
        public long RiskAssessmentHazardId { get; set; }
        public Guid UserId { get; set; }
        public Guid TaskAssignedToId { get; set; }
        public DateTime? TaskCompletionDueDate { get; set; }
        public int FurtherControlMeasureTaskCategoryId { get; set; }
        public TaskStatus TaskStatus { get; set; }
        public List<CreateDocumentRequest> CreateDocumentRequests { get; set; }
        public List<long> DocumentLibraryIdsToDelete { get; set; }
        public string CompletedComments { get; set; }
        public int TaskReoccurringTypeId { get; set; }
        public DateTime? TaskReoccurringEndDate { get; set; }
        public bool SendTaskNotification { get; set; }
        public bool SendTaskCompletedNotification { get; set; }
        public bool SendTaskOverdueNotification { get; set; }
        public bool SendTaskDueTomorrowNotification { get; set; }
        public Guid TaskGuid { get; set; }
        public bool IsReoccurringTask
        {
            get { return TaskReoccurringTypeId > 0; }
        }

        public SaveFurtherControlMeasureTaskRequest()
        {
            CreateDocumentRequests = new List<CreateDocumentRequest>();
            DocumentLibraryIdsToDelete = new List<long>();            
        }

        public static SaveFurtherControlMeasureTaskRequest Create(
            string title,
            string description, 
            string reference,
            string taskCompletionDueDate,
            int taskStatusId,
            long companyId,
            long riskAssessmentId,
            long riskAssessmentHazardId,
            Guid taskAssignedToId,
            int furtherControlMeasureTaskCategoryId,
            int taskReoccurringTypeId,
            string taskReoccurringFirstDueDate,
            DateTime? taskReoccurringEndDate,
            bool sendTaskNotification,
            bool sendTaskCompletedNotification,
            bool sendTaskOverdueNotification,
            bool sendTaskDueTomorrowNotification,
            Guid userId,
            List<CreateDocumentRequest> createDocumentRequests,
            Guid taskGuid
            )
        {
            var request = new SaveFurtherControlMeasureTaskRequest();

            request.Title = title;
            request.Description = description;
            request.Reference = reference;
            request.TaskStatus = (TaskStatus)taskStatusId;
            request.CompanyId = companyId;
            request.RiskAssessmentId = riskAssessmentId;
            request.RiskAssessmentHazardId = riskAssessmentHazardId;
            request.TaskAssignedToId = taskAssignedToId;
            request.FurtherControlMeasureTaskCategoryId = furtherControlMeasureTaskCategoryId;
            request.TaskReoccurringTypeId = taskReoccurringTypeId;
            request.TaskReoccurringEndDate = taskReoccurringEndDate;
            request.SendTaskNotification = sendTaskNotification;
            request.SendTaskCompletedNotification = sendTaskCompletedNotification;
            request.SendTaskOverdueNotification = sendTaskOverdueNotification;
            request.SendTaskDueTomorrowNotification = sendTaskDueTomorrowNotification;
            request.UserId = userId;

            if (request.IsReoccurringTask)
            {
                request.TaskCompletionDueDate = string.IsNullOrEmpty(taskReoccurringFirstDueDate)
                                        ? (DateTime?)null
                                        : DateTime.Parse(taskReoccurringFirstDueDate);
            }
            else
            {
                request.TaskCompletionDueDate = string.IsNullOrEmpty(taskCompletionDueDate)
                                            ? (DateTime?)null
                                            : DateTime.Parse(taskCompletionDueDate);
            }

            request.CreateDocumentRequests.AddRange(createDocumentRequests);
            request.TaskGuid = taskGuid;
            return request;
        }

        
    }
}