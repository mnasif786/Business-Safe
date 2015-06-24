using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using BusinessSafe.MessageHandlers.Emails.Activation;
using BusinessSafe.MessageHandlers.Emails.Controllers;
using BusinessSafe.MessageHandlers.Emails.EmailSender;
using BusinessSafe.MessageHandlers.Emails.ViewModels;
using BusinessSafe.Messages.Emails.Commands;
using NHibernate.Mapping;
using NServiceBus;
using BusinessSafe.Domain.RepositoryContracts.SafeCheck;
using ActionMailer.Net.Standalone;

namespace BusinessSafe.MessageHandlers.Emails.MessageHandlers
{
    public class SendEmployeeDigestEmailHandler : IHandleMessages<SendEmployeeDigestEmail>
    {
        private readonly IEmailSender _emailSender;
        private readonly IBusinessSafeEmailLinkBaseUrlConfiguration _businessSafeEmailLinkBaseUrlConfiguration;

        public SendEmployeeDigestEmailHandler(IEmailSender emailSender, IBusinessSafeEmailLinkBaseUrlConfiguration businessSafeEmailLinkBaseUrlConfiguration)
        {
            _emailSender = emailSender;
            _businessSafeEmailLinkBaseUrlConfiguration = businessSafeEmailLinkBaseUrlConfiguration;
        }

        public void Handle(SendEmployeeDigestEmail message)
        {
            if (HasRecipientEmailAddress(message))
            {
                var emailLink = _businessSafeEmailLinkBaseUrlConfiguration.GetBaseUrl();

                var viewModel = new SendEmployeeDigestEmailViewModel()
                {
                    To = message.RecipientEmail,
                    From = "BusinessSafeProject@peninsula-uk.com",
                    Subject = "BusinessSafe Online Notifications",
                    BusinessSafeOnlineLink = new HtmlString(emailLink),
                    IsAnyOverdueTaskToNotify = IsAnyOverdueTask(message),
                    IsAnyCompletedTaskToNotify = IsAnyCompletedTask(message),
                    IsAnyDueTomorrowTaskToNotify = IsAnyDueTomorrowTask(message)
                };

                if (viewModel.IsAnyOverdueTaskToNotify || viewModel.IsAnyCompletedTaskToNotify || viewModel.IsAnyDueTomorrowTaskToNotify)
                {
                    //Overdue Tasks
                    viewModel.OverdueGeneralRiskAssessmentsTasksViewModel = message.GeneralRiskAssessmentsOverdueTasks.ToTaskDetailViewModel();

                    viewModel.OverduePersonalRiskAssessmentsTasksViewModel = message.PersonalRiskAssessmentTasksOverdue.ToTaskDetailViewModel();

                    viewModel.OverdueFireRiskAssessmentsTasksViewModel = message.FireRiskAssessmentsOverdueTasks.ToTaskDetailViewModel();

                    viewModel.OverdueHazardousSubstanceRiskAssessmentsTasksViewModel = message.HazardousSubstanceRiskAssessmentTasksOverdue.ToTaskDetailViewModel();

                    viewModel.OverdueReviewRiskAssessmentsTasksViewModel = message.RiskAssessmentReviewTasksOverdue.ToTaskDetailViewModel();

                    viewModel.OverdueResponsibilitiesTasksViewModel = message.ResponsibilitiesTasksOverdue.ToTaskDetailViewModel();

                    viewModel.OverdueActionTasksViewModel = message.ActionTasksOverdue.ToTaskDetailViewModel();


                    //Completed Tasks
                    viewModel.CompletedGeneralRiskAssessmentTasksViewModel = message.GeneralRiskAssessmentTasksCompleted.ToTaskDetailViewModel();

                    viewModel.CompletedPersonalRiskAssessmentTasksViewModel = message.PersonalRiskAssessmentTasksCompleted.ToTaskDetailViewModel();

                    viewModel.CompletedHazardousSubstanceTasksViewModel = message.HazardousSubstanceTasksCompleted.ToTaskDetailViewModel();

                    viewModel.CompletedFireRiskAssessmentTasksViewModel = message.FireRiskAssessmentTasksCompleted.ToTaskDetailViewModel();

                    viewModel.CompletedReviewRiskAssessmentTasksViewModel = message.RiskAssessmentReviewTasksCompleted.ToTaskDetailViewModel();


                    //Due Tomorrow Tasks
                    viewModel.DueTomorrowGeneralRiskAssessmentsTasksViewModel = message.GeneralRiskAssessmentsTasksDueTomorrow.ToTaskDetailViewModel();

                    viewModel.DueTomorrowPersonalRiskAssessmentsTasksViewModel = message.PersonalRiskAssessmentTasksDueTomorrow.ToTaskDetailViewModel();

                    viewModel.DueTomorrowFireRiskAssessmentsTasksViewModel = message.FireRiskAssessmentsTasksDueTomorrow.ToTaskDetailViewModel();

                    viewModel.DueTomorrowHazardousSubstanceRiskAssessmentsTasksViewModel = message.HazardousSubstanceRiskAssessmentTasksDueTomorrow.ToTaskDetailViewModel();

                    viewModel.DueTomorrowReviewRiskAssessmentsTasksViewModel = message.RiskAssessmentReviewTasksDueTomorrow.ToTaskDetailViewModel();

                    viewModel.DueTomorrowResponsibilitiesTasksViewModel = message.ResponsibilitiesTasksDueTomorrow.ToTaskDetailViewModel();

                    viewModel.DueTomorrowActionTasksViewModel = message.ActionTasksDueTomorrow.ToTaskDetailViewModel();


                    //Sends the emails
                    var email = CreateRazorEmailResult(viewModel);
                    _emailSender.Send(email);
                }
                
            }

            Log4NetHelper.Log.Info("SendEmployeeDigestEmail Command Handled");
        }

        private bool HasRecipientEmailAddress(SendEmployeeDigestEmail message)
        {
            return !string.IsNullOrEmpty(message.RecipientEmail);
        }

        private bool IsAnyOverdueTask(SendEmployeeDigestEmail message)
        {
            return
                message.ActionTasksOverdue.Any() ||
                message.FireRiskAssessmentsOverdueTasks.Any() ||
                message.GeneralRiskAssessmentsOverdueTasks.Any() ||
                message.HazardousSubstanceRiskAssessmentTasksOverdue.Any() ||
                message.PersonalRiskAssessmentTasksOverdue.Any() ||
                message.ResponsibilitiesTasksOverdue.Any() ||
                message.RiskAssessmentReviewTasksOverdue.Any();
        }

        private bool IsAnyDueTomorrowTask(SendEmployeeDigestEmail message)
        {
            return
                message.ActionTasksDueTomorrow.Any() ||
                message.FireRiskAssessmentsTasksDueTomorrow.Any() ||
                message.GeneralRiskAssessmentsTasksDueTomorrow.Any() ||
                message.HazardousSubstanceRiskAssessmentTasksDueTomorrow.Any() ||
                message.PersonalRiskAssessmentTasksDueTomorrow.Any() ||
                message.ResponsibilitiesTasksDueTomorrow.Any() ||
                message.RiskAssessmentReviewTasksDueTomorrow.Any();
        }

        private bool IsAnyCompletedTask(SendEmployeeDigestEmail message)
        {
            return
                message.FireRiskAssessmentTasksCompleted.Any() ||
                message.GeneralRiskAssessmentTasksCompleted.Any() ||
                message.HazardousSubstanceTasksCompleted.Any() ||
                message.PersonalRiskAssessmentTasksCompleted.Any() ||
                message.RiskAssessmentReviewTasksCompleted.Any();
        }

        protected virtual RazorEmailResult CreateRazorEmailResult(SendEmployeeDigestEmailViewModel viewModel)
        {
            var email = new MailerController().SendEmployeeDigestEmail(viewModel);
            return email;
        }
    }

    public static class RiskAssessmentMappers
    {
        public static List<TaskDetailsViewModel> ToTaskDetailViewModel(this List<TaskDetails> taskDetailsList)
        {
            var taskDetailsViewModel = new List<TaskDetailsViewModel>();

            taskDetailsList.ForEach( t =>
                taskDetailsViewModel.Add(new TaskDetailsViewModel()
                {
                    Title = t.Title,
                    TaskReference =t.TaskReference,
                    RiskAssesmentReference = t.RiskAssesmentReference,
                    Description = t.Description,
                    RiskAssessor = t.RiskAssessor,
                    TaskAssignedTo = t.TaskAssignedTo,
                    CompletionDueDate = t.CompletionDueDate.HasValue ? String.Format("{0:dd/MM/yy}", t.CompletionDueDate.Value) : string.Empty,
                    CompletedDate = t.CompletedDate.HasValue ? String.Format("{0:dd/MM/yy}", t.CompletedDate.Value) : string.Empty
                }));

            return taskDetailsViewModel;
        }
    }
}
