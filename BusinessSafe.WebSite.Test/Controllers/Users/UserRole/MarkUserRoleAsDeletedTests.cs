using System;

using BusinessSafe.Application.Contracts.Users;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.WebSite.Areas.Users.Controllers;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.Users.UserRole
{
    [TestFixture]
    [Category("Unit")]
    public class MarkUserRoleAsDeletedTests
    {
        private long _companyId;
        private Guid _roleId;
        private Mock<IRolesService> _rolesService;
        private string expectedMessage;

        [SetUp]
        public void Setup()
        {
            _rolesService = new Mock<IRolesService>();
            _roleId = Guid.NewGuid();
            _companyId = 100;
            
        }

        [Test]
        public void Given_invalid_request_companyId_not_set_When_mark_user_role_as_deleted_Then_should_return_the_correct_result()
        {
            // Given
             var controller = CreateUserRoleController();

             expectedMessage = "Invalid UserRoleId and CompanyId";

            _companyId = 0;


            // When
            var result = controller.MarkUserRoleAsDeleted(_companyId, _roleId.ToString());

            // Then
            dynamic data = result.Data;
            Assert.That(data.Success, Is.EqualTo(false));
            Assert.That(data.Message, Is.EqualTo(expectedMessage));
        }

        [Test]
        public void Given_invalid_request_userroleid_not_set_When_mark_user_role_as_deleted_Then_should_return_the_correct_result()
        {
            // Given
            var controller = CreateUserRoleController();

            expectedMessage = "Invalid UserRoleId and CompanyId";

            
            // When
            var result = controller.MarkUserRoleAsDeleted(_companyId, "");

            // Then
            dynamic data = result.Data;
            Assert.That(data.Success, Is.EqualTo(false));
            Assert.That(data.Message, Is.EqualTo(expectedMessage));
        }


        [Test]
        public void Given_invalid_request_role_currently_still_in_use_When_mark_user_role_as_deleted_Then_should_return_the_correct_result()
        {
            // Given
            var controller = CreateUserRoleController();

            expectedMessage = "Role is currently in use and can not be deleted.";

            _rolesService
                .Setup(x => x.MarkUserRoleAsDeleted(It.Is<MarkUserRoleAsDeletedRequest>(y => 
                    y.UserRoleId == _roleId && 
                    y.CompanyId == _companyId && 
                    y.UserId != Guid.Empty))).Throws<AttemptingToDeleteRoleCurrentlyUsedByUsersException>();


            // When
            var result = controller.MarkUserRoleAsDeleted(_companyId, _roleId.ToString());

            // Then
            dynamic data = result.Data;
            Assert.That(data.Success, Is.EqualTo(false));
            Assert.That(data.Message, Is.EqualTo(expectedMessage));
        }

        [Test]
        public void Given_valid_request_When_mark_user_role_as_deleted_Then_should_call_correct_methods()
        {
            // Given
            var controller = CreateUserRoleController();

            
            // When
            controller.MarkUserRoleAsDeleted(_companyId, _roleId.ToString());

            // Then
            _rolesService.Verify(x => x.MarkUserRoleAsDeleted(It.Is<MarkUserRoleAsDeletedRequest>(y => y.UserRoleId == _roleId && y.CompanyId == _companyId && y.UserId != Guid.Empty)));
        }

        [Test]
        public void Given_valid_request_When_mark_user_role_as_deleted_Then_should_return_correct_result()
        {
            // Given
            var controller = CreateUserRoleController();


            // When
            var result = controller.MarkUserRoleAsDeleted(_companyId, _roleId.ToString());

            // Then
            dynamic data = result.Data;
            Assert.That(data.Success, Is.EqualTo(true));
        }


        private UserRolesController CreateUserRoleController()
        {
            var result = new UserRolesController(_rolesService.Object, null, null);
            return TestControllerHelpers.AddUserToController(result);
        }
    }
}