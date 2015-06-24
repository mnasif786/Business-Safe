using System;
using System.Web;
using ActionMailer.Net.Standalone;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using BusinessSafe.MessageHandlers.Emails.Activation;
using BusinessSafe.MessageHandlers.Emails.Controllers;
using BusinessSafe.MessageHandlers.Emails.EmailSender;
using BusinessSafe.MessageHandlers.Emails.ViewModels;
using BusinessSafe.Messages.Emails.Commands;
using NServiceBus;

namespace BusinessSafe.MessageHandlers.Emails.MessageHandlers
{
    public class SendTaskOverdueUserEmailHandler : IHandleMessages<SendTaskOverdueUserEmail>
    {
        private readonly IEmailSender _emailSender;
        private readonly IBusinessSafeEmailLinkBaseUrlConfiguration _businessSafeEmailLinkBaseUrlConfiguration;
        private readonly ITasksRepository _taskRepository;

        public SendTaskOverdueUserEmailHandler(IEmailSender emailSender, IBusinessSafeEmailLinkBaseUrlConfiguration businessSafeEmailLinkBaseUrlConfiguration, ITasksRepository taskRepository)
        {
            _emailSender = emailSender;
            _businessSafeEmailLinkBaseUrlConfiguration = businessSafeEmailLinkBaseUrlConfiguration;
            _taskRepository = taskRepository;
        }

        public void Handle(SendTaskOverdueUserEmail message)
        {
            try
            {
                var task = _taskRepository.GetByTaskGuid(message.TaskGuid);

                var isActionTask = ((task as ActionTask) != null);

                var emailLink = _businessSafeEmailLinkBaseUrlConfiguration.GetBaseUrl();

                var viewModel = new TaskOverdueViewModel()
                {
                    From = "BusinessSafeProject@peninsula-uk.com",
                    To = message.RecipientEmail,
                    TaskReference = message.TaskReference,
                    Title = message.Title,
                    Description = message.Description,
                    RiskAssessor = message.RiskAssessor,
                    BusinessSafeOnlineLink = new HtmlString(emailLink),
                    TaskCompletionDueDate = message.TaskCompletionDueDate,
                };

                if (task is ResponsibilityTask)
                {
                    viewModel.Subject = string.Format("{0} {1}", "Responsibility", "Task Overdue");
                    viewModel.TaskTypeDescription = "Responsibility";
                }
                else if (isActionTask)
                {
                    viewModel.IsIRN = ((ActionTask) task).Action.Category == ActionCategory.ImmediateRiskNotification;

                    viewModel.Subject = viewModel.IsIRN
                        ? string.Format("{0} {1}", "Immediate Action", "Task Overdue")
                        : string.Format("{0} {1}", "Action", "Task Overdue");

                    viewModel.TaskTypeDescription = viewModel.IsIRN ? "Immediate Action" : "Action";
                    viewModel.ReferenceLabel = viewModel.IsIRN ? "Reference" : "Number";
                    viewModel.ActionRequiredLabel = viewModel.IsIRN ? "Immediate Action Required" : "Action Required";
                    viewModel.ActionRequired = ((ActionTask) task).Action.ActionRequired;
                    viewModel.TaskAssignedTo = task.TaskAssignedTo.FullName;
                    viewModel.AreaOfNonCompliance = ((ActionTask) task).Action.AreaOfNonCompliance;
                }
                else
                {
                    viewModel.Subject = string.Format("{0} {1}", "Risk Assessment", "Task Overdue");
                    viewModel.TaskTypeDescription = "Further Control Measure";
                }

                var userEmail = CreateRazorEmailResultForUser(viewModel, isActionTask);
                _emailSender.Send(userEmail);

                Log4NetHelper.Log.Info("SendTaskOverdueUserEmail Command Handled");
            }
            catch (Exception ex)
            {
                Log4NetHelper.Log.Error("Exception in SendTaskOverdueUserEmail - Could Not send email ", ex);
            }
        }

        protected virtual RazorEmailResult CreateRazorEmailResultForUser(TaskOverdueViewModel viewModel, bool isActionTask)
        {
            var email = isActionTask ? new MailerController().ActionTaskOverdueUser(viewModel) :
                new MailerController().TaskOverdueUser(viewModel);
            return email;
        }

      
    }
}