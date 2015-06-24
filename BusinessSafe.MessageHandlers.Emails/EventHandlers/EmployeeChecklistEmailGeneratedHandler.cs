using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using ActionMailer.Net.Standalone;
using BusinessSafe.MessageHandlers.Emails.Activation;
using BusinessSafe.MessageHandlers.Emails.Controllers;
using BusinessSafe.MessageHandlers.Emails.EmailSender;
using BusinessSafe.MessageHandlers.Emails.ViewModels;
using BusinessSafe.Messages.Events;
using NServiceBus;

namespace BusinessSafe.MessageHandlers.Emails.EventHandlers
{
    public class EmployeeChecklistEmailGeneratedHandler : IHandleMessages<EmployeeChecklistEmailGenerated>
    {
        private readonly IEmailSender _emailSender;
        private readonly IBusinessSafeEmailLinkBaseUrlConfiguration _businessSafeEmailLinkBaseUrlConfiguration;

        public EmployeeChecklistEmailGeneratedHandler(
            IEmailSender emailSender,
            IBusinessSafeEmailLinkBaseUrlConfiguration businessSafeEmailLinkBaseUrlConfiguration)
        {
            _emailSender = emailSender;
            _businessSafeEmailLinkBaseUrlConfiguration = businessSafeEmailLinkBaseUrlConfiguration;
        }

       
        public void Handle(EmployeeChecklistEmailGenerated message)
        {
            try
            {            
                var viewModel = new EmployeeChecklistEmailGeneratedViewModel()
                                    {
                                        From = "BusinessSafeProject@peninsula-uk.com",
                                        Subject = "",
                                        To = message.RecipientEmail,
                                        Message = GetMessage(message.Message),
                                        ChecklistUrls = message.EmployeeChecklistIds.Select(CreateChecklistUrl),
                                        CompletionDueDateForChecklists = GetCompletionDueDateInformation(message.CompletionDueDateForChecklists)
                                    };

                var email = CreateRazorEmailResult(viewModel);

                _emailSender.Send(email);

                Log4NetHelper.Log.Info("SendEmployeeChecklistEmailGeneratedHandler Command Handled");
            }
            catch (Exception ex)
            {
                Log4NetHelper.Log.Debug("Exception in EmployeeChecklistEmailGeneratedHandler - ", ex);
            }
        }

        private string CreateChecklistUrl(Guid guid)
        {
            return _businessSafeEmailLinkBaseUrlConfiguration.GetBaseUrl() + "/Checklists/" + guid;
        }

        public virtual RazorEmailResult CreateRazorEmailResult(EmployeeChecklistEmailGeneratedViewModel viewModel)
        {
            var email = new MailerController().EmployeeChecklistEmailGenerated(viewModel);
            return email;
        }

        private HtmlString GetCompletionDueDateInformation(DateTime? completionDueDate)
        {
            if (completionDueDate.HasValue == false)
            {
                return new HtmlString(string.Empty);
            }

            var formattedDateString = completionDueDate.Value.ToString("D", CultureInfo.CreateSpecificCulture("en-GB"));

            return new HtmlString(string.Format("Checklist Completion Due Date : {0} {1}", formattedDateString, Environment.NewLine));
        }

        private IList<string> GetMessage(string message)
        {
            var paragraphs = message.Split(new[] {'\n'}, StringSplitOptions.RemoveEmptyEntries);
            return paragraphs.ToList();
        }
    }
}
