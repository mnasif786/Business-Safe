using System.Net.Mail;
using System.Text;

using ActionMailer.Net;
using ActionMailer.Net.Standalone;

using BusinessSafe.MessageHandlers.Emails.EmailSender;
using BusinessSafe.MessageHandlers.Emails.MessageHandlers;
using BusinessSafe.MessageHandlers.Emails.ViewModels;

using Moq;

namespace BusinessSafe.MessageHandlers.Emails.Test
{
    public class MySendCompanyDetailsUpdatedEmailHandler : SendCompanyDetailsUpdatedEmailHandler
    {
        public MySendCompanyDetailsUpdatedEmailHandler(IEmailSender emailSender)
            : base(emailSender)
        {
        }

        protected override RazorEmailResult CreateRazorEmailResult(SendCompanyDetailsUpdatedEmailViewModel viewModel)
        {
            ViewModelTestHelper(viewModel);

            return new RazorEmailResult(new Mock<IMailInterceptor>().Object,
                new Mock<IMailSender>().Object,
                new Mock<MailMessage>().Object,
                "ViewName",
                Encoding.ASCII,
                "ViewPath");
        }

        // this method is purely for testing what is in the viewModel
        public virtual void ViewModelTestHelper(SendCompanyDetailsUpdatedEmailViewModel viewModel)
        {
            
        }
    }
}