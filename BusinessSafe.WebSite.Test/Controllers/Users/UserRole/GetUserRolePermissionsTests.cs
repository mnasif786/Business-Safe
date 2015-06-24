using System;

using BusinessSafe.WebSite.Areas.Users.Controllers;
using BusinessSafe.WebSite.Areas.Users.Factories;
using BusinessSafe.WebSite.Areas.Users.ViewModels;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.Users.UserRole
{
    [TestFixture]
    [Category("Unit")]
    public class GetUserRolePermissionsTests
    {
        private Guid roleId = Guid.NewGuid();
        private const long companyId = 1234;
        private Mock<IUserRolePermissionsViewModelFactory> _viewModelFactory;

        [SetUp]
        public void Setup()
        {
            _viewModelFactory = new Mock<IUserRolePermissionsViewModelFactory>();
        }

        [Test]
        public void Given_GetUserRolePermissions_Then_should_call_appropiate_methods()
        {
            // Given
            var controller = CreateUserRoleController();

            _viewModelFactory
                .Setup(x => x.WithCompanyId(companyId))
                .Returns(_viewModelFactory.Object);

            _viewModelFactory
                .Setup(x => x.WithRoleId(roleId.ToString()))
                .Returns(_viewModelFactory.Object);

            _viewModelFactory
                .Setup(x => x.WithEnableCustomRoleEditing(true))
                .Returns(_viewModelFactory.Object);

            var viewModel = new UserRolePermissionsViewModel();
            _viewModelFactory
                .Setup(x => x.GetViewModel()).Returns(viewModel);

            // When
            controller.GetUserRolePermissions(companyId, roleId.ToString(), true);

            // Then
            _viewModelFactory.VerifyAll();
        }

        private UserRolesController CreateUserRoleController()
        {
            return new UserRolesController(null, null, _viewModelFactory.Object);
        }
    }
}