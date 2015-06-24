using System;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.Responsibilities;
using BusinessSafe.Application.Request.Responsibilities;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.Responsibilities.Controllers;
using BusinessSafe.WebSite.Areas.Responsibilities.Factories;
using BusinessSafe.WebSite.Areas.Responsibilities.ViewModels;
using Moq;
using NUnit.Framework;
using NServiceBus;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.WebSite.Tests.Controllers.Responsibilities
{
    [TestFixture]
    [Category("Unit")]
    public class CreateWithViewModelTests
    {
        private Mock<ISearchResponsibilityViewModelFactory> _searchViewModelFactory;
        private Mock<IResponsibilityTaskService> _responsibilityTaskService;
        private Mock<IResponsibilityViewModelFactory> _createViewModelFactory;
        private Mock<IResponsibilitiesService> _responsibilitiesService;
        private Mock<ICreateUpdateResponsibilityTaskViewModelFactory> _createUpdateResponsibilityTaskViewModelFactory;
        private Mock<ICompleteResponsibilityTaskViewModelFactory> _completeResponsibilityTaskViewModelFactory;
        private Mock<IBus> _bus;
        private Mock<IBusinessSafeSessionManager> _businessSafeSessionManager;

        [SetUp]
        public void SetUp()
        {
            _searchViewModelFactory = new Mock<ISearchResponsibilityViewModelFactory>();

            _searchViewModelFactory
                .Setup(x => x.WithCompanyId(It.IsAny<long>()))
                .Returns(_searchViewModelFactory.Object);

            _createViewModelFactory = new Mock<IResponsibilityViewModelFactory>();

            _createViewModelFactory
                .Setup(x => x.WithCompanyId(It.IsAny<long>()))
                .Returns(_createViewModelFactory.Object);


            _createViewModelFactory
                .Setup(x => x.WithResponsibilityId(It.IsAny<long>()))
                .Returns(_createViewModelFactory.Object);

            _createViewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(new ResponsibilityViewModel());

            _responsibilitiesService = new Mock<IResponsibilitiesService>();
            _responsibilityTaskService = new Mock<IResponsibilityTaskService>();

            _createUpdateResponsibilityTaskViewModelFactory =
                new Mock<ICreateUpdateResponsibilityTaskViewModelFactory>();

            _completeResponsibilityTaskViewModelFactory =
                new Mock<ICompleteResponsibilityTaskViewModelFactory>();

            _bus = new Mock<IBus>();
            _businessSafeSessionManager = new Mock<IBusinessSafeSessionManager>();
        }

        [Test]
        public void When_get_create_Then_should_return_correct_view()
        {
            //Given
            var target = GetTarget();

            //When
            var result = target.Create(1) as ViewResult;

            //Then
            Assert.That(result.ViewName, Is.EqualTo("Create"));
        }


        [Test]
        public void When_get_create_Then_should_return_correct_view_model()
        {
            //Given
            var target = GetTarget();

            //When
            var result = target.Create(1) as ViewResult;

            //Then
            Assert.That(result.Model, Is.TypeOf<ResponsibilityViewModel>());
        }

        [Test]
        public void When_post_create_valid_viewmodel_Then_should_call_correct_methods()
        {
            //Given
            var target = GetTarget();
            var viewModel = new ResponsibilityViewModel
                                {
                                    CategoryId = default(int),
                                    Title = "R1",
                                    Description = "Responsibility1",
                                    SiteId = default(int),
                                    ReasonId = default(int),
                                    OwnerId = new Guid(),
                                    FrequencyId = (int)TaskReoccurringType.Annually
                                };

            //When
            var result = target.Create(viewModel) as ViewResult;

            //Then
            _responsibilitiesService
                .Verify(x => x.SaveResponsibility(It.IsAny<SaveResponsibilityRequest>()), Times.Once());
        }

        private ResponsibilityController GetTarget()
        {
            var result = new ResponsibilityController(
                _responsibilitiesService.Object, 
                null,
                _responsibilityTaskService.Object,
                _searchViewModelFactory.Object,
                _createViewModelFactory.Object,
                _createUpdateResponsibilityTaskViewModelFactory.Object,
                _completeResponsibilityTaskViewModelFactory.Object, null,
                null, 
                _bus.Object,
                _businessSafeSessionManager.Object);

            return TestControllerHelpers.AddUserToController(result);
        }    
    }
}