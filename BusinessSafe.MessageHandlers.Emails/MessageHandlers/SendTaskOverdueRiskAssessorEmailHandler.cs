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
    public class SendTaskOverdueRiskAssessorEmailHandler : IHandleMessages<SendTaskOverdueRiskAssessorEmail>
    {
        private readonly IEmailSender _emailSender;
        private readonly IBusinessSafeEmailLinkBaseUrlConfiguration _businessSafeEmailLinkBaseUrlConfiguration;
        private readonly ITasksRepository _taskRepository;

        public SendTaskOverdueRiskAssessorEmailHandler(IEmailSender emailSender, IBusinessSafeEmailLinkBaseUrlConfiguration businessSafeEmailLinkBaseUrlConfiguration, ITasksRepository taskRepository)
        {
            _emailSender = emailSender;
            _businessSafeEmailLinkBaseUrlConfiguration = businessSafeEmailLinkBaseUrlConfiguration;
            _taskRepository = taskRepository;
        }

        public void Handle(SendTaskOverdueRiskAssessorEmail message)
        {
            try
            {
                var emailLink = _businessSafeEmailLinkBaseUrlConfiguration.GetBaseUrl();

                var task = _taskRepository.GetByTaskGuid(message.TaskGuid);

                string taskType;
                string taskTypeDescription;

                if (task is ResponsibilityTask)
                {
                    taskType = "Responsibility";
                    taskTypeDescription = "Responsibility";
                }
                else
                {
                    taskType = "Risk Assessment";
                    taskTypeDescription = "Further Control Measure";
                }

                var viewModel = new TaskOverdueViewModel()
                {
                    From = "BusinessSafeProject@peninsula-uk.com",
                    Subject = string.Format("{0} {1}", taskType, "Task Overdue"),
                    To = message.RecipientEmail,
                    TaskReference = message.TaskReference,
                    Title = message.Title,
                    Description = message.Description,
                    BusinessSafeOnlineLink = new HtmlString(emailLink),
                    TaskAssignedTo = message.TaskAssignedTo,
                    TaskCompletionDueDate = message.TaskCompletionDueDate,
                    TaskTypeDescription = taskTypeDescription
                };

                var userEmail = CreateRazorEmailResultForRiskAssessor(viewModel);
                _emailSender.Send(userEmail);

                Log4NetHelper.Log.Info("SendTaskOverdueRiskAssessorEmail Command Handled");
            }
            catch (Exception ex)
            {
                Log4NetHelper.Log.Error("Exception in SendTaskOverdueRiskAssessorEmail - Could Not send email ", ex);
            }
        }

        protected virtual RazorEmailResult CreateRazorEmailResultForRiskAssessor(TaskOverdueViewModel viewModel)
        {
            var email = new MailerController().TaskOverdueRiskAssessor(viewModel);
            return email;
        }
    }
}