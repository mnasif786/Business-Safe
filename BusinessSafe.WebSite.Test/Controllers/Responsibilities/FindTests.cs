using System;
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
    public class FindTests
    {
        private Mock<ISearchResponsibilityViewModelFactory> _searchViewModelFactory;
        private Mock<ICreateUpdateResponsibilityTaskViewModelFactory> _createUpdateResponsibilityTaskViewModelFactory;
        private Mock<ICompleteResponsibilityTaskViewModelFactory> _completeResponsibilityTaskViewModelFactory;
        private Mock<IResponsibilityTaskService> _responsibilityTaskService;
        private ResponsibilitiesIndexViewModel _request;
        private ResponsibilitiesIndexViewModel _returnedModel;
        private Mock<IBus> _bus;
        private Mock<IBusinessSafeSessionManager> _businessSafeSessionManager;

        [SetUp]
        public void SetUp()
        {
            _returnedModel = new ResponsibilitiesIndexViewModel();
            _request = new ResponsibilitiesIndexViewModel()
                           {
                               CategoryId = 1234L,
                               SiteId = 12345L,
                               SiteGroupId = 3574L,
                               Title = "title",
                               CreatedFrom = DateTime.Now.AddDays(-1434).ToShortDateString(),
                               CreatedTo = DateTime.Now.AddDays(-2).ToShortDateString(),
                               IsShowDeleted = true
                           };
            _searchViewModelFactory = new Mock<ISearchResponsibilityViewModelFactory>();

            _searchViewModelFactory
                .Setup(x => x.WithCompanyId(It.IsAny<long>()))
                .Returns(_searchViewModelFactory.Object);

            _searchViewModelFactory
                .Setup(x => x.WithCategoryId(It.IsAny<long>()))
                .Returns(_searchViewModelFactory.Object);

            _searchViewModelFactory
                .Setup(x => x.WithSiteId(It.IsAny<long>()))
                .Returns(_searchViewModelFactory.Object);

            _searchViewModelFactory
                .Setup(x => x.WithSiteGroupId(It.IsAny<long>()))
                .Returns(_searchViewModelFactory.Object);

            _searchViewModelFactory
                .Setup(x => x.WithTitle(It.IsAny<string>()))
                .Returns(_searchViewModelFactory.Object);

            _searchViewModelFactory
                .Setup(x => x.WithCreatedFrom(It.IsAny<DateTime>()))
                .Returns(_searchViewModelFactory.Object);

            _searchViewModelFactory
                .Setup(x => x.WithCreatedTo(It.IsAny<DateTime>()))
                .Returns(_searchViewModelFactory.Object);

            _searchViewModelFactory
                .Setup(x => x.WithShowDeleted(It.IsAny<bool>()))
                .Returns(_searchViewModelFactory.Object);

            _searchViewModelFactory
              .Setup(x => x.WithPageNumber(It.IsAny<int>()))
              .Returns(_searchViewModelFactory.Object);

            _searchViewModelFactory
              .Setup(x => x.WithPageSize(It.IsAny<int>()))
              .Returns(_searchViewModelFactory.Object);

            _searchViewModelFactory
                .Setup(x => x.WithOrderBy(It.IsAny<string>()))
                .Returns(_searchViewModelFactory.Object);

            _searchViewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(_returnedModel);

            _createUpdateResponsibilityTaskViewModelFactory =
                new Mock<ICreateUpdateResponsibilityTaskViewModelFactory>();

            _completeResponsibilityTaskViewModelFactory =
                new Mock<ICompleteResponsibilityTaskViewModelFactory>();

            _bus = new Mock<IBus>();
            _businessSafeSessionManager = new Mock<IBusinessSafeSessionManager>();

            _responsibilityTaskService = new Mock<IResponsibilityTaskService>();
        }

        [Test]
        public void When_Find_Then_return_Index_view()
        {
            // Given
            var controller = GetTarget();
            var viewModel = new ResponsibilitiesIndexViewModel();
            _searchViewModelFactory.Setup(mock => mock.GetViewModel()).Returns(viewModel);
         
            // When
            var result = controller.Find(viewModel) as ViewResult;

            // Then
            Assert.That(result.ViewName, Is.EqualTo("Index"));
        }

        [Test]
        public void When_Find_Then_return_view_model_from_factory()
        {
            // Given
            var controller = GetTarget();

            // When
            var result = controller.Find(_request);

            // Then
            Assert.That(result.Model, Is.EqualTo(_returnedModel));
        }

        [Test]
        public void When_Find_Then_set_company_id()
        {
            // Given
            var controller = GetTarget();

            // When
            var result = controller.Find(_request);

            // Then
            _searchViewModelFactory.Verify(x => x.WithCompanyId(TestControllerHelpers.CompanyIdAssigned));
        }

        [Test]
        public void When_Find_Then_set_category_id()
        {
            // Given
            var controller = GetTarget();

            // When
            var result = controller.Find(_request);

            // Then
            _searchViewModelFactory.Verify(x => x.WithCategoryId(_request.CategoryId.Value));
        }

        [Test]
        public void When_Find_Then_set_site_id()
        {
            // Given
            var controller = GetTarget();

            // When
            var result = controller.Find(_request);

            // Then
            _searchViewModelFactory.Verify(x => x.WithSiteId(_request.SiteId.Value));
        }

        [Test]
        public void When_Find_Then_set_sitegroup_id()
        {
            // Given
            var controller = GetTarget();

            // When
            var result = controller.Find(_request);

            // Then
            _searchViewModelFactory.Verify(x => x.WithSiteGroupId(_request.SiteGroupId.Value));
        }

        [Test]
        public void When_Find_Then_set_title()
        {
            // Given
            var controller = GetTarget();

            // When
            var result = controller.Find(_request);

            // Then
            _searchViewModelFactory.Verify(x => x.WithTitle(_request.Title));
        }

        [Test]
        public void When_Find_Then_set_createdFrom()
        {
            // Given
            var controller = GetTarget();

            // When
            var result = controller.Find(_request);

            // Then
            _searchViewModelFactory.Verify(x => x.WithCreatedFrom(DateTime.Parse(_request.CreatedFrom).Date));
        }

        [Test]
        public void When_Find_Then_set_createdTo()
        {
            // Given
            var controller = GetTarget();

            // When
            var result = controller.Find(_request);

            // Then
            _searchViewModelFactory.Verify(x => x.WithCreatedTo(DateTime.Parse(_request.CreatedTo).Date));
        }

        [Test]
        public void When_Find_Then_set_show_deleted()
        {
            // Given
            var controller = GetTarget();

            // When
            var result = controller.Find(_request);

            // Then
            _searchViewModelFactory.Verify(x => x.WithShowDeleted(_request.IsShowDeleted));
        }

        private ResponsibilityController GetTarget()
        {
            var result = new ResponsibilityController(null, null,
                                                      _responsibilityTaskService.Object,
                                                      _searchViewModelFactory.Object,
                                                      null,
                                                      _createUpdateResponsibilityTaskViewModelFactory.Object,
                                                      _completeResponsibilityTaskViewModelFactory.Object, null,
                                                      null, 
                                                      _bus.Object,
                                                      _businessSafeSessionManager.Object);

            return TestControllerHelpers.AddUserToController(result);
        }
    }
}