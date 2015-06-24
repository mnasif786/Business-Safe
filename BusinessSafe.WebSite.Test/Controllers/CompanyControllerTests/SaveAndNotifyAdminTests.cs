using System;
using System.Web.Mvc;

using BusinessSafe.Application.Contracts.Company;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Company.ViewModels;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.CompanyControllerTests
{
    [TestFixture]
    [Category("Unit")]
    public class SaveAndNotifyAdminTests
    {
        private Mock<ICompanyDetailsService> _companyDetailsService;
        private Mock<IBusinessSafeCompanyDetailService> _businessSafeCompanyDetailsService;
        CompanyDetailsViewModel _passedViewModel;
        private CompanyDetailsDto _companyDetailsDto;
        private BusinessSafeCompanyDetailDto _businessSafeCompanyDetails;

        [SetUp]
        public void Setup()
        {
            _passedViewModel = new CompanyDetailsViewModel()
                       {
                           Id = TestControllerHelpers.CompanyIdAssigned,
                           AddressLine1 = "address line 1",
                           AddressLine2 = "address line 2",
                           AddressLine3 = "address line 3",
                           AddressLine4 = "address line 4",
                           CAN = "can",
                           CompanyName = "CompanyName",
                           MainContact = "MainContact",
                           PostCode = "PostCode",
                           Telephone = "Telephone",
                           Website = "Website",
                           BusinessSafeContact = "Ryan Giggs"
                       };

            _companyDetailsService = new Mock<ICompanyDetailsService>();
            _companyDetailsService
                .Setup(cdss => cdss.Update(It.IsAny<CompanyDetailsRequest>()));
            
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
            _companyDetailsService
                .Setup(x => x.GetCompanyDetails(It.IsAny<long>()))
                .Returns(_companyDetailsDto);

            _businessSafeCompanyDetailsService = new Mock<IBusinessSafeCompanyDetailService>();
            _businessSafeCompanyDetailsService
                .Setup(b => b.UpdateBusinessSafeContact(It.IsAny<CompanyDetailsRequest>()));

            _businessSafeCompanyDetails = new BusinessSafeCompanyDetailDto()
            {
                BusinessSafeContactEmployeeFullName = "David May",
                BusinessSafeContactEmployeeId = Guid.NewGuid(),
                CompanyId = 1234L
            };

            _businessSafeCompanyDetailsService = new Mock<IBusinessSafeCompanyDetailService>();
            _businessSafeCompanyDetailsService
                .Setup(x => x.Get(It.IsAny<long>()))
                .Returns(_businessSafeCompanyDetails);
        }

        [Test]
        public void Given_that_save_and_notify_admin_is_called_Then_company_detail_is_saved()
        {

            var target = GetTarget();

            //When
            target.SaveAndNotifyAdmin(_passedViewModel);

            //Then
            _companyDetailsService.Verify(x => x.Update(It.Is<CompanyDetailsRequest>(y =>
                y.ActioningUserName == TestControllerHelpers.UserFullNameAssigned &&
                y.NewCompanyDetails.AddressLine1 == _passedViewModel.AddressLine1 &&
                y.NewCompanyDetails.AddressLine2 == _passedViewModel.AddressLine2 &&
                y.NewCompanyDetails.AddressLine3 == _passedViewModel.AddressLine3 &&
                y.NewCompanyDetails.AddressLine4 == _passedViewModel.AddressLine4 &&
                y.NewCompanyDetails.CompanyName == _passedViewModel.CompanyName &&
                y.CAN == _passedViewModel.CAN &&
                y.Id == _passedViewModel.Id &&
                y.NewCompanyDetails.MainContact == _passedViewModel.MainContact &&
                y.NewCompanyDetails.Postcode == _passedViewModel.PostCode &&
                y.NewCompanyDetails.Telephone == _passedViewModel.Telephone &&
                y.NewCompanyDetails.Website == _passedViewModel.Website &&
                y.NewCompanyDetails.BusinessSafeContactName == _passedViewModel.BusinessSafeContact
            )));
        }

        [Test]
        public void Given_that_company_details_is_saved_Then_correct_message_is_displayed()
        {
            //Given            
            var target = GetTarget();

            //When
            target.SaveAndNotifyAdmin(_passedViewModel);

            //Then
            Assert.That(target.TempData["Message"], Is.EqualTo("A member of Client Services has been notified and will be in contact in due course"));
        }

        [Test]
        public void Given_that_company_details_is_saved_Then_return_index_view_with_same_company_id()
        {
            //Given            
            var target = GetTarget();

            //When
            var result = target.SaveAndNotifyAdmin(_passedViewModel) as RedirectToRouteResult;

            //Then
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
            Assert.That(result.RouteValues["id"], Is.EqualTo(TestControllerHelpers.CompanyIdAssigned));
        }

        [Test]
        public void Given_that_model_state_is_invalid_Then_message_should_be_empty_and_it_is_not_redirected()
        {
            //Given            
            var target = GetTarget();
            target.ModelState.AddModelError("AddressLine1", "we need this !!!");

            //When
            var result = target.SaveAndNotifyAdmin(null);

            //Then
            Assert.That(target.TempData.ContainsKey("Message"), Is.False);
            Assert.That(result.GetType(), Is.EqualTo(typeof(ViewResult)));
        }

        [Test]
        public void Given_that_model_state_is_invalid_Then_viewbag_for_label_and_textbox_visibility_is_present()
        {
            //Given
            var target = GetTarget();
            target.ModelState.AddModelError("AddressLine1", "we need this !!!");

            //When
            var result = target.SaveAndNotifyAdmin(null);

            //Then
            Assert.That(((ViewResult)result).ViewBag.LabelVisibility, Is.Not.Null);
            Assert.That(((ViewResult)result).ViewBag.TextBoxVisibility, Is.Not.Null);
        }

        [Test]
        public void Given_that_save_and_notify_admin_is_called_Then_BusinessSafeContactName_is_saved()
        {
            //Given
            var target = GetTarget();

            //When
            target.SaveAndNotifyAdmin(_passedViewModel);

            //Then
            _businessSafeCompanyDetailsService.Verify(x => x.UpdateBusinessSafeContact(
                It.Is<CompanyDetailsRequest>(y => y.NewCompanyDetails.BusinessSafeContactName == _passedViewModel.BusinessSafeContact)));
        }

        [Test]
        public void When_SaveAndNotifyAdmin_Then_old_details_are_added_to_update_company_request()
        {
            //Given
            var target = GetTarget();

            //When
            target.SaveAndNotifyAdmin(_passedViewModel);

            //Then
            _businessSafeCompanyDetailsService.Verify(x => x.UpdateBusinessSafeContact(
                It.Is<CompanyDetailsRequest>(y =>
                    y.ExistingCompanyDetails.AddressLine1 == _companyDetailsDto.AddressLine1 &&
                    y.ExistingCompanyDetails.AddressLine2 == _companyDetailsDto.AddressLine2 &&
                    y.ExistingCompanyDetails.AddressLine3 == _companyDetailsDto.AddressLine3 &&
                    y.ExistingCompanyDetails.AddressLine4 == _companyDetailsDto.AddressLine4 &&
                    y.ExistingCompanyDetails.CompanyName == _companyDetailsDto.CompanyName &&
                    y.ExistingCompanyDetails.MainContact == _companyDetailsDto.MainContact &&
                    y.ExistingCompanyDetails.Postcode == _companyDetailsDto.PostCode &&
                    y.ExistingCompanyDetails.Telephone == _companyDetailsDto.Telephone &&
                    y.ExistingCompanyDetails.Website == _companyDetailsDto.Website &&
                    y.ExistingCompanyDetails.BusinessSafeContactName == _businessSafeCompanyDetails.BusinessSafeContactEmployeeFullName
                )));
        }

        private WebSite.Areas.Company.Controllers.CompanyController GetTarget()
        {
            var controller = new WebSite.Areas.Company.Controllers.CompanyController(_companyDetailsService.Object, _businessSafeCompanyDetailsService.Object);
            return TestControllerHelpers.AddUserToController(controller);
        }
    }
}
