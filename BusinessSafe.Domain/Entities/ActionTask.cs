using System;
using System.Collections.Generic;
using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.Domain.ParameterClasses;

namespace BusinessSafe.Domain.Entities
{
    public class ActionTask : Task
    {
        public virtual Action Action { get; set; }

        public static ActionTask Create(string reference,
            string title,
            string description,
            DateTime? taskCompletionDueDate,
            TaskStatus taskStatus, 
            Employee assignedTo,
            UserForAuditing user,
            IEnumerable<CreateDocumentParameters> createDocumentParameterObjects,
            TaskCategory taskCategory,
            int taskReoccurringTypeId,
            DateTime? taskReoccurringEndDate,
            bool sendTaskNotification,
            bool sendTaskCompletedNotification,
            bool sendTaskOverdueNotification,
            bool sendTaskDueTomorrowNotification,
            Guid taskGuid,
            Site site,
            Action action)
        {
            var task = new ActionTask();

            task.SetValuesForCreate(
                reference,
                title,
                description,
                taskCompletionDueDate,
                taskStatus,
                assignedTo,
                user,
                createDocumentParameterObjects,
                taskCategory,
                taskReoccurringTypeId,
                taskReoccurringEndDate,
                sendTaskNotification,
                sendTaskCompletedNotification,
                sendTaskOverdueNotification,
                sendTaskDueTomorrowNotification,
                taskGuid,
                site);

            task.Action = action;
            action.AddTask(task);
            action.SetLastModifiedBy(user);
            return task;
        }

        public virtual void Update(
            string title,
            string description,
            DateTime? taskCompletionDueDate,
            TaskStatus taskStatus,
            Employee assignedTo,
            UserForAuditing user,
            IEnumerable<CreateDocumentParameters> createDocumentParameterObjects,
            TaskCategory taskCategory,
            int taskReoccurringTypeId,
            DateTime? taskReoccurringEndDate,
            bool sendTaskNotification,
            bool sendTaskCompletedNotification,
            bool sendTaskOverdueNotification,
            bool sendTaskDueTomorrowNotification,
            Guid taskGuid,
            Site site)
        {
            
            Title = title;
            Description = description;
            TaskCompletionDueDate = taskCompletionDueDate.HasValue ? taskCompletionDueDate.Value : (DateTime?) null;
            TaskAssignedTo = assignedTo;
            TaskStatus = taskStatus;
            CreatedOn = DateTime.Now;
            CreatedBy = user;
            TaskReoccurringEndDate = taskReoccurringEndDate;
            OriginalTask = this;
            LastModifiedBy = user;
            LastModifiedOn = DateTime.Now;
            AddDocumentsToTask(createDocumentParameterObjects, user);
            TaskGuid = taskGuid;
            SendTaskNotification = sendTaskNotification;
            SendTaskCompletedNotification = sendTaskCompletedNotification;
            SendTaskOverdueNotification = sendTaskOverdueNotification;
            SendTaskDueTomorrowNotification = SendTaskDueTomorrowNotification;
            Site = site;
            Action.SetLastModifiedBy(user);
        }


        protected override Task GetBasisForClone()
        {
            return new ActionTask
            {
                Action = Action
            };
        }

        public override void MarkAsNoLongerRequired(UserForAuditing user)
        {
            if (TaskStatus == TaskStatus.NoLongerRequired)
            {
                throw new AttemptingToMarkAsNoLongerRequiredActionTaskThatIsCompletedException(Id);
            }

            TaskStatus = TaskStatus.NoLongerRequired;
            SetLastModifiedDetails(user);
        }

        public override RiskAssessment RiskAssessment
        {
            get { return null; }
        }

        public override bool IsTaskCompletedNotificationRequired()
        {
            return (
                SendTaskCompletedNotification.HasValue
                && SendTaskCompletedNotification.Value
                && Action.AssignedTo != null
                && Action.AssignedTo.HasEmail
                && !string.IsNullOrEmpty(Action.AssignedTo.GetEmail())
            );
        } 

    }
}
