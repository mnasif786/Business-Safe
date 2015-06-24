using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Messages.Emails.Commands;
using NServiceBus;
using NUnit.Framework;
using BusinessSafe.Application.RestAPI;
using BusinessSafe.Application.Contracts;
using BusinessSafe.Domain.InfrastructureContracts.Email;
using Moq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.ServiceCompositionGateways;
using BusinessSafe.WebSite.Areas.Sites.ViewModels;

namespace BusinessSafe.WebSite.Tests.ServiceCompositionGateways
{
    [TestFixture]
    public class SiteUpdateCompositionGatewayTests
    {
        private Mock<IClientService> _clientService;
        private Mock<IEmailTemplateService> _emailTemplateService;
        private Mock<ITemplateEngine> _templateEngine;
        private Mock<IEmail> _emailSenderService;
        private long _companyId;
        private long _peninsulaSiteId;
        private long _siteStructureId;
        private SiteAddressDto _siteAddressDto;
        private EmailTemplateDto _emailTemplateDto;
        private string _emailBody;
        private SiteDetailsViewModel _viewModel;
        private Mock<IBus> _bus;
        private string _can;

        [SetUp]
        public void Setup()
        {
            _bus = new Mock<IBus>();
            _clientService = new Mock<IClientService>();
            _emailTemplateService = new Mock<IEmailTemplateService>();
            _templateEngine = new Mock<ITemplateEngine>();
            _emailSenderService = new Mock<IEmail>();
            _companyId = 735L;
            _peninsulaSiteId = 763476L;
            _siteStructureId = 111L;

            _siteAddressDto = new SiteAddressDto
                                  {
                                      AddressLine1 = "Add1",
                                      AddressLine2 = "Add2",
                                      AddressLine3 = "Add3",
                                      AddressLine4 = "Add4",
                                      Postcode = "PC",
                                      Telephone = "01234 567890",
                                      SiteId = _peninsulaSiteId
                                  };

            _can = "my can";
            var _companyDetails = new CompanyDetailsDto(123, "companyName", _can, "address_line1", "address_line2", "address_line3", "address_line4", 123456L, "postcode", "telephone", "website", "main contact"

            );

            _bus.Setup(x => x.Send(It.IsAny<SendSiteDetailsUpdatedEmail>()));

            _clientService
                .Setup(x => x.GetSite(_companyId, _peninsulaSiteId))
                .Returns(_siteAddressDto);

            _clientService
                .Setup(x => x.GetCompanyDetails(_companyId))
                .Returns(_companyDetails);

            _emailTemplateDto = new EmailTemplateDto
                                    {
                                        Subject = "TEST SUBJECT"
                                    };

            _emailTemplateService
                .Setup(x => x.GetByEmailTemplateName(EmailTemplateName.SiteAddressChangeNotification))
                .Returns(_emailTemplateDto);

            _emailBody = "TEST EMAIL BODY";

            _templateEngine
                .Setup(x => x.Render(It.IsAny<CompanyDetails>(), It.IsAny<string>()))
                .Returns(_emailBody);

            _viewModel = new SiteDetailsViewModel
                             {
                                 AddressLine1 = "Add1",
                                 AddressLine2 = "Add2",
                                 AddressLine3 = "Add3",
                                 AddressLine4 = "Add4",
                                 Postcode = "PC",
                                 Telephone = "01234 567890",
                                 SiteId = _peninsulaSiteId,
                                 SiteStructureId = _siteStructureId,
                                 ClientId = _companyId,
                                 SiteStatusUpdated = SiteStatus.NoChange
                             };

            _emailSenderService.SetupSet(x => x.Body = It.IsAny<string>()).Verifiable();
            _emailSenderService.SetupSet(x => x.Subject = It.IsAny<string>()).Verifiable();
            _emailSenderService.SetupSet(x => x.From = It.IsAny<string>()).Verifiable();
        }

        [Test]
        public void Given_address_line_1_has_changed_When_SendEmailIfRequired_is_called_Then_required_methods_are_called_and_result_is_true()
        {
            // Given
            _viewModel.AddressLine1 = "Add1 changed";

            // When
            var result = GetTarget().SendEmailIfRequired(_viewModel);

            // Then
            _bus.Verify(x => x.Send(It.IsAny<SendSiteDetailsUpdatedEmail>()));
            Assert.IsTrue(result);
        }

        [Test]
        public void Given_address_line_2_has_changed_When_SendEmailIfRequired_is_called_Then_required_methods_are_called_and_result_is_true()
        {
            // Given
            _viewModel.AddressLine2 = "Add2 changed";

            // When
            var result = GetTarget().SendEmailIfRequired(_viewModel);

            // Then
            _bus.Verify(x => x.Send(It.IsAny<SendSiteDetailsUpdatedEmail>()));

            Assert.IsTrue(result);
        }

        [Test]
        public void Given_postcode_has_changed_When_SendEmailIfRequired_is_called_Then_required_methods_are_called_and_result_is_true()
        {
            //G iven
            _viewModel.Postcode = "PC changed";

            // When
            var result = GetTarget().SendEmailIfRequired(_viewModel);

            // Then
            _bus.Verify(x => x.Send(It.IsAny<SendSiteDetailsUpdatedEmail>()));

            Assert.IsTrue(result);
        }

        [Test]
        public void Given_telephone_has_changed_When_SendEmailIfRequired_is_called_Then_required_methods_are_called_and_result_is_true()
        {
            // Given
            _viewModel.Telephone = "01234 567890 changed";

            // When
            var result = GetTarget().SendEmailIfRequired(_viewModel);

            // Then
            _bus.Verify(x => x.Send(It.IsAny<SendSiteDetailsUpdatedEmail>()));

            Assert.IsTrue(result);
        }

        [Test]
        public void Given_address_details_have_not_changed_When_SendEmailIfRequired_is_called_Then_required_methods_are_called_and_result_is_false()
        {
            // Given

            // When
            var result = GetTarget().SendEmailIfRequired(_viewModel);

            // Then
            Assert.IsFalse(result);
        }

        [Test]
        public void Given_address_line1_is_changed_When_Then_SendEmailIfRequired_can_is_retrieved_from_service()
        {
            //Given
            _viewModel.AddressLine1 = "Add1 changed";
            var target = GetTarget();

            //When
            target.SendEmailIfRequired(_viewModel);

            //Then
            _bus.Verify(x => x.Send(It.Is<SendSiteDetailsUpdatedEmail>(y => y.CAN == _can)));
        }

        public ISiteUpdateCompositionGateway GetTarget()
        {
            return new SiteUpdateCompositionGateway(
                _clientService.Object,
                _bus.Object
                );
        }
    }
}
