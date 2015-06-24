using System;
using System.Collections.Generic;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.Domain.ParameterClasses;

namespace BusinessSafe.Domain.Entities
{
    public class RiskAssessmentReview : Entity<long>
    {
        public virtual string Comments { get; protected set; }
        public virtual RiskAssessment RiskAssessment { get; set; }
        public virtual DateTime? CompletionDueDate { get; set; }
        public virtual Employee ReviewAssignedTo { get; set; }
        public virtual DateTime? CompletedDate { get; set; }
        public virtual Employee CompletedBy { get; set; }
        public virtual RiskAssessmentReviewTask RiskAssessmentReviewTask { get; set; }

        public virtual bool IsReviewOutstanding
        {
            get
            {
                return !CompletedDate.HasValue;
            }
        }

        public virtual void Edit(UserForAuditing user, Employee assignedToEmployee, DateTime completionDueDate)
        {
            ReviewAssignedTo = assignedToEmployee;
            CompletionDueDate = completionDueDate;
            RiskAssessmentReviewTask.TaskAssignedTo = assignedToEmployee;
            RiskAssessmentReviewTask.TaskCompletionDueDate = completionDueDate;
            SetLastModifiedDetails(user);
            RiskAssessment.RecalculateNextReviewDate();
        }

        public virtual void Complete(
            string comments, 
            UserForAuditing completingUserForAuditing, 
            DateTime? nextReviewDate, 
            bool archive,
            IList<CreateDocumentParameters> createDocumentParameters,
            User completingUser)
        {
            if (!archive && !nextReviewDate.HasValue)
                throw new AttemptingToCompleteRiskAssessmentReviewWithoutArchiveOrNextReviewDateSetException(Id);

            if((CompletedDate != null) || (CompletedBy != null))
                throw new AttemptingToCompleteRiskAssessmentReviewThatIsCompletedException(Id);

            Comments = comments;
            CompletedDate = DateTime.Now;
            CompletedBy = completingUser.Employee;

            RiskAssessmentReviewTask.Complete(
                comments, 
                createDocumentParameters,
                new List<long>(),
                completingUserForAuditing, 
                completingUser);

            SetLastModifiedDetails(completingUserForAuditing);

            if (archive)
            {
                RiskAssessment.MarkAsArchived(completingUserForAuditing);
            }
            else
            {
                CreateFollowUp(completingUserForAuditing, ReviewAssignedTo, nextReviewDate.Value);
            }


            RiskAssessment.RecalculateNextReviewDate();
        }

        protected void CreateFollowUp(
            UserForAuditing user,
            Employee assignedToEmployee,
            DateTime completionDueDate)
        {
            var followUpRiskAssessmentReview = new RiskAssessmentReview()
            {
                RiskAssessment = RiskAssessment,
                ReviewAssignedTo = assignedToEmployee,
                CreatedBy = user,
                CreatedOn = DateTime.Now,
                CompletionDueDate = completionDueDate
            };

            RiskAssessment.AddReview(followUpRiskAssessmentReview);

            followUpRiskAssessmentReview.RiskAssessmentReviewTask = RiskAssessmentReviewTask.Create(
                RiskAssessment.Reference,
                RiskAssessment.Title,
                RiskAssessmentReviewTask.Description,
                completionDueDate,
                TaskStatus.Outstanding,
                assignedToEmployee,
                user,
                new List<CreateDocumentParameters>(),
                RiskAssessmentReviewTask.Category,
                (int)TaskReoccurringType.None,
                null,
                followUpRiskAssessmentReview,
                false,
                false,
                false,
                false,
                Guid.NewGuid());
        }

        public override void MarkForDelete(UserForAuditing user)
        {
            base.MarkForDelete(user);

            RiskAssessment.RecalculateNextReviewDate();
        }

    }
}