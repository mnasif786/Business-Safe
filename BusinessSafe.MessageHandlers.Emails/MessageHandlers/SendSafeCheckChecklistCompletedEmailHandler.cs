using System;
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
    public class SendSafeCheckChecklistCompletedEmailHandler : IHandleMessages<SendSafeCheckChecklistCompletedEmail>
    {
        private readonly IEmailSender _emailSender;
        private readonly ISafeCheckEmailLinkBaseUrlConfiguration _urlConfiguration;
        private readonly ICheckListRepository _checklistRepository;
        private readonly IQaAdvisorRepository _qaAdvisorRepository;

        public SendSafeCheckChecklistCompletedEmailHandler(IEmailSender emailSender,
            ISafeCheckEmailLinkBaseUrlConfiguration urlConfiguration,
            ICheckListRepository checklistRepository, IQaAdvisorRepository qaAdvisorRepository)
        {
            _emailSender = emailSender;
            _urlConfiguration = urlConfiguration;
            _checklistRepository = checklistRepository;
            _qaAdvisorRepository = qaAdvisorRepository;
        }

        public void Handle(SendSafeCheckChecklistCompletedEmail message)
        {
            try
            {
                var checklist = _checklistRepository.GetById(message.ChecklistId);
                var activeDirectory = new WS_ActiveDirectory();
                var user = activeDirectory.GetUserByUsername(ConstructADUserName(checklist.ChecklistCompletedBy));

                if (user != null && !string.IsNullOrEmpty(user.EmailAddress))
                {
                    var viewModel = new SafeCheckChecklistCompletedViewModel()
                    {
                        To = checklist.QaAdvisor != null ? checklist.QaAdvisor.Email : "",
                        From = user.Forename + user.Surname,
                        Subject =
                            string.Format("SafeCheck Report has been completed ({0} - {1})", message.Can,
                                message.Postcode),
                        Can = message.Can,
                        Postcode = message.Postcode,
                        ReportUrl = _urlConfiguration.GetBaseUrl() + "/index.htm#/evaluationchecklists/" + checklist.Id,

                        VisitDate = checklist.VisitDate.HasValue
                            ? checklist.VisitDate.Value.ToString("dd/MM/yyyy")
                            : "",

                        CompletedBy = checklist.ChecklistCompletedBy,
                        CompletedDate = checklist.ChecklistCompletedOn.HasValue
                                        ? checklist.ChecklistCompletedOn.Value.ToString("dd/MM/yyyy")
                                        : ""
                    };

                    var email = CreateRazorEmailResult(viewModel);

                    _emailSender.Send(email);
                }
            }
            catch (Exception e)
            {
                Log4NetHelper.Log.Error("SendSafeCheckChecklistCompletedEmail: " + e.Message);
            }
        }

        private string ConstructADUserName(string userName)
        {
            return userName.Replace(" ", ".");
        }

        protected virtual RazorEmailResult CreateRazorEmailResult(SafeCheckChecklistCompletedViewModel viewModel)
        {
            var email = new MailerController().CompletedNotification(viewModel);
            return email;
        }
    }
}
