using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ActionMailer.Net.Standalone;
using BusinessSafe.Domain.RepositoryContracts.SafeCheck;
using BusinessSafe.MessageHandlers.Emails.Activation;
using BusinessSafe.MessageHandlers.Emails.ActiveDirectory;
using BusinessSafe.MessageHandlers.Emails.Controllers;
using BusinessSafe.MessageHandlers.Emails.EmailSender;
using BusinessSafe.MessageHandlers.Emails.ViewModels;
using BusinessSafe.Messages.Emails.Commands;
using NServiceBus;



namespace BusinessSafe.MessageHandlers.Emails.MessageHandlers
{
    public class SendUpdateRequiredEmailHandler : IHandleMessages<SendUpdateRequiredEmail>
    {
        private readonly IEmailSender _emailSender;
        private readonly ICheckListRepository _checklistRepository;
        private readonly IQaAdvisorRepository _qaAdvisorRepository;
        private readonly ISafeCheckEmailLinkBaseUrlConfiguration _safeCheckEmailLinkBaseUrlConfiguration;

        public SendUpdateRequiredEmailHandler(
            IEmailSender emailSender,
            ICheckListRepository checklistRepository,
            IQaAdvisorRepository qaAdvisorRepository,
            ISafeCheckEmailLinkBaseUrlConfiguration safeCheckEmailLinkBaseUrlConfiguration)
        {
            _emailSender = emailSender;
            _checklistRepository = checklistRepository;
            _qaAdvisorRepository = qaAdvisorRepository;
            _safeCheckEmailLinkBaseUrlConfiguration = safeCheckEmailLinkBaseUrlConfiguration;
        }

        public void Handle(SendUpdateRequiredEmail message)
        {
            var checklist = _checklistRepository.GetById(message.ChecklistId);

            if (checklist.ChecklistCompletedBy == null)
                return;

            var activeDirectory = new WS_ActiveDirectory();
            var user = activeDirectory.GetUserByUsername(ConstructADUserName(checklist.ChecklistCompletedBy));

            if (user != null && !string.IsNullOrEmpty(user.EmailAddress))
            {
                var viewModel = new SendUpdateRequiredEmailViewModel()
                {
                    To = user.EmailAddress,
                    From = "BusinessSafeProject@peninsula-uk.com",
                    Subject = string.Format("SafeCheck Report Updates Required ({0} - {1})" , message.Can, message.Postcode),
                    Can = message.Can,
                    QaComments = checklist.QAComments,
                    AdvisorName = checklist.QaAdvisor != null ? checklist.QaAdvisor.Forename + " " + checklist.QaAdvisor.Surname : string.Empty,
                    Postcode = message.Postcode,
                    ReportUrl = _safeCheckEmailLinkBaseUrlConfiguration.GetBaseUrl() + "/index.htm#/evaluationchecklists/" + checklist.Id,
                    
                    VisitDate = checklist.VisitDate.HasValue
                                                ? checklist.VisitDate.Value.ToString("dd/MM/yyyy")
                                                : ""
                    
                 };

                var email = CreateRazorEmailResult(viewModel);

                _emailSender.Send(email);
            }

            Log4NetHelper.Log.Info("SendUpdateRequiredEmail Command Handled");
        }

        private string ConstructADUserName(string userName)
        {
            return userName.Replace(" ", ".");
        }

        protected virtual RazorEmailResult CreateRazorEmailResult(SendUpdateRequiredEmailViewModel viewModel)
        {
            var email = new MailerController().UpdateRequired(viewModel);
            return email;
        }

    }
}
