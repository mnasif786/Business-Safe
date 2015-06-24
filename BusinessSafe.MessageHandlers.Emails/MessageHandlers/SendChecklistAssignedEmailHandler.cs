using BusinessSafe.MessageHandlers.Emails.Activation;
using BusinessSafe.MessageHandlers.Emails.Controllers;
using BusinessSafe.MessageHandlers.Emails.EmailSender;
using BusinessSafe.MessageHandlers.Emails.ViewModels;
using BusinessSafe.Messages.Emails.Commands;
using NServiceBus;
using BusinessSafe.Domain.RepositoryContracts.SafeCheck;
using ActionMailer.Net.Standalone;

namespace BusinessSafe.MessageHandlers.Emails.MessageHandlers
{
    public class SendChecklistAssignedEmailHandler : IHandleMessages<SendChecklistAssignedEmail>
    {
        private readonly IEmailSender _emailSender;
        private readonly ICheckListRepository _checklistRepository;
        private readonly IQaAdvisorRepository _qaAdvisorRepository;
        private readonly ISafeCheckEmailLinkBaseUrlConfiguration _safeCheckEmailLinkBaseUrlConfiguration;
        

        public SendChecklistAssignedEmailHandler(
            IEmailSender emailSender,
            ICheckListRepository checklistRepository,
            IQaAdvisorRepository qaAdvisorRepository,
            ISafeCheckEmailLinkBaseUrlConfiguration safeCheckEmailLinkBaseUrlConfiguration)
        {
            _emailSender = emailSender;
            _checklistRepository = checklistRepository;
            _qaAdvisorRepository = qaAdvisorRepository;
            _safeCheckEmailLinkBaseUrlConfiguration = safeCheckEmailLinkBaseUrlConfiguration;;
        }

        public void Handle(SendChecklistAssignedEmail message)
        {
            var assignedTo = _qaAdvisorRepository.GetById(message.AssignedToId);
            var checklist = _checklistRepository.GetById(message.ChecklistId);
            
            if (assignedTo != null && !string.IsNullOrEmpty(assignedTo.Email))
            {
                var viewModel = new SendChecklistAssignedEmailViewModel
                                    {
                                        From = "BusinessSafeProject@peninsula-uk.com",
                                        Subject = "SafeCheck Report - " + message.Can + " - " + message.Postcode,
                                        To = assignedTo.Email,
                                        Can = message.Can,
                                        VisitDate =
                                            checklist.VisitDate.HasValue
                                                ? checklist.VisitDate.Value.ToString("dd/MM/yyyy")
                                                : "",
                                        VisitBy = checklist.ChecklistCompletedBy,
                                        LinkUrl = _safeCheckEmailLinkBaseUrlConfiguration.GetBaseUrl() + "/index.htm#/evaluationchecklists/" + checklist.Id,
                                        CompletedDate = 
                                           checklist.ChecklistCompletedOn.HasValue
                                                ? checklist.ChecklistCompletedOn.Value.ToString("dd/MM/yyyy")
                                                : ""
                                    };

                viewModel.EmailReportTo = checklist.EmailReportToPerson ? checklist.EmailAddress : "";
                if(checklist.EmailReportToPerson && checklist.EmailReportToOthers)
                {
                    viewModel.EmailReportTo += ", ";
                }
                viewModel.EmailReportTo += checklist.EmailReportToOthers ? checklist.OtherEmailAddresses : "";

                if (checklist.PostReport)
                {
                    viewModel.PostReportTo = checklist.PostReport
                                                 ? message.SiteName + ", " +
                                                   message.Address1 + ", " +
                                                   message.Address2 + ", " +
                                                   message.Address3 + ", " +
                                                   message.Address4 + ", " +
                                                   message.Address5 + ", " +
                                                   message.Postcode
                                                 : "";

                }

                var email = CreateRazorEmailResult(viewModel);
                
                _emailSender.Send(email);
            }

            Log4NetHelper.Log.Info("SendReviewCompletedEmail Command Handled");
        }

        protected virtual RazorEmailResult CreateRazorEmailResult(SendChecklistAssignedEmailViewModel viewModel)
        {
            var email = new MailerController().ChecklistAssigned(viewModel);
            return email;
        }
    }
}
