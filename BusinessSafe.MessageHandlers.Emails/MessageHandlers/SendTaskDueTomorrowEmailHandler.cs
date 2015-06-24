using System;
using System.Web;
using ActionMailer.Net.Standalone;
using BusinessSafe.MessageHandlers.Emails.Activation;
using BusinessSafe.MessageHandlers.Emails.Controllers;
using BusinessSafe.MessageHandlers.Emails.EmailSender;
using BusinessSafe.MessageHandlers.Emails.ViewModels;
using BusinessSafe.Messages.Emails.Commands;
using NServiceBus;

namespace BusinessSafe.MessageHandlers.Emails.MessageHandlers
{
    public class SendTaskDueTomorrowEmailHandler : IHandleMessages<SendTaskDueTomorrowEmail>
    {
        private readonly IEmailSender _emailSender;
        private readonly IBusinessSafeEmailLinkBaseUrlConfiguration _urlConfiguration;
       
        public SendTaskDueTomorrowEmailHandler(IEmailSender emailSender,
            IBusinessSafeEmailLinkBaseUrlConfiguration urlConfiguration)
        {
            _emailSender = emailSender;
            _urlConfiguration = urlConfiguration;
        }

        public void Handle(SendTaskDueTomorrowEmail message)
        {
            var viewModel = new TaskDueTomorrowViewModel()
            {
                From = "BusinessSafeProject@peninsula-uk.com",
                Subject = "Task is due tomorrow",
                To = message.RecipientEmail,
                TaskType = message.TaskType,
                AssignedBy = message.TaskAssignedBy,
                Description = message.Description,
                DueDate = message.TaskCompletionDueDate,
                TaskReference = message.TaskReference,
                Title = message.Title,
                BusinessSafeOnlineLink = new HtmlString(_urlConfiguration.GetBaseUrl()),
            };

            //Sends the email
            _emailSender.Send(CreateRazorEmailResult(viewModel));
         
            Log4NetHelper.Log.Info("SendTaskDueTomorrowEmail Command Handled");
        }

        protected virtual RazorEmailResult CreateRazorEmailResult(TaskDueTomorrowViewModel viewModel)
        {
            var email = new MailerController().SendTaskDueTomorrowEmail(viewModel);
            return email;
        }
    }
}
