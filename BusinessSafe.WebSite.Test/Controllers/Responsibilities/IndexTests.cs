using System.Web.Mvc;
using BusinessSafe.Application.Contracts.Responsibilities;
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
    public class IndexWithViewModelTests
    {
        private Mock<IResponsibilitiesService> _responsibilitiesService;
        private Mock<IResponsibilityTaskService> _responsibilityTaskService;
        private Mock<ISearchResponsibilityViewModelFactory> _searchViewModelFactory;
        private Mock<IResponsibilityViewModelFactory> _createViewModelFactory;
        private Mock<ICreateUpdateResponsibilityTaskViewModelFactory> _createUpdateResponsibilityTaskViewModelFactory;
        private Mock<ICompleteResponsibilityTaskViewModelFactory> _completeResponsibilityTaskViewModelFactory;
        private Mock<IBus> _bus;
        private Mock<IBusinessSafeSessionManager> _businessSafeSessionManager;

        [SetUp]
        public void SetUp()
        {
            _responsibilitiesService = new Mock<IResponsibilitiesService>();
            _responsibilityTaskService = new Mock<IResponsibilityTaskService>();
            _searchViewModelFactory = new Mock<ISearchResponsibilityViewModelFactory>();

            _searchViewModelFactory
                .Setup(x => x.WithCompanyId(It.IsAny<long>()))
                .Returns(_searchViewModelFactory.Object);

            _searchViewModelFactory
                .Setup(x => x.WithTitle(It.IsAny<string>()))
                .Returns(_searchViewModelFactory.Object);

             _searchViewModelFactory
                .Setup(x => x.WithPageSize(It.IsAny<int>()))
                .Returns(_searchViewModelFactory.Object);

              _searchViewModelFactory
                .Setup(x => x.WithPageNumber(It.IsAny<int>()))
                .Returns(_searchViewModelFactory.Object);

              _searchViewModelFactory
                .Setup(x => x.WithOrderBy(It.IsAny<string>()))
                .Returns(_searchViewModelFactory.Object);

            _createViewModelFactory = new Mock<IResponsibilityViewModelFactory>();

            _createViewModelFactory
                .Setup(x => x.WithCompanyId(It.IsAny<long>()))
                .Returns(_createViewModelFactory.Object);
       
            _createViewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(new ResponsibilityViewModel());

            _createUpdateResponsibilityTaskViewModelFactory =
                new Mock<ICreateUpdateResponsibilityTaskViewModelFactory>();

            _completeResponsibilityTaskViewModelFactory =
                new Mock<ICompleteResponsibilityTaskViewModelFactory>();

            _bus = new Mock<IBus>();
            _businessSafeSessionManager = new Mock<IBusinessSafeSessionManager>();
        }

        [Test]
        public void Given_get_When_responsibilities_index_page_Then_should_return_index_view()
        {
            // Given
            var controller = GetTarget();
            var viewModel = new ResponsibilitiesIndexViewModel();
            _searchViewModelFactory.Setup(mock => mock.GetViewModel()).Returns(viewModel);
         
            // When
            var result = controller.Index(viewModel) as ViewResult;

            // Then
            Assert.That(result.ViewName, Is.EqualTo("Index"));
        }

        [Test]
        public void Given_get_When_responsibilities_index_page_Then_should_return_ResponsibilitiesIndexViewModel()
        {
            // Given
            var controller = GetTarget();
            var viewModel = new ResponsibilitiesIndexViewModel();
            _searchViewModelFactory.Setup(mock => mock.GetViewModel()).Returns(viewModel);

            // When
            var result = controller.Index(viewModel) as ViewResult;

            // Then
            Assert.That(result.Model, Is.InstanceOf<ResponsibilitiesIndexViewModel>());
        }


        private ResponsibilityController GetTarget()
        {
            var result = new ResponsibilityController(_responsibilitiesService.Object,null,
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