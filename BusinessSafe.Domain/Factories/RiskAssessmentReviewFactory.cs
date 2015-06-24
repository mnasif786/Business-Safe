using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using BusinessSafe.Domain.ParameterClasses;

namespace BusinessSafe.Domain.Factories
{
    public class RiskAssessmentReviewFactory
    {
        public static RiskAssessmentReview Create(
            RiskAssessment riskAssessment,
            UserForAuditing user,
            Employee assignedToEmployee,
            DateTime completionDueDate,
            ITaskCategoryRepository responsibilityTaskCategoryRepository,
            bool sendTaskNotification,
            bool sendTaskCompletedNotification,
            bool sendTaskOverdueNotification,
            bool sendTaskDueTomorrowNotification,
            Guid taskGuid
            )
        {
            var result = new RiskAssessmentReview()
            {
                RiskAssessment = riskAssessment,
                ReviewAssignedTo = assignedToEmployee,
                CreatedBy = user,
                CreatedOn = DateTime.Now,
                CompletionDueDate = completionDueDate,
                LastModifiedBy = user,
                LastModifiedOn = DateTime.Now
                
            };

            riskAssessment.AddReview(result);
            TaskCategory taskCategory = null;
            string description = null;

            if(riskAssessment as HazardousSubstanceRiskAssessment != null)
            {
                taskCategory = responsibilityTaskCategoryRepository.GetHazardousSubstanceRiskAssessmentTaskCategory();
                description = "HSRA Review";
            }

            if (riskAssessment as GeneralRiskAssessment != null)
            {
                taskCategory = responsibilityTaskCategoryRepository.GetGeneralRiskAssessmentTaskCategory();
                description = "GRA Review";
            }

            if (riskAssessment as PersonalRiskAssessment != null)
            {
                taskCategory = responsibilityTaskCategoryRepository.GetPersonalRiskAssessmentTaskCategory();
                description = "PRA Review";
            }

            if (riskAssessment as FireRiskAssessment != null)
            {
                taskCategory = responsibilityTaskCategoryRepository.GetFireRiskAssessmentTaskCategory();
                description = "FRA Review";
            }

            if (riskAssessment.LastModifiedBy == null)
            {
                riskAssessment.LastModifiedBy = user;
                riskAssessment.LastModifiedOn = DateTime.Now;
            }

            result.RiskAssessmentReviewTask = RiskAssessmentReviewTask.Create(
                riskAssessment.Reference,
                riskAssessment.Title,
                description,
                completionDueDate,
                TaskStatus.Outstanding,
                assignedToEmployee,
                user,
                new List<CreateDocumentParameters>(),
                taskCategory,
                (int)TaskReoccurringType.None,
                null,
                result,
                sendTaskNotification,
                sendTaskCompletedNotification,
                sendTaskOverdueNotification,
                sendTaskDueTomorrowNotification,
                taskGuid);

            return result;
        }
    }
}
