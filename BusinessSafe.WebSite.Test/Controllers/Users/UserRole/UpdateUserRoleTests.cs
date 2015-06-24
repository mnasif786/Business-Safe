using System;
using System.Collections.Generic;

using BusinessSafe.Application.Contracts.Users;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Users.Controllers;
using BusinessSafe.WebSite.Areas.Users.ViewModels;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.Users.UserRole
{
    [TestFixture]
    [Category("Unit")]
    public class UpdateUserRoleTests
    {
        private long _companyId;
        private List<string> _permissions;
        private Mock<IRolesService> _rolesService;
        private Guid _roleId;
        private string _roleName;

        [SetUp]
        public void Setup()
        {
            _rolesService = new Mock<IRolesService>();
            _companyId = 200;
            _roleId = Guid.NewGuid();
            _roleName = "test role name";
            _permissions = new List<string>() { "1", "2", "3", "4", "5" };
        }

        [Test]
        public void Given_CreateUserRole_Then_should_call_appropiate_methods()
        {
            // Given
            var controller = CreateUserRoleController();

            _rolesService.Setup(
                x =>
                x.Update(
                    It.Is<UpdateUserRoleRequest>(
                        y => y.RoleId == _roleId &&
                             y.RoleName == _roleName &&
                             y.Permissions.Length == _permissions.Count &&
                             y.CompanyId == _companyId)));

            // When
            controller.UpdateUserRole(new SaveUserRoleViewModel(){RoleId = _roleId.ToString(), RoleName = _roleName,Permisssions = _permissions, CompanyId = _companyId});

            // Then
            _rolesService.VerifyAll();
        }

        private UserRolesController CreateUserRoleController()
        {
            var result = new UserRolesController(_rolesService.Object, null, null);
            return TestControllerHelpers.AddUserToController(result);
        }
    }
}