using System;
using System.Collections.Generic;
using ActionMailer.Net.Standalone;
using BusinessSafe.Domain.Entities.SafeCheck;
using BusinessSafe.Domain.RepositoryContracts.SafeCheck;
using BusinessSafe.MessageHandlers.Emails.Activation;
using BusinessSafe.MessageHandlers.Emails.EmailSender;
using BusinessSafe.MessageHandlers.Emails.MessageHandlers;
using BusinessSafe.MessageHandlers.Emails.ViewModels;
using BusinessSafe.Messages.Emails.Commands;
using Moq;
using Moq.Protected;
using NUnit.Framework;

namespace BusinessSafe.MessageHandlers.Emails.Test
{
    [TestFixture]
    public class SendIRNNotificationEmailHandlerTest
    {
        private Mock<IEmailSender> _emailSender;
        private Mock<IBusinessSafeEmailLinkBaseUrlConfiguration> _urlConfiguration;
        private Mock<ICheckListRepository> _checklistRepository;

        [SetUp]
        public void SetUp()
        {
            _emailSender = new Mock<IEmailSender>();
            _checklistRepository = new Mock<ICheckListRepository>();
            
            _urlConfiguration = new Mock<IBusinessSafeEmailLinkBaseUrlConfiguration>();
            _urlConfiguration.Setup(u => u.GetBaseUrl()).Returns(string.Empty);
        }

        [Test]
        public void When_handle_then_Should_Call_Correct_Methods()
        {
            // Given
            var handler = GetTarget();
            var message = new SendIRNNotificationEmail();

            _checklistRepository.Setup(c => c.GetById(It.IsAny<Guid>()))
                .Returns(() =>
                             {
                                 var checklist = new Checklist()
                                                     {
                                                         Id = Guid.NewGuid(),
                                                         EmailAddress = "test@test.com",

                                                     };
                                 checklist.AddImmediateRiskNotification(new ImmediateRiskNotification()
                                                                            {
                                                                                Id = Guid.NewGuid(),
                                                                                RecommendedImmediateAction =
                                                                                    "Test IRN action",
                                                                                Deleted = false
                                                                            });

                                 return checklist;
                             }
                );

            // When
            handler.Object.Handle(message);


            // Then
            _emailSender.Verify(x => x.Send(It.IsAny<RazorEmailResult>()));
            handler.Protected().Verify("CreateRazorEmailResult", Times.Once(),
                                       ItExpr.IsAny<SendIRNNotificationEmailViewModel>());
        }

        [Test]
        public void Given_deleted_irns_When_handle_then_deleted_irns_should_not_be_added_to_email()
        {
            // Given
            var handler = GetTarget();
            var message = new SendIRNNotificationEmail();
            var checklist = new Checklist()
                                {
                                    Id = Guid.NewGuid(),
                                    EmailAddress = "test@test.com"
                                };
            checklist.AddImmediateRiskNotification(new ImmediateRiskNotification()
                                                       {
                                                           Id = Guid.NewGuid(),
                                                           RecommendedImmediateAction = "Test IRN action",
                                                           Deleted = false
                                                       });
            checklist.AddImmediateRiskNotification(new ImmediateRiskNotification()
                                                       {
                                                           Id = Guid.NewGuid(),
                                                           Deleted = true
                                                       });
            checklist.AddImmediateRiskNotification(new ImmediateRiskNotification()
                                                       {
                                                           Id = Guid.NewGuid(),
                                                           Deleted = true
                                                       });

            _checklistRepository.Setup(c => c.GetById(It.IsAny<Guid>()))
                .Returns(() => checklist);

            SendIRNNotificationEmailViewModel viewModelUsedToCreateEmail = null;
            handler.Protected()
                .Setup("CreateRazorEmailResult", ItExpr.IsAny<SendIRNNotificationEmailViewModel>())
                .Callback<SendIRNNotificationEmailViewModel>(viewModel => viewModelUsedToCreateEmail = viewModel);

            // When
            handler.Object.Handle(message);

            // Then
            Assert.That(viewModelUsedToCreateEmail.IRNList.Count, Is.EqualTo(1));
        }

        private Mock<SendIRNNotificationEmailHandler> GetTarget()
        {
            var constructorParameters = new object[]
                                            {_emailSender.Object, _checklistRepository.Object, _urlConfiguration.Object};
            
            var handler = new Mock<SendIRNNotificationEmailHandler>(constructorParameters ){CallBase = true}; 
            handler.Protected()
                .Setup("CreateRazorEmailResult", ItExpr.IsAny<SendIRNNotificationEmailViewModel>());
                

            return handler;
        }

    }


}
