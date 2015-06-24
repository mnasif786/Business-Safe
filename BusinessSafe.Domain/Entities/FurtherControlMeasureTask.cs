using System;
using System.Collections.Generic;
using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.Domain.ParameterClasses;
using System.Linq;

namespace BusinessSafe.Domain.Entities
{
    public abstract class FurtherControlMeasureTask : Task
    {
        public virtual DocumentTypeEnum DefaultDocumentType
        {
            get
            {
                if (this is FireRiskAssessmentFurtherControlMeasureTask)
                {
                    return DocumentTypeEnum.FRADocumentType;
                }
                if (this is HazardousSubstanceRiskAssessmentFurtherControlMeasureTask)
                {
                    return DocumentTypeEnum.HSRADocumentType;
                }
                if (this is MultiHazardRiskAssessmentFurtherControlMeasureTask)
                {
                    return DocumentTypeEnum.GRADocumentType;
                }

                throw new Exception("Document type not recognised for this task implementation");


            }
            set{}
        }

        public virtual void Update(
            string reference, 
            string title, 
            string description, 
            DateTime? taskCompletionDueDate, 
            TaskStatus taskStatus,
            IEnumerable<CreateDocumentParameters> createDocumentParameterObjects,
            IEnumerable<long> documentLibraryIdsToRemove, 
            int taskReoccurringTypeId, 
            DateTime? taskReoccurringEndDate, 
            Employee assignedTo, 
            UserForAuditing user, 
            bool sendTaskCompletedNotification, 
            bool sendTaskNotification, 
            bool sendTaskOverdueNotification,
            bool sendTaskDueTomorrowNotification
            )
        {
            Reference = reference;
            Title = title;
            Description = description;
            TaskCompletionDueDate = taskCompletionDueDate.HasValue ? taskCompletionDueDate.Value : (DateTime?)null;
            TaskAssignedTo = assignedTo;
            TaskStatus = taskStatus;
            TaskReoccurringType = (TaskReoccurringType)taskReoccurringTypeId;
            TaskReoccurringEndDate = taskReoccurringEndDate;
            LastModifiedOn = DateTime.Now;

            SendTaskCompletedNotification = sendTaskCompletedNotification;
            SendTaskNotification = sendTaskNotification;
            SendTaskOverdueNotification = sendTaskOverdueNotification;
            SendTaskDueTomorrowNotification = sendTaskDueTomorrowNotification;

            AddDocumentsToTask(createDocumentParameterObjects, user);
            RemoveDocumentsFromTask(documentLibraryIdsToRemove, user);
            SetLastModifiedDetails(user);
        }

        public override void Complete(
            string completedComments,
            IEnumerable<CreateDocumentParameters> createDocumentParameterObjects,
            IEnumerable<long> documentLibraryIdsToRemove,
            UserForAuditing userForAuditing,
            User user,
            DateTimeOffset completedDate)
        {
            //Addition conditions for completeing a FurtherControlMeasureTask.
            if (TaskStatus == TaskStatus.NoLongerRequired)
            {
                throw new AttemptingToCompleteFurtherControlMeasureTaskThatIsNotRequiredException();
            }

            if (!CanUserComplete(user))
            {
                throw new AttemptingToCompleteFurtherControlMeasureTaskThatTheUserDoesNotHavePermissionToAccess(Id);
            }

            base.Complete(
                completedComments,
                createDocumentParameterObjects,
                documentLibraryIdsToRemove,
                userForAuditing,
                user,
                completedDate);
        }

        public override void MarkForDelete(UserForAuditing user)
        {
            if (TaskStatus == TaskStatus.Completed)
            {
                throw new AttemptingToDeleteFurtherControlMeasureTaskThatIsCompletedException(Id);
            }

            base.MarkForDelete(user);

        }

        public override void MarkAsNoLongerRequired(UserForAuditing user)
        {
            if (TaskStatus == TaskStatus.Completed)
            {
                throw new AttemptingToMarkAsNoLongerRequiredFurtherActionTaskThatIsCompletedException(Id);
            }
            
            TaskStatus = TaskStatus.NoLongerRequired;
            SetLastModifiedDetails(user);
        }

        public virtual bool CanUserComplete(User user)
        {
            if (TaskAssignedTo == null 
                || TaskAssignedTo.Site == null
                || user.Site.GetThisAndAllDescendants()
                    .Select(x=> x.Id )
                    .Contains(TaskAssignedTo.Site.Id)
                || user.Site.GetThisAndAllDescendants()
                    .Select(x => x.Id)
                    .Contains(RiskAssessment.RiskAssessmentSite.Id))
              
            {
                return true;
            }

            return false;
        }

        public override bool IsTaskCompletedNotificationRequired()
        {
            return (
                SendTaskCompletedNotification.HasValue
                && SendTaskCompletedNotification.Value
                && RiskAssessment.RiskAssessor != null
                && RiskAssessment.RiskAssessor.Employee.MainContactDetails != null
                && !string.IsNullOrEmpty(RiskAssessment.RiskAssessor.Employee.MainContactDetails.Email)
            );
        } 
    }
}
