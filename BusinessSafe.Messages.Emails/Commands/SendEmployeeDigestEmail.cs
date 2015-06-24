using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;

namespace BusinessSafe.Messages.Emails.Commands
{
    public class SendEmployeeDigestEmail : ICommand
    {
        public string RecipientEmail { get; set; }

        //Overdue tasks
        public List<TaskDetails> GeneralRiskAssessmentsOverdueTasks { get; set; }
        public List<TaskDetails> FireRiskAssessmentsOverdueTasks { get; set; }
        public List<TaskDetails> PersonalRiskAssessmentTasksOverdue { get; set; }
        public List<TaskDetails> HazardousSubstanceRiskAssessmentTasksOverdue { get; set; }
        public List<TaskDetails> RiskAssessmentReviewTasksOverdue { get; set; }
        public List<TaskDetails> ResponsibilitiesTasksOverdue { get; set; }
        public List<TaskDetails> ActionTasksOverdue { get; set; }

        //Completed Tasks
        public List<TaskDetails> HazardousSubstanceTasksCompleted { get; set; }
        public List<TaskDetails> FireRiskAssessmentTasksCompleted { get; set; }
        public List<TaskDetails> GeneralRiskAssessmentTasksCompleted { get; set; }
        public List<TaskDetails> PersonalRiskAssessmentTasksCompleted { get; set; }
        public List<TaskDetails> RiskAssessmentReviewTasksCompleted { get; set; }

        //Due Tomorrow tasks
        public List<TaskDetails> GeneralRiskAssessmentsTasksDueTomorrow { get; set; }
        public List<TaskDetails> FireRiskAssessmentsTasksDueTomorrow { get; set; }
        public List<TaskDetails> PersonalRiskAssessmentTasksDueTomorrow { get; set; }
        public List<TaskDetails> HazardousSubstanceRiskAssessmentTasksDueTomorrow { get; set; }
        public List<TaskDetails> RiskAssessmentReviewTasksDueTomorrow { get; set; }
        public List<TaskDetails> ResponsibilitiesTasksDueTomorrow { get; set; }
        public List<TaskDetails> ActionTasksDueTomorrow { get; set; }

        public SendEmployeeDigestEmail()
        {
            //Overdue tasks
            GeneralRiskAssessmentsOverdueTasks = new List<TaskDetails>();
            FireRiskAssessmentsOverdueTasks = new List<TaskDetails>();
            PersonalRiskAssessmentTasksOverdue = new List<TaskDetails>();
            HazardousSubstanceRiskAssessmentTasksOverdue = new List<TaskDetails>();
            RiskAssessmentReviewTasksOverdue = new List<TaskDetails>();
            ResponsibilitiesTasksOverdue = new List<TaskDetails>();
            ActionTasksOverdue = new List<TaskDetails>();

            //Completed Tasks
            GeneralRiskAssessmentTasksCompleted = new List<TaskDetails>();
            FireRiskAssessmentTasksCompleted = new List<TaskDetails>();
            PersonalRiskAssessmentTasksCompleted = new List<TaskDetails>();
            HazardousSubstanceTasksCompleted = new List<TaskDetails>();
            RiskAssessmentReviewTasksCompleted = new List<TaskDetails>();

            //Due Tomorrow tasks
            GeneralRiskAssessmentsTasksDueTomorrow = new List<TaskDetails>();
            FireRiskAssessmentsTasksDueTomorrow = new List<TaskDetails>();
            PersonalRiskAssessmentTasksDueTomorrow = new List<TaskDetails>();
            HazardousSubstanceRiskAssessmentTasksDueTomorrow = new List<TaskDetails>();
            RiskAssessmentReviewTasksDueTomorrow = new List<TaskDetails>();
            ResponsibilitiesTasksDueTomorrow = new List<TaskDetails>();
            ActionTasksDueTomorrow = new List<TaskDetails>();
        }
    }

    public class TaskDetails
    {
        public string TaskReference { get; set; }
        public string RiskAssesmentReference { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string RiskAssessor { get; set; }
        public string TaskAssignedTo { get; set; }
        public DateTime? CompletionDueDate { get; set; }
        public DateTime? OverDueDate { get; set; }
        public DateTime? CompletedDate { get; set; }
    }
}


