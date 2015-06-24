using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using ActionMailer.Net;
using ActionMailer.Net.Standalone;
using BusinessSafe.MessageHandlers.Emails.EmailSender;
using BusinessSafe.MessageHandlers.Emails.MessageHandlers;
using BusinessSafe.MessageHandlers.Emails.ViewModels;
using BusinessSafe.Messages.Emails.Commands;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.MessageHandlers.Emails.Test
{
     [TestFixture]
    public class SendHardCopyToOfficeEmailHandlerTests
    {
        private Mock<IEmailSender> _emailSender;
        //private Mock<IBusinessSafeEmailLinkBaseUrlConfiguration> _urlConfiguration;        

        [SetUp]
        public void SetUp()
        {
            _emailSender = new Mock<IEmailSender>();            

            //_urlConfiguration = new Mock<IBusinessSafeEmailLinkBaseUrlConfiguration>();
            //_urlConfiguration.Setup(u => u.GetBaseUrl()).Returns(string.Empty);
        }

        [Test]
        public void When_handle_Then_should_call_correct_methods()
        {
            //Given            
            var message = new SendHardCopyToOfficeEmail()
            {
                ChecklistId = Guid.NewGuid(),
                CAN = "SGG72",
                VisitDate = DateTime.Now,
                VisitBy = "Fred Flintstone",
                SubmittedBy = "Barney Rubble",
                SubmittedDate = DateTime.Now,        
                SiteAddressLine1 = "22 Acacia Avenue",
                SiteAddressLine2 = "Rubble Town",
                SiteAddressLine3 = "Rockville",
                SiteAddressLine4 = "",
                SiteAddressLine5 = "",        
                SitePostcode = "FF1 2RV"
            };          

            //When
            var handler = CreateTarget();
            handler.Handle(message);

            //Then
            _emailSender.Verify(x => x.Send(It.IsAny<RazorEmailResult>()));
        }

        private SendHardCopyToOfficeEmailHandler CreateTarget()
        {
            var handler = new MockSendReviewCompletedEmailHandler(_emailSender.Object);
            return handler;
        }

     

        public class MockSendReviewCompletedEmailHandler : SendHardCopyToOfficeEmailHandler
        {
            public MockSendReviewCompletedEmailHandler(IEmailSender emailSender)
                : base(emailSender)
            {
            }

            protected override RazorEmailResult CreateRazorEmailResult(SendHardCopyToOfficeEmailViewModel viewModel)
            {
                return new RazorEmailResult(new Mock<IMailInterceptor>().Object,
                                            new Mock<IMailSender>().Object,
                                            new Mock<MailMessage>().Object,
                                            "ViewName",
                                            Encoding.ASCII,
                                            "ViewPath");
            }
        }
    }
}
