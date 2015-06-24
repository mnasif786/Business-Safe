using System;

using BusinessSafe.Application.Common;
using BusinessSafe.Application.Contracts.Company;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Areas.Company.Controllers;
using BusinessSafe.WebSite.Areas.Company.ViewModels;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.CompanyControllerTests
{
    [TestFixture]
    [Category("Unit")]
    public class IndexTests
    {
        private CompanyDetailsDto _companyDetailsDto;
        private Mock<ICompanyDetailsService> _companyDetailsService;
        private Mock<IBusinessSafeCompanyDetailService> _businessSafeCompanyDetailsService;
        private BusinessSafeCompanyDetailDto returnedBusinessSafeCompanyDetailDto;

        [SetUp]
        public void Setup()
        {
            returnedBusinessSafeCompanyDetailDto = new BusinessSafeCompanyDetailDto()
                                                     {
                                                        BusinessSafeContactEmployeeId  = Guid.NewGuid(),
                                                        BusinessSafeContactEmployeeFullName = "Cristiano Ronaldo"
                                                     };

            _businessSafeCompanyDetailsService = new Mock<IBusinessSafeCompanyDetailService>();
            _businessSafeCompanyDetailsService
                .Setup(x => x.Get(It.IsAny<long>()))
                .Returns(returnedBusinessSafeCompanyDetailDto);

            _companyDetailsDto = new CompanyDetailsDto(
                1,
                "company name",
                "can",
                "address line 1",
                "address line 2",
                "address line 3",
                "address line 4",
                1234L,
                "postcode",
                "telephone",
                "website",
                "main contact"
            );

            _companyDetailsService = new Mock<ICompanyDetailsService>();
            _companyDetailsService
                .Setup(c => c.GetCompanyDetails(It.IsAny<long>()))
                .Returns(_companyDetailsDto);
        }

        [Test]
        public void Given_that_user_goto_CompanyDetails_page_Then_view_name_is_correct()
        {
            // Given
            const string viewName = "Index";
            var target = GetTarget();

            // When
            var result = target.Index();

            // Then
            Assert.That(result.ViewName, Is.EqualTo(viewName));
        }

        [Test]
        public void Given_that_CompanyDetails_page_is_requested_Then_get_company_details_on_company_details_service_is_called()
        {
            // Given
            var target = GetTarget();

            // When
            target.Index();

            // Then            
            _companyDetailsService.VerifyAll();
        }

        [Test]
        public void When_Index_Then_returns_CompanyDetailsViewModel()
        {
            // Given
            var target = GetTarget();

            // When
            var result = target.Index();

            // Then            
            Assert.That(result.Model, Is.InstanceOf<CompanyDetailsViewModel>());
        }

        [Test]
        public void Given_that_user_go_to_company_details_page_When_company_details_is_returned_Then_company_details_are_in_view_model()
        {
            // Given
            var target = GetTarget();

            // When
            var result = target.Index();
            var model = result.Model as CompanyDetailsViewModel;

            // Then            
            Assert.That(model.Id, Is.EqualTo(_companyDetailsDto.Id));
            Assert.That(model.AddressLine1, Is.EqualTo(_companyDetailsDto.AddressLine1));
            Assert.That(model.AddressLine2, Is.EqualTo(_companyDetailsDto.AddressLine2));
            Assert.That(model.AddressLine3, Is.EqualTo(_companyDetailsDto.AddressLine3));
            Assert.That(model.AddressLine4, Is.EqualTo(_companyDetailsDto.AddressLine4));
            Assert.That(model.CAN, Is.EqualTo(_companyDetailsDto.CAN));
            Assert.That(model.CompanyName, Is.EqualTo(_companyDetailsDto.CompanyName));
            Assert.That(model.MainContact, Is.EqualTo(_companyDetailsDto.MainContact));
            Assert.That(model.PostCode, Is.EqualTo(_companyDetailsDto.PostCode));
            Assert.That(model.Telephone, Is.EqualTo(_companyDetailsDto.Telephone));
            Assert.That(model.Website, Is.EqualTo(_companyDetailsDto.Website));
        }

        [Test]
        public void Given_that_CompanyDetails_page_is_requested_Then_message_in_temp_data_is_passed_to_viewbag()
        {
            // Given
            const string expectedMessage = "Admin has been notified";
            var target = GetTarget();

            target.TempData["Message"] = expectedMessage;

            // When
            target.Index();

            // Then            
            Assert.That(target.ViewBag.Message, Is.EqualTo(expectedMessage));
        }

        [Test]
        public void When_Index_Then_view_model_has_bso_contact_employee_id_and_name()
        {
            // Given
            var target = GetTarget();

            // When
            var result = target.Index();
            var model = result.Model as CompanyDetailsViewModel;

            // Then            
            Assert.That(model.BusinessSafeContactId, Is.EqualTo(returnedBusinessSafeCompanyDetailDto.BusinessSafeContactEmployeeId));
            Assert.That(model.BusinessSafeContact, Is.EqualTo(returnedBusinessSafeCompanyDetailDto.BusinessSafeContactEmployeeFullName));
        }

        private CompanyController GetTarget()
        {
            var result = new CompanyController(_companyDetailsService.Object, _businessSafeCompanyDetailsService.Object);

            return TestControllerHelpers.AddUserToController(result);
        }
    }
}