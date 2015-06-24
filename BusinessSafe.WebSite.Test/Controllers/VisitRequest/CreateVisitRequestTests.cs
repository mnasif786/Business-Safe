using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.Company;
using BusinessSafe.Application.Contracts.VisitRequest;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.VisitRequest.Controllers;
using BusinessSafe.WebSite.Areas.VisitRequest.Factories;
using BusinessSafe.WebSite.Areas.VisitRequest.ViewModels;
using Moq;
using NUnit.Framework;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.WebSite.Tests.Controllers.VisitRequest
{
    [TestFixture]
    public class CreateVisitRequestTests
    {
        private Mock<VisitRequestViewModel> _passedViewModel;
        private VisitRequestViewModel _validViewModel;
        private VisitRequestViewModel _inValidViewModel;
        private List<ValidationResult> _validationResultWithErrors;
        private Mock<IBusinessSafeSessionManager> _businessSafeSessionManager;
        private Mock<IVisitRequestService> _visitRequestService;
        private Mock<IVisitRequestViewModelFactory> _visitRequestViewModelFactory;
        private Mock<ICompanyDetailsService> _companyDetailsService;
        private CompanyDetailsDto _companyDetailsDto;

        [SetUp]
        public void Setup()
        {
           
            _visitRequestService = new Mock<IVisitRequestService>();
            _passedViewModel = new Mock<VisitRequestViewModel>();
            
            //View Model Factory
            _visitRequestViewModelFactory = new Mock<IVisitRequestViewModelFactory>();
            _visitRequestViewModelFactory.Setup(x => x.WithCompanyId(It.IsAny<long>()))
               .Returns(_visitRequestViewModelFactory.Object);
            _visitRequestViewModelFactory.Setup(x => x.WithEmailAddress(It.IsAny<string>()))
                .Returns(_visitRequestViewModelFactory.Object);
            _visitRequestViewModelFactory.Setup(x => x.WithPersonToVisit(It.IsAny<string>()))
                .Returns(_visitRequestViewModelFactory.Object);
            _visitRequestViewModelFactory.Setup(x => x.WithAllowedSiteIds(It.IsAny<IList<long>>()))
               .Returns(_visitRequestViewModelFactory.Object);
            _visitRequestViewModelFactory.Setup(x => x.WithVisitTimePreference(It.IsAny<string>()))
               .Returns(_visitRequestViewModelFactory.Object);
            _visitRequestViewModelFactory.Setup(x => x.GetViewModel())
              .Returns(_passedViewModel.Object);

            //InvalidCastException view Model with wrong email address
            _inValidViewModel = new VisitRequestViewModel()
            {
                Comments = "abc",
                SiteId = 1,
                ContactNumber = "1231",
                EmailAddress = "asa@asd",
                PersonNameToVisit = "xyz",
                Sites = null,
                VisitFrom = DateTime.Now.Date.ToString(),
                VisitTo = DateTime.Now.Date.ToString(),
                VisitTimePreference = "AM"
            };

            _validViewModel = new VisitRequestViewModel()
            {
                Comments = "abc",
                SiteId = 1,
                ContactNumber = "1231",
                EmailAddress = "asa@asd.com",
                PersonNameToVisit = "xyz",
                Sites = null,
                VisitFrom = DateTime.Now.Date.ToString(),
                VisitTo = DateTime.Now.Date.ToString(),
                VisitTimePreference = "AM"
            };
            _businessSafeSessionManager = new Mock<IBusinessSafeSessionManager>();
        }

        [Test]
        public void Given_get_When_index_visit_request_Then_should_return_correct_view()
        {
            // Given
            var controller = GetTarget();

            // When
            var result = controller.Index() as ViewResult;

            // Then
            Assert.That(result.ViewName, Is.EqualTo("Index"));
        }

        [Test]
        public void Given_get_When_index_visit_request_Then_should_return_correct_viewmodel()
        {
            // Given
            var controller = GetTarget();
            
            var expectedViewModel = _passedViewModel.Object;
            
            _visitRequestViewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(expectedViewModel);

            // When
            var result = controller.Index() as ViewResult;

            // Then
            Assert.That(result.ViewData.Model, Is.EqualTo(expectedViewModel));
        }

       [Test]
        public void When_create_viewmodel_validation_is_called()
        {
            // Given
            var target = GetTarget();

            _visitRequestViewModelFactory
                .Setup(x => x.GetViewModel(_inValidViewModel))
                .Returns(_inValidViewModel);

            // When
            var result = target.Create(_inValidViewModel);

            // Then
            _passedViewModel.VerifyAll();
        }

        [Test]
        public void When_create_and_viewmodel_is_inValid_return_failed_model_state()
        {
            // Given
            var target = GetTarget();
           
            // When
            dynamic result = target.Create(_inValidViewModel);
            
            // Then
            Assert.IsFalse(target.ModelState.IsValid);
        }
        
        [Test]
        public void When_Create_with_valid_view_model_Then_tell_service_to_send_notification_email()
        {
            // Given
            var target = GetTarget();

            _visitRequestViewModelFactory
              .Setup(x => x.GetViewModel(_validViewModel))
              .Returns(_validViewModel);

            // When
            target.Create(_validViewModel);

            // Then
            _visitRequestService.Verify(x => x.SendVisitRequestedEmail(It.IsAny<RequestVisitRequest>()));
        }

        private VisitRequestController GetTarget()
        {
            var controller = new VisitRequestController(_businessSafeSessionManager.Object,_visitRequestService.Object, _visitRequestViewModelFactory.Object);
            return TestControllerHelpers.AddUserToController(controller);
        }
    }
}
