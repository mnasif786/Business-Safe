using ActionMailer.Net.Standalone;
using BusinessSafe.MessageHandlers.Emails.Activation;
using BusinessSafe.MessageHandlers.Emails.Controllers;
using BusinessSafe.MessageHandlers.Emails.EmailSender;
using BusinessSafe.MessageHandlers.Emails.ViewModels;
using BusinessSafe.Messages.Emails.Commands;
using NServiceBus;

namespace BusinessSafe.MessageHandlers.Emails.MessageHandlers
{
    public class SendSiteDetailsUpdatedEmailHandler : IHandleMessages<SendSiteDetailsUpdatedEmail>
    {
        private readonly IEmailSender _emailSender;

        public SendSiteDetailsUpdatedEmailHandler(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public void Handle(SendSiteDetailsUpdatedEmail message)
        {
            var viewModel = new SendSiteDetailsUpdatedEmailViewModel()
                                {
                                    ActioningUserName = message.ActioningUserName,
                                    From = "BusinessSafeProject@peninsula-uk.com",
                                    Subject = "Client Request - Change Client Site Details",
                                    To = "client.services@peninsula-uk.com",
                                    CompanyId = message.CompanyId,
                                    CAN = message.CAN,
                                    SiteContactUpdated = message.SiteContactUpdated,
                                    NameUpdated = message.NameUpdated,
                                    AddressLine1Updated = message.AddressLine1Updated,
                                    AddressLine2Updated = message.AddressLine2Updated,
                                    AddressLine3Updated = message.AddressLine3Updated,
                                    AddressLine4Updated = message.AddressLine4Updated,
                                    AddressLine5Updated = message.AddressLine5Updated,
                                    CountyUpdated = message.CountyUpdated,
                                    PostcodeUpdated = message.PostcodeUpdated,
                                    TelephoneUpdated = message.TelephoneUpdated,

                                    SiteContactCurrent = message.SiteContactCurrent,
                                    NameCurrent = message.NameCurrent,
                                    AddressLine1Current = message.AddressLine1Current,
                                    AddressLine2Current = message.AddressLine2Current,
                                    AddressLine3Current = message.AddressLine3Current,
                                    AddressLine4Current = message.AddressLine4Current,
                                    AddressLine5Current = message.AddressLine5Current,
                                    CountyCurrent = message.CountyCurrent,
                                    PostcodeCurrent = message.PostcodeCurrent,
                                    TelephoneCurrent = message.TelephoneCurrent,
                                    SiteStatusCurrent = message.SiteStatusCurrent,
                                    SiteStatusUpdated = message.SiteStatusUpdated
                                };

            var email = CreateRazorEmailResult(viewModel);

            _emailSender.Send(email);

            Log4NetHelper.Log.Info("SendSiteDetailsUpdatedEmailHandler Command Handled");
        }

        protected virtual RazorEmailResult CreateRazorEmailResult(SendSiteDetailsUpdatedEmailViewModel viewModel)
        {
            var email = new MailerController().SiteDetailsUpdated(viewModel);
            return email;
        }
    }
}
