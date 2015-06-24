using System.Web.Mvc;

using BusinessSafe.WebSite.Areas.Users.Controllers;
using BusinessSafe.WebSite.Areas.Users.Factories;
using BusinessSafe.WebSite.Tests.Builder;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.Users.UserRole
{
    [TestFixture]
    [Category("Unit")]
    public class IndexTests
    {
        private const long companyId = 1234;
        private Mock<IUserRolesViewModelFactory> _userRolesViewModelFactory;

        [SetUp]
        public void Setup()
        {
            _userRolesViewModelFactory = new Mock<IUserRolesViewModelFactory>();
        }

        [Test]
        public void Given_get_Then_should_return_correct_view()
        {
            // Given
            var controller = CreateUserRoleController();

            _userRolesViewModelFactory
               .Setup(x => x.WithRoleId(null))
               .Returns(_userRolesViewModelFactory.Object);

            _userRolesViewModelFactory
                .Setup(x => x.WithCompanyId(companyId))
                .Returns(_userRolesViewModelFactory.Object);

            // When
            var result = controller.Index(companyId, null) as ViewResult;

            // Then
            Assert.That(result.ViewName, Is.EqualTo(""));
        }

        [Test]
        public void Given_get_When_index_employee_Then_should_return_correct_viewmodel()
        {
            // Given
            var controller = CreateUserRoleController();

            var expectedViewModel = UserRolesViewModelBuilder
                                                .Create()
                                                .Build();

            _userRolesViewModelFactory
                .Setup(x => x.WithCompanyId(companyId))
                .Returns(_userRolesViewModelFactory.Object);

            _userRolesViewModelFactory
               .Setup(x => x.WithRoleId(null))
               .Returns(_userRolesViewModelFactory.Object);

            _userRolesViewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(expectedViewModel);

            // When
            var result = controller.Index(companyId, null) as ViewResult;

            // Then
            Assert.That(result.ViewData.Model, Is.EqualTo(expectedViewModel));
        }

        [Test]
        public void Given_get_When_index_employee_Then_should_call_correct_methods()
        {
            // Given
            var controller = CreateUserRoleController();

            var expectedViewModel = UserRolesViewModelBuilder
                                                .Create()
                                                .Build();

            _userRolesViewModelFactory
                .Setup(x => x.WithCompanyId(companyId))
                .Returns(_userRolesViewModelFactory.Object);

            _userRolesViewModelFactory
                .Setup(x => x.WithRoleId(null))
                .Returns(_userRolesViewModelFactory.Object);

            _userRolesViewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(expectedViewModel);

            // When
            controller.Index(companyId, null);

            // Then
            _userRolesViewModelFactory.VerifyAll();
        }


        private UserRolesController CreateUserRoleController()
        {
            return new UserRolesController(null, _userRolesViewModelFactory.Object, null);
        }
    }
}