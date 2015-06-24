using System;
using System.Collections.Generic;
using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.Domain.ParameterClasses;

namespace BusinessSafe.Domain.Entities
{
    public class ResponsibilityTask : Task
    {
        public virtual Responsibility Responsibility { get; set; }
        public virtual StatutoryResponsibilityTaskTemplate StatutoryResponsibilityTaskTemplateCreatedFrom { get; set; }

        public static ResponsibilityTask Create(string title,
            string description, DateTime? taskCompletionDueDate,
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
            Responsibility responsibility)
        {
            var task = new ResponsibilityTask();

            task.SetValuesForCreate(
                string.Empty,
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

            task.Responsibility = responsibility;
            responsibility.AddTask(task);
            responsibility.SetLastModifiedBy(user);
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
            SetTaskReoccurringType((TaskReoccurringType) taskReoccurringTypeId);
            TaskReoccurringEndDate = taskReoccurringEndDate;
            OriginalTask = this;
            LastModifiedBy = user;
            LastModifiedOn = DateTime.Now;
            AddDocumentsToTask(createDocumentParameterObjects, user);
            TaskGuid = taskGuid;
            SendTaskNotification = sendTaskNotification;
            SendTaskCompletedNotification = sendTaskCompletedNotification;
            SendTaskOverdueNotification = sendTaskOverdueNotification;
            SendTaskDueTomorrowNotification = sendTaskDueTomorrowNotification;
            Site = site;
            Responsibility.SetLastModifiedBy(user);

        }

        private void SetTaskReoccurringType(TaskReoccurringType taskReoccurringType)
        {
            bool hasMultipleFrequenciesBefore = Responsibility.HasMultipleFrequencies;
            TaskReoccurringType = taskReoccurringType;
            bool hasMultipleFrequenciesAfter = Responsibility.HasMultipleFrequencies;

            if ((hasMultipleFrequenciesBefore != hasMultipleFrequenciesAfter) && hasMultipleFrequenciesAfter)
            {
                Responsibility.RaiseOnHasMultipleFrequencyChangeToTrue();
            }
        }

        protected override Task GetBasisForClone()
        {
            return new ResponsibilityTask
            {
                Responsibility = Responsibility
            };
        }

        public override void MarkAsNoLongerRequired(UserForAuditing user)
        {
            if (TaskStatus == TaskStatus.NoLongerRequired)
            {
                throw new AttemptingToMarkAsNoLongerRequiredResponsibilityTaskThatIsCompletedException(Id);
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
                && Responsibility.Owner != null
                && Responsibility.Owner.HasEmail
                && !string.IsNullOrEmpty(Responsibility.Owner.GetEmail())
            );
        } 

        public virtual bool IsStatutoryResponsibilityTask
        {
            get { return StatutoryResponsibilityTaskTemplateCreatedFrom != null; }
        }
    }
}
