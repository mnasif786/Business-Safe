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
    public class SendTaskActionCompletedEmailHandler : IHandleMessages<SendActionTaskCompletedEmail>
    {
        private readonly IEmailSender _emailSender;
        private readonly IBusinessSafeEmailLinkBaseUrlConfiguration _urlConfiguration;
        private readonly ITasksRepository _taskRepository;
        private readonly IUserRepository _userRepository;

        public SendTaskActionCompletedEmailHandler(
            IEmailSender emailSender,
            IBusinessSafeEmailLinkBaseUrlConfiguration urlConfiguration,
            ITasksRepository taskRepository,
            IUserRepository userRepository)
        {
            _emailSender = emailSender;
            _urlConfiguration = urlConfiguration;
            _taskRepository = taskRepository;
            _userRepository = userRepository;
        }

        public void Handle(SendActionTaskCompletedEmail message)
        {
            Task task = null;
            bool isActionTask = false;
            string recipientEmail = string.Empty;
            try 
            { 
                task = _taskRepository.GetByTaskGuid(message.TaskGuid);
                isActionTask = ((task as ActionTask) != null);
                recipientEmail = GetRecipientEmailAddress(task);
            }
            catch (Exception ex)
            {
                Log4NetHelper.Log.Error("Exception in SendActionTaskCompletedEmail - Could Not Retrieve Review Task ", ex);
            }

            //sends email only if we have a recipient email address and task is an action task
            if (!string.IsNullOrEmpty(recipientEmail) && isActionTask)
            {
                var action = ((ActionTask) task).Action;
                
                var isIrn = action.Category == ActionCategory.ImmediateRiskNotification;

                var taskTypeDescription = (isIrn) ? "Immediate Action" : "Action";

                var viewModel = new ActionTaskCompletedViewModel()
                {
                    IsIrn = isIrn,
                    From = "BusinessSafeProject@peninsula-uk.com",
                    To = recipientEmail,
                    Subject = string.Format("{0} {1}", taskTypeDescription, "Task Completed"),
                    TaskReference = task.Reference,
                    Title = task.Title,
                    TaskType = taskTypeDescription,
                    AssignedTo = task.TaskAssignedTo.FullName,
                    ActionRequired = action.ActionRequired,
                    AreaOfNonCompliance = action.AreaOfNonCompliance,
                    BusinessSafeOnlineLink = new HtmlString(_urlConfiguration.GetBaseUrl()),

                    TaskReferenceLabel = isIrn ? "Reference" : "Number",
                    ActionRequiredLabel = isIrn ? "Immediate Action Required" : "Action Required"
                };

                //Sends the email
                _emailSender.Send(
                    CreateRazorEmailResult(viewModel)
                    );

                Log4NetHelper.Log.Info("SendActionTaskCompletedEmail Command Handled");
            }
        }

        /// <summary>
        /// This is an email address of the user who created the task
        /// </summary>
        /// <param name="task">The task</param>
        /// <returns>Returns teh email address of the user if exists otherwise empty string</returns>
        private string GetRecipientEmailAddress(Task task)
        {
            string recipientemail = null;
            var user = _userRepository.GetById(task.CreatedBy.Id);
            if (user !=null && user.Employee != null)
            {
                recipientemail = user.Employee.GetEmail();
            }
            return recipientemail;
        }

        protected virtual RazorEmailResult CreateRazorEmailResult(ActionTaskCompletedViewModel viewModel)
        {
            var email = new MailerController().ActionTaskCompleted(viewModel);
            return email;
        }
    }
}
