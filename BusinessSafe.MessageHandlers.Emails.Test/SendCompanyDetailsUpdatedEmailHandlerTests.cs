using ActionMailer.Net.Standalone;
using BusinessSafe.MessageHandlers.Emails.EmailSender;
using BusinessSafe.MessageHandlers.Emails.ViewModels;
using BusinessSafe.Messages.Emails.Commands;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.MessageHandlers.Emails.Test
{
    [TestFixture]
    public class SendCompanyDetailsUpdatedEmailHandlerTests
    {
        private Mock<IEmailSender> _emailSender;
        private SendCompanyDetailsUpdatedEmail _message;

        [SetUp]
        public void SetUp()
        {
            _emailSender = new Mock<IEmailSender>();
            _message = new SendCompanyDetailsUpdatedEmail()
            {
                ActioningUserName = "Malcolm Glazer",
                CAN = "CAN",
                CompanyId = 1234L,
                ExistingCompanyDetailsInformation = new CompanyDetailsInformation()
                {
                    AddressLine1 = "old address line 1",
                    AddressLine2 = "old address line 2",
                    AddressLine3 = "old address line 3",
                    AddressLine4 = "old address line 4",
                    BusinessSafeContactName = "Denis Law",
                    CompanyName = "old company name",
                    MainContact = "Alex Ferguson",
                    Postcode = "old postcode",
                    Telephone = "old telephone",
                    Website = "old website"
                },
                NewCompanyDetailsInformation = new CompanyDetailsInformation()
                {
                    AddressLine1 = "address line 1",
                    AddressLine2 = "address line 2",
                    AddressLine3 = "address line 3",
                    AddressLine4 = "address line 4",
                    BusinessSafeContactName = "Danny Welbeck",
                    CompanyName = "company name",
                    MainContact = "David Moyes",
                    Postcode = "postcode",
                    Telephone = "telephone",
                    Website = "website"
                }
            };
        }

        [Test]
        public void When_handle_Then_send_RazorEmailResult()
        {
            //Given
            var handler = CreateTarget();

            //When
            handler.Object.Handle(_message);

            //Then
            _emailSender.Verify(x => x.Send(It.IsAny<RazorEmailResult>()));
        }

        [Test]
        public void When_handle_Then_viewModel_is_populated_with_general_company_details()
        {
            //Given
            SendCompanyDetailsUpdatedEmailViewModel passedViewModel = null;

            var handler = CreateTarget();

            handler
                .Setup(x => x.ViewModelTestHelper(It.IsAny<SendCompanyDetailsUpdatedEmailViewModel>()))
                .Callback<SendCompanyDetailsUpdatedEmailViewModel>(y => passedViewModel = y);


            //When
            handler.Object.Handle(_message);

            //Then
            Assert.That(passedViewModel.ActioningUserName, Is.EqualTo(_message.ActioningUserName));
            Assert.That(passedViewModel.CAN, Is.EqualTo(_message.CAN));
            Assert.That(passedViewModel.CompanyId, Is.EqualTo(_message.CompanyId));
        }

        [Test]
        public void When_handle_Then_viewModel_is_populated_with_existing_company_details()
        {
            //Given
            SendCompanyDetailsUpdatedEmailViewModel passedViewModel = null;

            var handler = CreateTarget();

            handler
                .Setup(x => x.ViewModelTestHelper(It.IsAny<SendCompanyDetailsUpdatedEmailViewModel>()))
                .Callback<SendCompanyDetailsUpdatedEmailViewModel>(y => passedViewModel = y);

            //When
            handler.Object.Handle(_message);

            //Then
            Assert.That(passedViewModel.OldAddressLine1, Is.EqualTo(_message.ExistingCompanyDetailsInformation.AddressLine1));
            Assert.That(passedViewModel.OldAddressLine2, Is.EqualTo(_message.ExistingCompanyDetailsInformation.AddressLine2));
            Assert.That(passedViewModel.OldAddressLine3, Is.EqualTo(_message.ExistingCompanyDetailsInformation.AddressLine3));
            Assert.That(passedViewModel.OldAddressLine4, Is.EqualTo(_message.ExistingCompanyDetailsInformation.AddressLine4));
            Assert.That(passedViewModel.OldBusinssSafeContactName, Is.EqualTo(_message.ExistingCompanyDetailsInformation.BusinessSafeContactName));
            Assert.That(passedViewModel.OldCompanyName, Is.EqualTo(_message.ExistingCompanyDetailsInformation.CompanyName));
            Assert.That(passedViewModel.OldMainContact, Is.EqualTo(_message.ExistingCompanyDetailsInformation.MainContact));
            Assert.That(passedViewModel.OldPostcode, Is.EqualTo(_message.ExistingCompanyDetailsInformation.Postcode));
            Assert.That(passedViewModel.OldTelephone, Is.EqualTo(_message.ExistingCompanyDetailsInformation.Telephone));
            Assert.That(passedViewModel.OldWebsite, Is.EqualTo(_message.ExistingCompanyDetailsInformation.Website));
        }

        [Test]
        public void When_handle_Then_viewModel_is_populated_with_new_company_details()
        {
            //Given

            SendCompanyDetailsUpdatedEmailViewModel passedViewModel = null;

            var handler = CreateTarget();

            handler
                .Setup(x => x.ViewModelTestHelper(It.IsAny<SendCompanyDetailsUpdatedEmailViewModel>()))
                .Callback<SendCompanyDetailsUpdatedEmailViewModel>(y => passedViewModel = y);


            //When
            handler.Object.Handle(_message);

            //Then
            Assert.That(passedViewModel.AddressLine1, Is.EqualTo(_message.NewCompanyDetailsInformation.AddressLine1));
            Assert.That(passedViewModel.AddressLine2, Is.EqualTo(_message.NewCompanyDetailsInformation.AddressLine2));
            Assert.That(passedViewModel.AddressLine3, Is.EqualTo(_message.NewCompanyDetailsInformation.AddressLine3));
            Assert.That(passedViewModel.AddressLine4, Is.EqualTo(_message.NewCompanyDetailsInformation.AddressLine4));
            Assert.That(passedViewModel.BusinssSafeContactName, Is.EqualTo(_message.NewCompanyDetailsInformation.BusinessSafeContactName));
            Assert.That(passedViewModel.CompanyName, Is.EqualTo(_message.NewCompanyDetailsInformation.CompanyName));
            Assert.That(passedViewModel.MainContact, Is.EqualTo(_message.NewCompanyDetailsInformation.MainContact));
            Assert.That(passedViewModel.Postcode, Is.EqualTo(_message.NewCompanyDetailsInformation.Postcode));
            Assert.That(passedViewModel.Telephone, Is.EqualTo(_message.NewCompanyDetailsInformation.Telephone));
            Assert.That(passedViewModel.Website, Is.EqualTo(_message.NewCompanyDetailsInformation.Website));
        }

        private Mock<MySendCompanyDetailsUpdatedEmailHandler> CreateTarget()
        {
            var handler = new Mock<MySendCompanyDetailsUpdatedEmailHandler>(_emailSender.Object) { CallBase = true };
            return handler;
        }
    }
}
