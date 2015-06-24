using System;

using BusinessSafe.Application.Contracts.Company;
using BusinessSafe.Application.Implementations.Company;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Messages.Emails.Commands;
using Moq;
using NServiceBus;
using NUnit.Framework;
using StructureMap;

namespace BusinessSafe.Application.Tests.Implementations.CompanyDetailsServiceTests
{
    [TestFixture]
    [Category("Unit")]
    public class SaveTests
    {
        private Mock<IPeninsulaLog> _log;
        private Mock<IBus> _bus;
        private CompanyDetailsRequest _request;

        [SetUp]
        public void SetUp()
        {
             _request = new CompanyDetailsRequest("companyName", "can", "address1", "address2", "address3", "address4",
                "postcode", "tel", "website", "mainContact", "actioningUser", "businessSafeContact");

            var employee = new EmployeeForAuditing()
                           {
                               Forename = "George",
                               Surname = "Best",
                               Id = Guid.NewGuid()
                           };

            _request.ExistingCompanyDetails = new Application.Request.CompanyDetailsInformation()
                                             {
                                                 AddressLine1 = "existing address line 1",
                                                 AddressLine2 = "existing address line 2",
                                                 AddressLine3 = "existing address line 3",
                                                 AddressLine4 = "existing address line 4",
                                                 CompanyName = "existing CompanyName",
                                                 MainContact = "existing MainContact",
                                                 Postcode = "existing PostCode",
                                                 Telephone = "existing Telephone",
                                                 Website = "existing Website",
                                                 BusinessSafeContactId = employee.Id,
                                                 BusinessSafeContactName = employee.FullName
                                             };
            ;

            _bus = new Mock<IBus>();
            _log = new Mock<IPeninsulaLog>();

            ObjectFactory.Inject<IPeninsulaLog>(new StubPeninsulaLog());
        }

        [Test]
        public void When_update_company_details_Then_calls_bus_to_publish_send_company_details_updated_email_command_with_new_company_details_info()
        {
            //Given
            ICompanyDetailsService target = new CompanyDetailsService(null, _log.Object, _bus.Object);

            //When
            target.Update(_request);

            //Then
            _bus.Verify(x => x.Send(It.Is<SendCompanyDetailsUpdatedEmail>(y =>
                y.NewCompanyDetailsInformation.CompanyName == _request.NewCompanyDetails.CompanyName &&
                y.CAN == _request.CAN &&
                y.NewCompanyDetailsInformation.AddressLine1 == _request.NewCompanyDetails.AddressLine1 &&
                y.NewCompanyDetailsInformation.AddressLine2 == _request.NewCompanyDetails.AddressLine2 &&
                y.NewCompanyDetailsInformation.AddressLine3 == _request.NewCompanyDetails.AddressLine3 &&
                y.NewCompanyDetailsInformation.AddressLine4 == _request.NewCompanyDetails.AddressLine4 &&
                y.NewCompanyDetailsInformation.Postcode == _request.NewCompanyDetails.Postcode &&
                y.NewCompanyDetailsInformation.Telephone == _request.NewCompanyDetails.Telephone &&
                y.NewCompanyDetailsInformation.Website == _request.NewCompanyDetails.Website &&
                y.NewCompanyDetailsInformation.MainContact == _request.NewCompanyDetails.MainContact &&
                y.ActioningUserName == _request.ActioningUserName &&
                y.NewCompanyDetailsInformation.BusinessSafeContactName == _request.NewCompanyDetails.BusinessSafeContactName
            )), Times.Once());
        }

        [Test]
        public void When_update_company_details_Then_calls_bus_to_publish_send_company_details_updated_email_command_with_existing_company_details_info()
        {
            //Given
            ICompanyDetailsService target = new CompanyDetailsService(null, _log.Object, _bus.Object);

            //When
            target.Update(_request);

            //Then
            _bus.Verify(x => x.Send(It.Is<SendCompanyDetailsUpdatedEmail>(y =>
                y.NewCompanyDetailsInformation.CompanyName == _request.NewCompanyDetails.CompanyName &&
                y.CAN == _request.CAN &&
                y.ExistingCompanyDetailsInformation.AddressLine1 == _request.ExistingCompanyDetails.AddressLine1 &&
                y.ExistingCompanyDetailsInformation.AddressLine2 == _request.ExistingCompanyDetails.AddressLine2 &&
                y.ExistingCompanyDetailsInformation.AddressLine3 == _request.ExistingCompanyDetails.AddressLine3 &&
                y.ExistingCompanyDetailsInformation.AddressLine4 == _request.ExistingCompanyDetails.AddressLine4 &&
                y.ExistingCompanyDetailsInformation.Postcode == _request.ExistingCompanyDetails.Postcode &&
                y.ExistingCompanyDetailsInformation.Telephone == _request.ExistingCompanyDetails.Telephone &&
                y.ExistingCompanyDetailsInformation.Website == _request.ExistingCompanyDetails.Website &&
                y.ExistingCompanyDetailsInformation.MainContact == _request.ExistingCompanyDetails.MainContact &&
                y.ActioningUserName == _request.ActioningUserName &&
                y.ExistingCompanyDetailsInformation.BusinessSafeContactName == _request.ExistingCompanyDetails.BusinessSafeContactName
            )), Times.Once());
        }
    }
}
