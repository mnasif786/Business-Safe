using ActionMailer.Net.Standalone;
using BusinessSafe.MessageHandlers.Emails.Activation;
using BusinessSafe.MessageHandlers.Emails.Controllers;
using BusinessSafe.MessageHandlers.Emails.EmailSender;
using BusinessSafe.MessageHandlers.Emails.ViewModels;
using BusinessSafe.Messages.Emails.Commands;
using NServiceBus;

namespace BusinessSafe.MessageHandlers.Emails.MessageHandlers
{
    public class SendCompanyDetailsUpdatedEmailHandler : IHandleMessages<SendCompanyDetailsUpdatedEmail>
    {
        private readonly IEmailSender _emailSender;
        
        public SendCompanyDetailsUpdatedEmailHandler(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public void Handle(SendCompanyDetailsUpdatedEmail message)
        {
            
            var viewModel = new SendCompanyDetailsUpdatedEmailViewModel()
                                {
                                    From = "BusinessSafeProject@peninsula-uk.com",
                                    Subject = "Client Request - Change Client Details",
                                    To = "client.services@peninsula-uk.com",

                                    ActioningUserName = message.ActioningUserName,
                                    CompanyId = message.CompanyId,
                                    CAN = message.CAN,

                                    CompanyName = message.NewCompanyDetailsInformation.CompanyName,
                                    AddressLine1 = message.NewCompanyDetailsInformation.AddressLine1,
                                    AddressLine2 = message.NewCompanyDetailsInformation.AddressLine2,
                                    AddressLine3 = message.NewCompanyDetailsInformation.AddressLine3,
                                    AddressLine4 = message.NewCompanyDetailsInformation.AddressLine4,
                                    Postcode = message.NewCompanyDetailsInformation.Postcode,
                                    Telephone = message.NewCompanyDetailsInformation.Telephone,
                                    Website = message.NewCompanyDetailsInformation.Website,
                                    MainContact = message.NewCompanyDetailsInformation.MainContact,
                                    BusinssSafeContactName = message.NewCompanyDetailsInformation.BusinessSafeContactName,

                                    OldCompanyName = message.ExistingCompanyDetailsInformation.CompanyName,
                                    OldAddressLine1 = message.ExistingCompanyDetailsInformation.AddressLine1,
                                    OldAddressLine2 = message.ExistingCompanyDetailsInformation.AddressLine2,
                                    OldAddressLine3 = message.ExistingCompanyDetailsInformation.AddressLine3,
                                    OldAddressLine4 = message.ExistingCompanyDetailsInformation.AddressLine4,
                                    OldPostcode = message.ExistingCompanyDetailsInformation.Postcode,
                                    OldTelephone = message.ExistingCompanyDetailsInformation.Telephone,
                                    OldWebsite = message.ExistingCompanyDetailsInformation.Website,
                                    OldMainContact = message.ExistingCompanyDetailsInformation.MainContact,
                                    OldBusinssSafeContactName = message.ExistingCompanyDetailsInformation.BusinessSafeContactName
                                };

            
            var email = CreateRazorEmailResult(viewModel);

            _emailSender.Send(email);

            Log4NetHelper.Log.Info("SendCompanyDetailsUpdatedEmail Command Handled");
        }

        protected virtual RazorEmailResult CreateRazorEmailResult(SendCompanyDetailsUpdatedEmailViewModel viewModel)
        {
            var email = new MailerController().CompanyDetailsUpdated(viewModel);
            return email;
        }
    }
}