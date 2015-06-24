using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.Users;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Users.Controllers;
using BusinessSafe.WebSite.Areas.Users.Factories;
using BusinessSafe.WebSite.Areas.Users.ViewModels;
using BusinessSafe.WebSite.PeninsulaOnline;
using Moq;
using NHibernate;
using NUnit.Framework;
using System.Security.Principal;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.WebSite.Tests.Controllers.Users.UserSearch
{
    [TestFixture]
    [Category("Unit")]
    public class IndexTests
    {
        private const long _companyId = 100;
        private const string _forename = "Bob";
        private const string _surname = "Smith";
        private const long _groupSiteId = 1234;
        private const bool _showDeleted = false;
        private Mock<IUserSearchViewModelFactory> _userSearchViewModelFactory;
        private UserSearchViewModel _userSearchViewModel;
        private Mock<IViewUserViewModelFactory> _viewUserViewModelFactory;
        private ViewUserViewModel _viewUserViewModel;
        private Mock<IUserService> _userService;

        [SetUp]
        public void Setup()
        {
            _userSearchViewModelFactory = new Mock<IUserSearchViewModelFactory>();
            _userSearchViewModel = new UserSearchViewModel();
            _viewUserViewModelFactory = new Mock<IViewUserViewModelFactory>();
            _viewUserViewModel = new ViewUserViewModel();
            _userService = new Mock<IUserService>();

            _userSearchViewModelFactory
                .Setup(x => x.WithCompanyId(_companyId))
                .Returns(_userSearchViewModelFactory.Object);

            _userSearchViewModelFactory
                .Setup(x => x.WithForeName(It.IsAny<string>()))
                .Returns(_userSearchViewModelFactory.Object);

            _userSearchViewModelFactory
                .Setup(x => x.WithSurname(It.IsAny<string>()))
                .Returns(_userSearchViewModelFactory.Object);

            _userSearchViewModelFactory
                .Setup(x => x.WithSiteId(It.IsAny<long>()))
                .Returns(_userSearchViewModelFactory.Object);

            _userSearchViewModelFactory
                .Setup(x => x.WithSiteGroupId(It.IsAny<long>()))
                .Returns(_userSearchViewModelFactory.Object);

            _userSearchViewModelFactory
                .Setup(x => x.WithShowDeleted(It.IsAny<bool>()))
                .Returns(_userSearchViewModelFactory.Object);

            _userSearchViewModelFactory
                .Setup(x => x.WithAllowedSiteIds(It.IsAny<IList<long>>()))
                .Returns(_userSearchViewModelFactory.Object);

            _userSearchViewModelFactory
                .Setup(x => x.WithCurrentUser(It.IsAny<IPrincipal>()))
                .Returns(_userSearchViewModelFactory.Object);

            _userSearchViewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(_userSearchViewModel);

            _viewUserViewModelFactory
                .Setup(x => x.WithCompanyId(It.IsAny<long>()))
                .Returns(_viewUserViewModelFactory.Object);

            _viewUserViewModelFactory
                .Setup(x => x.WithEmployeeId(It.IsAny<Guid>()))
                .Returns(_viewUserViewModelFactory.Object);

            _viewUserViewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(_viewUserViewModel);

            _userService
                .Setup(x => x.Search(CreateUserSearchRequest()))
                .Returns(new List<UserDto>());
        }

        [Test]
        public void Given_get_When_search_users_Then_should_return_correct_view()
        {
            // Given
            var controller = CreateUserSearchController();

            // When
            var result = controller.Index(_companyId) as ViewResult;

            // Then
            Assert.That(result.ViewName, Is.EqualTo(""));
        }

        [Test]
        public void Given_get_when_search_users_then_should_return_correct_view_model()
        {
            // Given
            var controller = CreateUserSearchController();
            var expectedViewModel = _userSearchViewModel;

            // When
            var result = controller.Index(_companyId) as ViewResult;

            // Then
            Assert.That(result.ViewData.Model, Is.EqualTo(expectedViewModel));
        }

        [Test]
        public void Given_get_when_view_user_then_should_return_correct_view_model()
        {
            // Given
            var controller = CreateUserSearchController();

            //When
            var result = controller.ViewUser(It.IsAny<long>(), It.IsAny<Guid>());

            // Then
            Assert.That(result.ViewData.Model, Is.InstanceOf<ViewUserViewModel>());
        }

        [Test]
        public void Given_get_when_view_user_then_should_call_correct_methods()
        {
            // Given
            var controller = CreateUserSearchController();

            //When
            var result = controller.ViewUser(It.IsAny<long>(), It.IsAny<Guid>());

            // Then
            Assert.That(result.ViewData.Model, Is.InstanceOf<ViewUserViewModel>());
        }

        [Test]
        public void When_employee_id_supplied_then_should_call_correct_methods()
        {
            // Given
            var companyId = 1;
            var employeeId = new Guid();
            var controller = CreateUserSearchController();

            // When
            controller.ViewUser(companyId, employeeId);

            // Then
            _viewUserViewModelFactory.Verify(x => x.WithCompanyId(companyId));
            _viewUserViewModelFactory.Verify(x => x.WithEmployeeId(employeeId));
            _viewUserViewModelFactory.Verify(x => x.GetViewModel());
 }

        [Test]
        public void Given_search_by_site_requested_When_Index_Then_pass_to_view_model_factory()
        {
            // Given
            const long siteId = 1245L;

            var controller = CreateUserSearchController();

            // When
            controller.Index(_companyId, string.Empty, string.Empty, siteId, 0, false);

            // Then
            _userSearchViewModelFactory.Verify(x => x.WithSiteId(siteId));
        }

        private ViewUsersController CreateUserSearchController()
        {
            var sessionManager = new Mock<IBusinessSafeSessionManager>();
            var session = new Mock<ISession>();
            sessionManager.SetupGet(x => x.Session).Returns(session.Object);

            var result = new ViewUsersController(_userSearchViewModelFactory.Object, _viewUserViewModelFactory.Object, _userService.Object, null, new Mock<INewRegistrationRequestService>().Object, sessionManager.Object, null);
            TestControllerHelpers.AddUserToController(result);
            return result;
        }

        private SearchUsersRequest CreateUserSearchRequest()
        {
            var userSearchRequest = new SearchUsersRequest()
            {
                CompanyId = _companyId,
                ForenameLike = string.IsNullOrEmpty(_forename) ? null : _forename + "%",
                SurnameLike = string.IsNullOrEmpty(_surname) ? null : _surname + "%",
                SiteId = _groupSiteId,
                ShowDeleted = _showDeleted
            };

            return userSearchRequest;
        }
    }
}