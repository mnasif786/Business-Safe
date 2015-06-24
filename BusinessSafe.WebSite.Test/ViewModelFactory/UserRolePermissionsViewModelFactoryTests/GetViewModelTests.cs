using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.Users;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.Users.Factories;
using BusinessSafe.WebSite.Areas.Users.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.UserRolePermissionsViewModelFactoryTests
{
    [TestFixture]
    [Category("Unit")]
    public class GetViewModelTests
    {
        private Mock<IRolesService> _rolesService;
        private const long _companyId = 1234;
        private Guid _roleId = Guid.NewGuid();


        [SetUp]
        public void Setup()
        {
            _rolesService = new Mock<IRolesService>();
        }

        [Test]
        public void When_get_view_model_Then_return_correct_view_model()
        {
            //Given
            var target = CreateUserRolePermissionsViewModelFactory();
            const string expectedRoleName = "H&SManager";


            var allPermissions = new List<PermissionDto>
                                 {
                                     new PermissionDto()
                                     {
                                         PermissionActivity = PermissionActivity.View,
                                         PermissionGroupId = 1,
                                         PermissionGroupName = "Group1",
                                         Id = 1,
                                         Name = "ViewSiteDetails",
                                         PermissionTargetDto = new PermissionTargetDto() { Name = "SiteDetails" }
                                     },
                                     new PermissionDto()
                                     {
                                         PermissionActivity = PermissionActivity.Add,
                                         PermissionGroupId = 1,
                                         PermissionGroupName = "Group1",
                                         Id = 2,
                                         Name = "AddSiteDetails",
                                         PermissionTargetDto = new PermissionTargetDto() { Name = "SiteDetails" }
                                     },
                                     new PermissionDto()
                                     {
                                         PermissionActivity = PermissionActivity.Edit,
                                         PermissionGroupId = 1,
                                         PermissionGroupName = "Group1",
                                         Id = 3,
                                         Name = "EditSiteDetails",
                                         PermissionTargetDto = new PermissionTargetDto() { Name = "SiteDetails" }
                                     },
                                     new PermissionDto()
                                     {
                                         PermissionActivity = PermissionActivity.View,
                                         PermissionGroupId = 2,
                                         PermissionGroupName = "Group2",
                                         Id = 4,
                                         Name = "ViewEmployeeRecords",
                                         PermissionTargetDto = new PermissionTargetDto() { Name = "EmployeeRecords" }
                                     },
                                     new PermissionDto()
                                     {
                                         PermissionActivity = PermissionActivity.Add,
                                         PermissionGroupId = 2,
                                         PermissionGroupName = "Group2",
                                         Id = 5,
                                         Name = "AddEmployeeRecords",
                                         PermissionTargetDto = new PermissionTargetDto() { Name = "EmployeeRecords" }
                                     }
                                 };

            _rolesService.Setup(x => x.GetAllPermissions()).Returns(allPermissions);

            var rolePermissions = new List<RolePermissionDto>
                                  {
                                      new RolePermissionDto() { Name = expectedRoleName, Permission = new PermissionDto{ Id = 1 }},
                                      new RolePermissionDto() { Name = expectedRoleName, Permission = new PermissionDto { Id = 3} },
                                      new RolePermissionDto() { Name = expectedRoleName, Permission = new PermissionDto { Id = 5}  }
                                  };

            var role = new RoleDto()
                           {
                               Description = expectedRoleName,
                               RolePermissions = rolePermissions
                           };
            _rolesService.Setup(x => x.GetRole(_roleId, _companyId)).Returns(role);


            //When
            var result = target.WithCompanyId(_companyId).WithRoleId(_roleId.ToString()).GetViewModel();

            //Then
            Assert.That(result, Is.TypeOf<UserRolePermissionsViewModel>());
            Assert.That(result.RoleName, Is.EqualTo(expectedRoleName));
            Assert.That(result.PermissionGroups.Count(), Is.EqualTo(2));

            var firstPermissionGroup = result.PermissionGroups.First();
            Assert.That(firstPermissionGroup.PermissionGroupName, Is.EqualTo("Group1"));
            Assert.That(firstPermissionGroup.PermissionTargets.Count, Is.EqualTo(1));
            var firstPermissionGroupViewAddEditDeletes = firstPermissionGroup.PermissionTargets.First();
            Assert.That(firstPermissionGroupViewAddEditDeletes.PermissionTargetName, Is.EqualTo("SiteDetails"));
            Assert.That(firstPermissionGroupViewAddEditDeletes.Permissions.Count, Is.EqualTo(3));
            Assert.That(firstPermissionGroupViewAddEditDeletes.Permissions.First().PermissionActivity, Is.EqualTo(PermissionActivity.View));
            Assert.That(firstPermissionGroupViewAddEditDeletes.Permissions.Skip(1).First().PermissionActivity, Is.EqualTo(PermissionActivity.Add));
            Assert.That(firstPermissionGroupViewAddEditDeletes.Permissions.Skip(2).First().PermissionActivity, Is.EqualTo(PermissionActivity.Edit));

            var secondPermissionGroup = result.PermissionGroups.Last();
            Assert.That(secondPermissionGroup.PermissionGroupName, Is.EqualTo("Group2"));
            Assert.That(secondPermissionGroup.PermissionTargets.Count, Is.EqualTo(1));
            var secondPermissionGroupViewAddEditDeletes = secondPermissionGroup.PermissionTargets.First();
            Assert.That(secondPermissionGroupViewAddEditDeletes.PermissionTargetName, Is.EqualTo("EmployeeRecords"));
            Assert.That(secondPermissionGroupViewAddEditDeletes.Permissions.Count, Is.EqualTo(2));
            Assert.That(secondPermissionGroupViewAddEditDeletes.Permissions.First().PermissionActivity, Is.EqualTo(PermissionActivity.View));
            Assert.That(secondPermissionGroupViewAddEditDeletes.Permissions.Skip(1).First().PermissionActivity, Is.EqualTo(PermissionActivity.Add));

        }

        private UserRolePermissionsViewModelFactory CreateUserRolePermissionsViewModelFactory()
        {
            return new UserRolePermissionsViewModelFactory(_rolesService.Object);
        }
    }
}