using System;
using System.Collections.Generic;
using BusinessSafe.Domain.ParameterClasses;

namespace BusinessSafe.Domain.Entities
{
    public class HazardousSubstanceRiskAssessmentFurtherControlMeasureTask : FurtherControlMeasureTask
    {
        public virtual HazardousSubstanceRiskAssessment HazardousSubstanceRiskAssessment { get; set; }

        public static HazardousSubstanceRiskAssessmentFurtherControlMeasureTask Create(
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
            bool sendTaskNotification,
            bool sendTaskCompletedNotification,
            bool sendTaskOverdueNotification,
            bool sendTaskDueForTomorrowNotification,
            Guid taskGuid
            )
        {
            var furtherControlMeasureTask = new HazardousSubstanceRiskAssessmentFurtherControlMeasureTask();

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
                sendTaskDueForTomorrowNotification,
                taskGuid,null);

            return furtherControlMeasureTask;
        }

        protected override Task GetBasisForClone()
        {
            return new HazardousSubstanceRiskAssessmentFurtherControlMeasureTask
                       {
                           HazardousSubstanceRiskAssessment = HazardousSubstanceRiskAssessment
                       };
        }

        public override RiskAssessment RiskAssessment
        {
            get
            {
                //if (HazardousSubstanceRiskAssessment == null)
                //{
                //    throw new ApplicationException(
                //        "Risk Assessment not set on Risk Assessment Further Control Measure Task.");
                //}

                return HazardousSubstanceRiskAssessment;
            }
        }

    }
}