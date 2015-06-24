using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.Contracts.Users;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Users.Factories;
using BusinessSafe.WebSite.Areas.Users.ViewModels;
using Moq;
using NUnit.Framework;
using BusinessSafe.WebSite.AuthenticationService;
using System.Security.Principal;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.UserPermissionsViewModelFactoryTests
{
    [TestFixture]
    [Category("Unit")]
    public class GetViewModelTests
    {
        private Mock<IEmployeeService> _employeeService;
        private Mock<IRolesService> _roleService;
        private Mock<ISiteService> _siteService;
        private Mock<ISiteGroupService> _siteGroupService;
        private RoleDto[] _roles;
        private EmployeeDto[] _notRegisteredEmployees;
        private EmployeeDto[] _registeredEmployees;
        private List<EmployeeDto> _employees;
        private SiteDto[] _sites;
        private SiteGroupDto[] _siteGroups;
        private long _companyId = 999L;

        [SetUp]
        public void Setup()
        {
            _employeeService = new Mock<IEmployeeService>();
            _roleService = new Mock<IRolesService>();
            _siteService = new Mock<ISiteService>();
            _siteGroupService = new Mock<ISiteGroupService>();

            _roles = new[]
                            {
                                new RoleDto
                                    {
                                        Id = Guid.NewGuid(),
                                        Name = "Role1",
                                        Description = "Role 1"
                                    },
                                new RoleDto
                                    {
                                        Id = Guid.NewGuid(),
                                        Name = "Role2",
                                        Description = "Role 2"
                                    }
                            };

            _employees = new List<EmployeeDto>()
                            {
                                new EmployeeDto
                                    {
                                        Id = Guid.NewGuid(),
                                        EmployeeReference = "EMP001",
                                        FullName = "Barry Blue",
                                        JobTitle = "Job Title 01"
                                    },
                                new EmployeeDto
                                    {
                                        Id = Guid.NewGuid(),
                                        EmployeeReference = "EMP002",
                                        FullName = "Percy Purple",
                                        JobTitle = "Job Title 02"
                                    }
                            };

            _notRegisteredEmployees = new[]
                            {
                                new EmployeeDto
                                    {
                                        Id = Guid.NewGuid(),
                                        EmployeeReference = "EMP001",
                                        FullName = "Barry Blue",
                                        JobTitle = "Job Title 01"
                                    },
                                new EmployeeDto
                                    {
                                        Id = Guid.NewGuid(),
                                        EmployeeReference = "EMP002",
                                        FullName = "Percy Purple",
                                        JobTitle = "Job Title 02"
                                    }
                            };

            _registeredEmployees = new[]
                            {
                                new EmployeeDto
                                    {
                                        Id = Guid.NewGuid(),
                                        EmployeeReference = "EMP003",
                                        FullName = "Dave Barry Blue",
                                        JobTitle = "Job Title 01"
                                    },
                                new EmployeeDto
                                    {
                                        Id = Guid.NewGuid(),
                                        EmployeeReference = "EMP004",
                                        FullName = "John Percy Purple",
                                        JobTitle = "Job Title 02"
                                    }
                            };

            _sites = new[]
                            {
                                new SiteDto{Id = 1, Name = "Site 1", IsMainSite = true},
                                new SiteDto{ Id = 2, Name = "Site 2"}
                            };

            _siteGroups = new[]
                                 {
                                     new SiteGroupDto{ Id = 1, Name = "Site Group 1" },
                                     new SiteGroupDto{ Id = 2, Name = "Site Group 2" }
                                 };

            _employeeService.Setup(x => x.Search(It.IsAny<SearchEmployeesRequest>())).Returns(_employees);
            _employeeService.Setup(x => x.GetEmployee(_notRegisteredEmployees[0].Id, _companyId)).Returns(_notRegisteredEmployees[0]);
            _roleService.Setup(x => x.GetAllRoles(_companyId)).Returns(_roles);
            _siteService.Setup(x => x.GetByCompanyId(_companyId)).Returns(_sites);
            _siteGroupService.Setup(x => x.GetByCompanyId(_companyId)).Returns(_siteGroups);
        }

     
       [Test]
        public void Given_no_employee_is_set_When_get_view_model_Then_return_correct_view_model()
        {
            //Given
           SearchEmployeesRequest passedSearchEmployeesRequest = null;
            _employeeService
                .Setup(x => x.Search(It.IsAny<SearchEmployeesRequest>()))
                .Callback<SearchEmployeesRequest>(y => passedSearchEmployeesRequest = y)
                .Returns(_employees);

            var target = CreateUserPermissionsViewModelFactory();

            //When
            var result = target.GetViewModel(_companyId, null, null, true);

            //Then
            Assert.That(result, Is.TypeOf<AddUsersViewModel>());
            Assert.AreEqual(_companyId, result.CompanyId);
            Assert.That(result.Employees.Count(), Is.EqualTo(_employees.Count + 1)); // -- select option -- dropdown option

            Assert.That(passedSearchEmployeesRequest.CompanyId, Is.EqualTo(_companyId));
            Assert.That(passedSearchEmployeesRequest.ShowDeleted, Is.EqualTo(false));

            Assert.AreEqual(Guid.Empty, result.EmployeeId);
            Assert.AreEqual(default(Guid), result.UserId);
            Assert.AreEqual(null, result.EmployeeReference);
            Assert.AreEqual(null, result.JobTitle);
            Assert.AreEqual(null, result.SiteId);
            Assert.AreEqual(null, result.SiteGroupId);
            //Todo: department
            //Todo: manager
            Assert.AreEqual(_roles[0].Id.ToString(), result.Roles.Skip(1).Take(1).First().value);
            Assert.AreEqual(_roles[0].Description, result.Roles.Skip(1).Take(1).First().label);
            Assert.AreEqual(_roles[1].Id.ToString(), result.Roles.Last().value);
            Assert.AreEqual(_roles[1].Description, result.Roles.Last().label);
            Assert.AreEqual(_sites[0].Id.ToString(), result.Sites.Skip(1).Take(1).First().value);
            Assert.AreEqual(_sites[0].Name, result.Sites.Skip(1).Take(1).First().label);
            Assert.AreEqual(_sites[1].Id.ToString(), result.Sites.Last().value);
            Assert.AreEqual(_sites[1].Name, result.Sites.Last().label);
            Assert.AreEqual(_siteGroups[0].Id.ToString(), result.SiteGroups.Skip(1).Take(1).First().value);
            Assert.AreEqual(_siteGroups[0].Name, result.SiteGroups.Skip(1).Take(1).First().label);
            Assert.AreEqual(_siteGroups[1].Id.ToString(), result.SiteGroups.Last().value);
            Assert.AreEqual(_siteGroups[1].Name, result.SiteGroups.Last().label);
            //Todo: selected site group.
            Assert.IsFalse(result.SaveCancelButtonsVisible);
            Assert.IsFalse(result.SaveSuccessNotificationVisible);
            
        }
        
        [Test]
        public void Given_currentUser_is_same_as_seletced_user_and_current_user_is_admin_When_GetViewModel_is_called_Then_CanChangeRoleDdl_is_false()
        {
            var userId = Guid.NewGuid();
            var employeeId = Guid.NewGuid();
            var customPrinciple = new Mock<ICustomPrincipal>();

            customPrinciple
                .Setup(x => x.UserId)
                .Returns(userId);

            var employee = new EmployeeDto
                                {
                                    Id = employeeId,
                                    EmployeeReference = "EMP007",
                                    FullName = "Rodger Red",
                                    JobTitle = "Job Title 07",
                                    User = new UserDto
                                               {
                                                   Id = userId,
                                                   Role = new RoleDto
                                                              {
                                                                  Id = Guid.NewGuid(),
                                                                  Description = "User Admin"
                                                              }
                                               }
                                };

            _employeeService.Setup(x => x.GetEmployee(employeeId, _companyId)).Returns(employee);
            var target = CreateUserPermissionsViewModelFactory();
            var result = target.WithCurrentUser(customPrinciple.Object).GetViewModel(_companyId, employeeId, null, true);
            Assert.That(result.CanChangeRoleDdl, Is.False);
        }

        [Test]
        public void Given_currentUser_is_same_as_seletced_user_and_current_user_is_not_admin_When_GetViewModel_is_called_Then_CanChangeRoleDdl_is_false()
        {
            var userId = Guid.NewGuid();
            var employeeId = Guid.NewGuid();
            var customPrinciple = new Mock<ICustomPrincipal>();

            customPrinciple
                .Setup(x => x.UserId)
                .Returns(userId);

            var employee = new EmployeeDto
            {
                Id = employeeId,
                EmployeeReference = "EMP007",
                FullName = "Rodger Red",
                JobTitle = "Job Title 07",
                User = new UserDto
                {
                    Id = userId,
                    Role = new RoleDto
                    {
                        Id = Guid.NewGuid(),
                        Description = "General User"
                    }
                }
            };

            _employeeService.Setup(x => x.GetEmployee(employeeId, _companyId)).Returns(employee);
            var target = CreateUserPermissionsViewModelFactory();
            var result = target.WithCurrentUser(customPrinciple.Object).GetViewModel(_companyId, employeeId, null, true);
            Assert.That(result.CanChangeRoleDdl, Is.True);
        }

        [Test]
        public void Given_currentUser_is_not_same_as_seletced_user_and_current_user_is_admin_When_GetViewModel_is_called_Then_CanChangeRoleDdl_is_false()
        {
            var userId = Guid.NewGuid();
            var employeeId = Guid.NewGuid();
            var customPrinciple = new Mock<ICustomPrincipal>();

            customPrinciple
                .Setup(x => x.UserId)
                .Returns(Guid.NewGuid());

            var employee = new EmployeeDto
            {
                Id = employeeId,
                EmployeeReference = "EMP007",
                FullName = "Rodger Red",
                JobTitle = "Job Title 07",
                User = new UserDto
                {
                    Id = userId,
                    Role = new RoleDto
                    {
                        Id = Guid.NewGuid(),
                        Description = "User Admin"
                    }
                }
            };

            _employeeService.Setup(x => x.GetEmployee(employeeId, _companyId)).Returns(employee);
            var target = CreateUserPermissionsViewModelFactory();
            var result = target.WithCurrentUser(customPrinciple.Object).GetViewModel(_companyId, employeeId, null, true);
            Assert.That(result.CanChangeRoleDdl, Is.True);
        }
        private AddUsersViewModelFactory CreateUserPermissionsViewModelFactory()
        {
            return new AddUsersViewModelFactory(
                _employeeService.Object,
                _roleService.Object,
                _siteService.Object,
                _siteGroupService.Object
                );
        }
    }
}
