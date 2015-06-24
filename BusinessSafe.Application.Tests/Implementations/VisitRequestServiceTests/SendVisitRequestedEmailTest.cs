using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Implementations.VisitRequest;
using BusinessSafe.Application.Request;
using BusinessSafe.Messages.Emails.Commands;
using Moq;
using NServiceBus;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.VisitRequestServiceTests
{
    [TestFixture]
    [Category("Unit")]
    public class SendVisitRequestedEmailTest
    {
        private Mock<IBus> _bus;

        [SetUp]
        public void Setup ()
        {
            _bus = new Mock<IBus>();
        }

        [Test]
        public void Given_valid_details_when_send_email_called_then_appropriate_methods_are_called()
        {
            VisitRequestService target = GetTarget();

            RequestVisitRequest request = new RequestVisitRequest();

            target.SendVisitRequestedEmail( request );


            _bus.Verify(x => x.Send(It.IsAny<SendVisitRequestedEmail>()), Times.Once());
        }
        
        public VisitRequestService GetTarget()
        {
            return new VisitRequestService(_bus.Object);
        } 
    }
}
