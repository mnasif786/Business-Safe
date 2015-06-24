using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace BusinessSafe.MessageHandlers.Emails.ViewModels
{
    public class SendEmployeeDigestEmailViewModel
    {
        public string To { get; set; }

        public string Subject { get; set; }

        public string From { get; set; }
        public HtmlString BusinessSafeOnlineLink { get; set; }

        public bool IsAnyOverdueTaskToNotify { get; set; }
        public bool IsAnyCompletedTaskToNotify { get; set; }
        public bool IsAnyDueTomorrowTaskToNotify { get; set; }

        //Overdue Tasks
        public List<TaskDetailsViewModel> OverdueGeneralRiskAssessmentsTasksViewModel { get; set; }
        public List<TaskDetailsViewModel> OverduePersonalRiskAssessmentsTasksViewModel { get; set; }
        public List<TaskDetailsViewModel> OverdueFireRiskAssessmentsTasksViewModel { get; set; }
        public List<TaskDetailsViewModel> OverdueHazardousSubstanceRiskAssessmentsTasksViewModel { get; set; }
        public List<TaskDetailsViewModel> OverdueActionTasksViewModel { get; set; }
        public List<TaskDetailsViewModel> OverdueReviewRiskAssessmentsTasksViewModel { get; set; }
        public List<TaskDetailsViewModel> OverdueResponsibilitiesTasksViewModel { get; set; }


        //Completed Tasks
        public List<TaskDetailsViewModel> CompletedHazardousSubstanceTasksViewModel { get; set; }
        public List<TaskDetailsViewModel> CompletedPersonalRiskAssessmentTasksViewModel { get; set; }
        public List<TaskDetailsViewModel> CompletedGeneralRiskAssessmentTasksViewModel { get; set; }
        public List<TaskDetailsViewModel> CompletedFireRiskAssessmentTasksViewModel { get; set; }
        public List<TaskDetailsViewModel> CompletedReviewRiskAssessmentTasksViewModel { get; set; }


        //Due Tomorrow Tasks
        public List<TaskDetailsViewModel> DueTomorrowGeneralRiskAssessmentsTasksViewModel { get; set; }
        public List<TaskDetailsViewModel> DueTomorrowPersonalRiskAssessmentsTasksViewModel { get; set; }
        public List<TaskDetailsViewModel> DueTomorrowFireRiskAssessmentsTasksViewModel  { get; set; }
        public List<TaskDetailsViewModel> DueTomorrowHazardousSubstanceRiskAssessmentsTasksViewModel { get; set; }
        public List<TaskDetailsViewModel> DueTomorrowActionTasksViewModel { get; set; }
        public List<TaskDetailsViewModel> DueTomorrowReviewRiskAssessmentsTasksViewModel { get; set; }
        public List<TaskDetailsViewModel> DueTomorrowResponsibilitiesTasksViewModel { get; set; }
        
        public SendEmployeeDigestEmailViewModel()
        {
            OverdueHazardousSubstanceRiskAssessmentsTasksViewModel = new List<TaskDetailsViewModel>();
            OverduePersonalRiskAssessmentsTasksViewModel = new List<TaskDetailsViewModel>();
            OverdueFireRiskAssessmentsTasksViewModel = new List<TaskDetailsViewModel>();
            OverdueHazardousSubstanceRiskAssessmentsTasksViewModel = new List<TaskDetailsViewModel>();
            OverdueReviewRiskAssessmentsTasksViewModel = new List<TaskDetailsViewModel>();
            OverdueResponsibilitiesTasksViewModel = new List<TaskDetailsViewModel>();
            OverdueActionTasksViewModel = new List<TaskDetailsViewModel>();

            CompletedGeneralRiskAssessmentTasksViewModel= new List<TaskDetailsViewModel>();
            CompletedHazardousSubstanceTasksViewModel = new List<TaskDetailsViewModel>();
            CompletedPersonalRiskAssessmentTasksViewModel = new List<TaskDetailsViewModel>();
            CompletedFireRiskAssessmentTasksViewModel = new List<TaskDetailsViewModel>();
            CompletedReviewRiskAssessmentTasksViewModel = new List<TaskDetailsViewModel>();

            DueTomorrowGeneralRiskAssessmentsTasksViewModel = new List<TaskDetailsViewModel>();
            DueTomorrowPersonalRiskAssessmentsTasksViewModel = new List<TaskDetailsViewModel>();
            DueTomorrowFireRiskAssessmentsTasksViewModel = new List<TaskDetailsViewModel>();
            DueTomorrowHazardousSubstanceRiskAssessmentsTasksViewModel = new List<TaskDetailsViewModel>();
            DueTomorrowReviewRiskAssessmentsTasksViewModel = new List<TaskDetailsViewModel>();
            DueTomorrowResponsibilitiesTasksViewModel = new List<TaskDetailsViewModel>();
            DueTomorrowActionTasksViewModel = new List<TaskDetailsViewModel>();
        }
    }

    public class TaskDetailsViewModel
    {
        public string TaskReference { get; set; }
        public string RiskAssesmentReference { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string RiskAssessor { get; set; }
        public string TaskAssignedTo { get; set; }
        public string  CompletionDueDate { get; set; }
        public string  CompletedDate { get; set; }
    }
   
}
