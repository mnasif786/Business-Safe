using System;
using System.Collections.Generic;
using BusinessSafe.Domain.ParameterClasses;

namespace BusinessSafe.Domain.Entities
{
    public class FireRiskAssessmentFurtherControlMeasureTask : FurtherControlMeasureTask
    {

        public virtual SignificantFinding SignificantFinding { get; set; }

        public static FireRiskAssessmentFurtherControlMeasureTask Create(
            string reference, 
            string title, 
            string description, 
            DateTime? taskCompletionDueDate, 
            TaskStatus taskStatus, 
            Employee assignedTo,
            UserForAuditing user, 
            IEnumerable<CreateDocumentParameters> createDocumentParameterObjects, 
            TaskCategory furtherControlMeasureTaskCategory, 
            int taskReoccurringTypeId, 
            DateTime? taskReoccurringEndDate, 
            SignificantFinding significantFinding,
            bool sendTaskNotification,
            bool sendTaskCompletedNotification,
            bool sendTaskOverdueNotification,
            bool sendTaskDueTomorrowNotification,
            Guid taskGuid)
        {
            var furtherControlMeasureTask = new FireRiskAssessmentFurtherControlMeasureTask();
            furtherControlMeasureTask.SignificantFinding = significantFinding;
            furtherControlMeasureTask.SetValuesForCreate(
                reference,
                title,
                description,
                taskCompletionDueDate,
                taskStatus,
                assignedTo,
                user,
                createDocumentParameterObjects,
                furtherControlMeasureTaskCategory,
                taskReoccurringTypeId,
                taskReoccurringEndDate,
                sendTaskNotification,
                sendTaskCompletedNotification,
                sendTaskOverdueNotification,
                sendTaskDueTomorrowNotification,
                taskGuid,
                null);

            return furtherControlMeasureTask;
        }

        public override RiskAssessment RiskAssessment
        {
            get { return SignificantFinding.FireAnswer.FireRiskAssessmentChecklist.FireRiskAssessment; }
        }

        protected override Task GetBasisForClone()
        {
            return new FireRiskAssessmentFurtherControlMeasureTask
                       {
                           SignificantFinding = SignificantFinding
                       };

        }
    }
}