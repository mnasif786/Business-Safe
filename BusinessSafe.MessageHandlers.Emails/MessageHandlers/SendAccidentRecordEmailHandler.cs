using System;
using System.Linq;
using System.Web;
using ActionMailer.Net.Standalone;
using BusinessSafe.Application.Contracts.AccidentRecord;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.MessageHandlers.Emails.Activation;
using BusinessSafe.MessageHandlers.Emails.Controllers;
using BusinessSafe.MessageHandlers.Emails.EmailSender;
using BusinessSafe.MessageHandlers.Emails.ViewModels;
using BusinessSafe.Messages.Emails.Commands;
using NServiceBus;

namespace BusinessSafe.MessageHandlers.Emails.MessageHandlers
{
    public class SendAccidentRecordEmailHandler : IHandleMessages<SendAccidentRecordEmail>
    {
        private readonly IBusinessSafeEmailLinkBaseUrlConfiguration _businessSafeEmailLinkBaseUrlConfiguration;
        private readonly IEmailSender _emailSender;
        private readonly IAccidentRecordService _accidentService;
        private readonly ISiteService _siteService;

        public SendAccidentRecordEmailHandler(IEmailSender emailSender, IAccidentRecordService accidentRecordService,
            IBusinessSafeEmailLinkBaseUrlConfiguration businessSafeEmailLinkBaseUrlConfiguration, ISiteService siteService)
        {
            _emailSender = emailSender;
            _accidentService = accidentRecordService;
            _businessSafeEmailLinkBaseUrlConfiguration = businessSafeEmailLinkBaseUrlConfiguration;
            _siteService = siteService;
        }

        public void Handle(SendAccidentRecordEmail message)
        {
            var emailLink = _businessSafeEmailLinkBaseUrlConfiguration.GetBaseUrl();

            var accidentRecord = _accidentService.GetByIdAndCompanyId(message.AccidentRecordId, message.CompanyId);

            var accidentEmailDistributionList =
                _siteService.GetAccidentRecordNotificationMembers(accidentRecord.SiteWhereHappened.Id)
                    .Where(x =>  x.HasEmail() );

            foreach (var accidentRecordNotificationMember in accidentEmailDistributionList)
            {
                var viewModel = new AccidentRecordEmailViewModel()
                {
                    From = "BusinessSafeProject@peninsula-uk.com",
                    Subject = string.Format("Accident has been reported at {0}", accidentRecord.SiteWhereHappened.Name),
                    To = accidentRecordNotificationMember.Email(),
                    DateOfAccident =
                        accidentRecord.DateAndTimeOfAccident != null
                            ? accidentRecord.DateAndTimeOfAccident.Value.ToShortDateString()
                            : null,
                    TimeOfAccident =
                        accidentRecord.DateAndTimeOfAccident != null
                            ? accidentRecord.DateAndTimeOfAccident.Value.ToShortTimeString()
                            : null,
                    Location = accidentRecord.Location != null ? accidentRecord.Location : "",
                    PersonInvolved = accidentRecord.PersonInvolved,
                    SeverityOfInjury = accidentRecord.SeverityOfInjury,
                    ChainOfEvents = accidentRecord.DescriptionHowAccidentHappened ?? null,
                    TakenToHospital = accidentRecord.InjuredPersonWasTakenToHospital,
                    RiddorReportable = accidentRecord.IsReportable,
                    Injuries =
                        accidentRecord.AccidentRecordInjuries != null && accidentRecord.AccidentRecordInjuries.Count > 0
                            ? accidentRecord.AccidentRecordInjuries.First().Injury.Description
                            : "",
                    Site = accidentRecord.SiteWhereHappened != null ? accidentRecord.SiteWhereHappened.Name : null,
                    BusinessSafeOnlineLink = new HtmlString(emailLink)
                };

                var email = CreateRazorEmailResult(viewModel);
                _emailSender.Send(email);
            
                Log4NetHelper.Log.Info("SendAccidentRecordCommandHandled");
            }
        }

        protected virtual RazorEmailResult CreateRazorEmailResult(AccidentRecordEmailViewModel viewModel)
        {
            var email = new MailerController().SendAccidentRecordEmail(viewModel);
            return email;
        }
    }
}
