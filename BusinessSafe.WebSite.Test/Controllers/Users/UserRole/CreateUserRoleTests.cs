using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.Users;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Response;
using BusinessSafe.WebSite.Areas.Users.Controllers;
using BusinessSafe.WebSite.Areas.Users.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.Users.UserRole
{
    [TestFixture]
    [Category("Unit")]
    public class CreateUserRoleTests
    {
        private string _roleName;
        private long _companyId;
        private List<string> _permissions;
        private Guid _roleId;
        private Mock<IRolesService> _rolesService;

        [SetUp]
        public void Setup()
        {
            _rolesService = new Mock<IRolesService>();
            _permissions = new List<string>() { "1", "2", "3", "4", "5" };
            _roleId = Guid.NewGuid();
            _companyId = 100;
            _roleName = "Role Name";
        }

        [Test]
        public void Given_CreateUserRole_Then_should_call_appropiate_methods()
        {
            // Given
            var controller = CreateUserRoleController();

            _rolesService.Setup(x => x.Add(It.Is<AddUserRoleRequest>(y =>
                                                                    y.CompanyId == _companyId &&
                                                                    y.RoleName == _roleName &&
                                                                    y.Permissions.Count() == _permissions.Count &&
                                                                    y.UserId != Guid.Empty)))
                        .Returns(new AddUserRoleResponse());

            // When
            controller.CreateUserRole(new SaveUserRoleViewModel() { RoleName = _roleName, Permisssions = _permissions, CompanyId = _companyId });

            // Then
            _rolesService.VerifyAll();
        }

        [Test]
        public void Given_roles_service_does_not_save_When_CreateUserRole_Then_should_return_correct_result()
        {
            // Given
            var controller = CreateUserRoleController();

            _rolesService.Setup(x => x.Add(It.Is<AddUserRoleRequest>(y =>
                                                                    y.CompanyId == _companyId &&
                                                                    y.RoleName == _roleName &&
                                                                    y.Permissions.Count() == _permissions.Count &&
                                                                    y.UserId != Guid.Empty))).Returns(new AddUserRoleResponse()
                                                                                                          {
                                                                                                              Success = false
                                                                                                          });

            // When
            var result = controller.CreateUserRole(new SaveUserRoleViewModel() { RoleName = _roleName, Permisssions = _permissions, CompanyId = _companyId });

            // Then
            dynamic data = result.Data;
            Assert.That(data.Success, Is.EqualTo(false));

        }

        [Test]
        public void Given_roles_service_does_save_When_CreateUserRole_Then_should_return_correct_result()
        {
            // Given
            var controller = CreateUserRoleController();

            _rolesService.Setup(x => x.Add(It.Is<AddUserRoleRequest>(y =>
                                                                    y.CompanyId == _companyId &&
                                                                    y.RoleName == _roleName &&
                                                                    y.Permissions.Count() == _permissions.Count &&
                                                                    y.UserId != Guid.Empty)))
                        .Returns(new AddUserRoleResponse()
                                {
                                    Success = true,
                                    RoleId = _roleId
                                });

            // When
            var result = controller.CreateUserRole(new SaveUserRoleViewModel() { RoleName = _roleName, Permisssions = _permissions, CompanyId = _companyId });

            // Then
            dynamic data = result.Data;
            Assert.That(data.Success, Is.EqualTo(true));
            Assert.That(data.RoleId, Is.EqualTo(_roleId.ToString()));


        }

        private UserRolesController CreateUserRoleController()
        {
            var result = new UserRolesController(_rolesService.Object, null, null);
            return TestControllerHelpers.AddUserToController(result);
        }
    }
}