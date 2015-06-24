using System;
using System.Collections.Generic;
using BusinessSafe.Domain.ParameterClasses;

namespace BusinessSafe.Domain.Entities
{
    public class MultiHazardRiskAssessmentFurtherControlMeasureTask : FurtherControlMeasureTask
    {
        public virtual MultiHazardRiskAssessmentHazard MultiHazardRiskAssessmentHazard { get; set; }

        public static MultiHazardRiskAssessmentFurtherControlMeasureTask Create(
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
            bool sendTaskDueTomorrowNotification,
            Guid taskGuid
            )
        {
            var furtherControlMeasureTask = new MultiHazardRiskAssessmentFurtherControlMeasureTask();

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
                null
                );

            return furtherControlMeasureTask;
        }

        public override RiskAssessment RiskAssessment
        {
            get
            {
                return MultiHazardRiskAssessmentHazard == null ? null : MultiHazardRiskAssessmentHazard.MultiHazardRiskAssessment;
            }
        }

        protected override Task GetBasisForClone()
        {
            return new MultiHazardRiskAssessmentFurtherControlMeasureTask
                        {
                            MultiHazardRiskAssessmentHazard = MultiHazardRiskAssessmentHazard
                        };
        }
    }
}