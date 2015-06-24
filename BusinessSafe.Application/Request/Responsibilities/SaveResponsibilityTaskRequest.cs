using System;
using System.Collections.Generic;
using BusinessSafe.Application.Request.Documents;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Request.Responsibilities
{
    public class SaveResponsibilityTaskRequest
    {
        public long CompanyId { get; set; }
        public long ResponsibilityId { get; set; }
        public long TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TaskReoccurringTypeId { get; set; }
        public DateTime? TaskCompletionDueDate { get; set; }
        public DateTime? TaskReoccurringEndDate { get; set; }
        public Guid UserId { get; set; }
        public Guid TaskAssignedToId { get; set; }
        public TaskStatus TaskStatus { get; set; }
        public bool SendTaskNotification { get; set; }
        public bool SendTaskCompletedNotification { get; set; }
        public bool SendTaskOverdueNotification { get; set; }
        public bool SendTaskDueTomorrowNotification { get; set; }
        public Guid TaskGuid { get; protected set; }
        public long SiteId { get; set; }
        public List<CreateDocumentRequest> CreateDocumentRequests { get; set; }
        public long StatutoryResponsibilityTaskTemplateId { get; set; }

        public bool IsReoccurringTask
        {
            get { return TaskReoccurringTypeId > 0; }
        }

        public static SaveResponsibilityTaskRequest Create(long companyId, long responsibilityId, long taskId,
                                                           string title, string description, int taskReoccurringTypeId,
                                                           string taskCompletionDueDate,
                                                           DateTime? taskReoccurringEndDate, Guid userId,
                                                           Guid taskAssignedToId, int taskStatusId,
                                                           string taskReoccurringFirstDueDate, bool sendTaskNotification,
                                                           bool sendTaskCompleteNotification,
                                                           bool sendTaskOverdueNotification, 
                                                           bool sendTaskDueTomorrowNotification,Guid taskGuid, long siteId,
                                                           List<CreateDocumentRequest> createDocumentRequests)
        {
            return Create(companyId, responsibilityId, taskId, title, description, taskReoccurringTypeId,
                          taskCompletionDueDate,
                          taskReoccurringEndDate, userId,
                          taskAssignedToId, taskStatusId,
                          taskReoccurringFirstDueDate, sendTaskNotification,
                          sendTaskCompleteNotification,
                          sendTaskOverdueNotification, sendTaskDueTomorrowNotification, taskGuid, siteId,
                          createDocumentRequests, default(long));
        }

        public static SaveResponsibilityTaskRequest Create(long companyId, long responsibilityId, long taskId,
                                                           string title, string description, int taskReoccurringTypeId,
                                                           string taskCompletionDueDate,
                                                           DateTime? taskReoccurringEndDate, Guid userId,
                                                           Guid taskAssignedToId, int taskStatusId,
                                                           string taskReoccurringFirstDueDate, bool sendTaskNotification,
                                                           bool sendTaskCompleteNotification,
                                                           bool sendTaskOverdueNotification,
                                                           bool sendTaskDueTomorrowNotification,Guid taskGuid, long siteId,
                                                           List<CreateDocumentRequest> createDocumentRequests,
                                                           long statutoryResponsibilityTaskTemplateId)
        {
            var request = new SaveResponsibilityTaskRequest
                          {
                              CompanyId = companyId,
                              ResponsibilityId = responsibilityId,
                              TaskId = taskId,
                              Title = title,
                              Description = description,
                              TaskStatus = (TaskStatus)taskStatusId,
                              TaskAssignedToId = taskAssignedToId,
                              TaskReoccurringTypeId = taskReoccurringTypeId,
                              TaskReoccurringEndDate = taskReoccurringEndDate,
                              UserId = userId,
                              SendTaskNotification = sendTaskNotification,
                              SendTaskCompletedNotification = sendTaskCompleteNotification,
                              SendTaskOverdueNotification = sendTaskOverdueNotification,
                              SendTaskDueTomorrowNotification = sendTaskDueTomorrowNotification,
                              TaskGuid = taskGuid,
                              SiteId = siteId,
                              CreateDocumentRequests = createDocumentRequests,
                              StatutoryResponsibilityTaskTemplateId = statutoryResponsibilityTaskTemplateId
                          };

            if (request.IsReoccurringTask)
            {
                request.TaskCompletionDueDate = string.IsNullOrEmpty(taskReoccurringFirstDueDate)
                                                    ? (DateTime?) null
                                                    : DateTime.Parse(taskReoccurringFirstDueDate);
            }
            else
            {
                request.TaskCompletionDueDate = string.IsNullOrEmpty(taskCompletionDueDate)
                                                    ? (DateTime?) null
                                                    : DateTime.Parse(taskCompletionDueDate);
            }
            return request;
        }

        
    }
}