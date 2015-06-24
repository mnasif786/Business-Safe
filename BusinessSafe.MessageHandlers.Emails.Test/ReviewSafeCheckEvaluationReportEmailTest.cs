using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ActionMailer.Net.Standalone;
using BusinessSafe.MessageHandlers.Emails.EmailSender;
using BusinessSafe.Messages.Emails.Commands;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.MessageHandlers.Emails.Test
{
    [TestFixture]
    public class ReviewSafeCheckEvaluationReportEmailTest
    {
        private Mock<IEmailSender>  _emailSender;

        [SetUp]
        public void SetUp()
        {
            _emailSender = new Mock<IEmailSender>();
        }

        [Test]
        public void When_handle_Then_should_call_correct_methods()
        {
            //Given
            var handler = CreateTarget();
            var message = new SendSiteDetailsUpdatedEmail();

            //When
            handler.Handle(message);

            //Then
            _emailSender.Verify(x => x.Send(It.IsAny<RazorEmailResult>()));
        }

        private MyReviewSafeCheckEvaluationReporetEmail CreateTarget()
        {
            var handler = new MySendSiteDetailsUpdatedEmailHandler(_emailSender.Object);
            return handler;
        }
    }
}
