using System;
using System.Collections.Generic;
using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.Domain.ParameterClasses;

namespace BusinessSafe.Domain.Entities
{
    public class RiskAssessmentReviewTask : Task
    {
        public virtual RiskAssessmentReview RiskAssessmentReview { get; set; }

        public static new RiskAssessmentReviewTask Create(
            string reference,
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
            RiskAssessmentReview riskAssessmentReview,
            bool sendTaskNotification,
            bool sendTaskCompletedNotification,
            bool sendTaskOverdueNotification,
            bool sendTaskDueTomorrowNotification,
            Guid taskGuid
            )
        {
            var riskAssessmentReviewTask = new RiskAssessmentReviewTask();

            riskAssessmentReviewTask.SetValuesForCreate(
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
                taskGuid,null);

            riskAssessmentReviewTask.RiskAssessmentReview = riskAssessmentReview;

            return riskAssessmentReviewTask;
        }

        public override void Complete(
            string completedComments,
            IEnumerable<CreateDocumentParameters> createDocumentParameterObjects,
            IEnumerable<long> documentLibraryIdsToRemove,
            UserForAuditing userForAuditing,
            User user)
        {
            base.Complete(
                completedComments,
                createDocumentParameterObjects,
                documentLibraryIdsToRemove,
                userForAuditing,
                user);

            //Additional logic for completing RiskAssessmentReviewTask.
            if (!user.Employee.Equals(TaskAssignedTo))
                TaskStatus = TaskStatus.NoLongerRequired;
        }

        protected override Task GetBasisForClone()
        {
            return new RiskAssessmentReviewTask
            {
                RiskAssessmentReview = RiskAssessmentReview
            };
        }

        public override void MarkAsNoLongerRequired(UserForAuditing user)
        {
            if (TaskStatus == TaskStatus.NoLongerRequired)
            {
                throw new AttemptingToMarkAsNoLongerRequiredRiskAssessmentReviewTaskThatIsNoLongerRequiredException(Id);
            }

            TaskStatus = TaskStatus.NoLongerRequired;
            SetLastModifiedDetails(user);
        }

        public override void MarkForDelete(UserForAuditing user)
        {
            // Guard to prevent deleting risk assessment review
            throw new AttemptingToDeleteRiskAssessmentReviewTaskException(Id);
        }
        
        public override void ReassignTask(Employee reassigningTaskTo, UserForAuditing user)
        {
            TaskAssignedTo = reassigningTaskTo;
            SetLastModifiedDetails(user);
        }
        
        public override RiskAssessment RiskAssessment
        {
            get { return RiskAssessmentReview.RiskAssessment; }
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
