using System;
using System.Collections.Generic;
using BusinessSafe.Application.Request.Documents;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Request
{
    public class UpdateFurtherControlMeasureTaskRequest
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Reference { get; set; }
        public long CompanyId { get; set; }
        public Guid UserId { get; set; }
        public Guid TaskAssignedToId { get; set; }
        public DateTime? TaskCompletionDueDate { get; set; }
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

        public bool IsReoccurringTask
        {
            get { return TaskReoccurringTypeId > 0; }
        }

        public UpdateFurtherControlMeasureTaskRequest()
        {
            CreateDocumentRequests = new List<CreateDocumentRequest>();
            DocumentLibraryIdsToDelete = new List<long>();            
        }

        public static UpdateFurtherControlMeasureTaskRequest Create(
            string title,
            string description, 
            string reference, 
            string taskCompletionDueDate, 
            int taskStatusId, 
            long companyId, 
            long id, 
            Guid taskAssignedToId, 
            int taskReoccurringTypeId, 
            string taskReoccurringFirstDueDate, 
            DateTime? taskReoccurringEndDate, 
            Guid userId, 
            List<CreateDocumentRequest> createDocumentRequests, 
            List<long> deleteDocumentRequests,
            bool sendTaskNotification,
            bool sendTaskCompletedNotification,
            bool sendTaskOverdueNotification,
            bool sendTaskDueTomorrowNotification
        )
        {
            var request = new UpdateFurtherControlMeasureTaskRequest
                          {
                              Title = title,
                              Description = description,
                              Reference = reference,
                              TaskStatus = (TaskStatus)taskStatusId,
                              CompanyId = companyId,
                              Id = id,
                              TaskAssignedToId = taskAssignedToId,
                              TaskReoccurringTypeId = taskReoccurringTypeId,
                              TaskReoccurringEndDate = taskReoccurringEndDate,
                              UserId = userId,
                              CreateDocumentRequests = createDocumentRequests,
                              DocumentLibraryIdsToDelete = deleteDocumentRequests,
                              SendTaskCompletedNotification = sendTaskCompletedNotification,
                              SendTaskNotification = sendTaskNotification,
                              SendTaskOverdueNotification = sendTaskOverdueNotification,
                              SendTaskDueTomorrowNotification = sendTaskDueTomorrowNotification
                          };


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

            return request;
        }

    }
}