using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Contracts.Users;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Response;
using BusinessSafe.WebSite.Areas.Employees.Controllers;
using BusinessSafe.WebSite.Areas.Employees.Factories;
using BusinessSafe.WebSite.PeninsulaOnline;
using BusinessSafe.WebSite.Tests.Builder;
using FluentValidation.Results;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.Employees.EmployeeControllerTests
{
    [TestFixture]
    [Category("Unit")]
    public class CreateUserFromEmployeeTests
    {

        private Mock<IEmployeeService> _employeeService;
        private Mock<INewRegistrationRequestService> _newRegistrationRequestService;
        private Mock<IEmployeeViewModelFactory> _employeeViewModelFactory;
        private Mock<IUserService> _userService;

        [SetUp]
        public void Setup()
        {
            _employeeService = new Mock<IEmployeeService>();
            _employeeService
                .Setup(x => x.CreateUser(It.IsAny<CreateEmployeeAsUserRequest>()));

            _employeeViewModelFactory = new Mock<IEmployeeViewModelFactory>();
            _employeeViewModelFactory
                .Setup(x => x.WithEmployeeId(It.IsAny<Guid?>()))
                .Returns(_employeeViewModelFactory.Object);

            _employeeViewModelFactory
                .Setup(x => x.WithCompanyId(It.IsAny<long>()))
                .Returns(_employeeViewModelFactory.Object);

            _employeeService
                .Setup(x => x.ValidateRegisterAsUser(It.IsAny<CreateEmployeeAsUserRequest>()))
                .Returns(new ValidationResult());

            _newRegistrationRequestService = new Mock<INewRegistrationRequestService>();
            _newRegistrationRequestService
                .Setup(x => x.HasEmailBeenRegistered(It.IsAny<string>()))
                .Returns(false);

            _employeeService
                .Setup(x => x.Update(It.IsAny<UpdateEmployeeRequest>()))
                .Returns(new UpdateEmployeeResponse()
                {
                    Success = true
                });

            _userService = new Mock<IUserService>();
        }

        [Test]
        public void Given_Valid_employee_details_When_Create_is_called_Then_EmployeeService_CreateUser_is_Called()
        {
            // Given
            var controller = CreateEmployeeController();

            Guid employeeId = Guid.NewGuid();
            Guid userRoleId = Guid.NewGuid();
            long userSiteId = 1234;

            var request = EmployeeViewModelBuilder
                                    .Create()
                                    .WithEmployeeId(employeeId)
                                    .WithTitle("Mr")
                                    .WithForename("Fred")
                                    .WithSurname("Flintstone") 
                                    .WithSex("Male")                                  
                                    .WithUserRoleId(userRoleId)
                                    .WithUserSiteId(userSiteId)
                                    .Build();

            var  employeeDto = new EmployeeDto
            {
                Id = employeeId,
                MainContactDetails = new EmployeeContactDetailDto
                {
                    Email = "testing@test.com",
                    Telephone2 = "333444555"
                }
            };

            _employeeService
                .Setup(x => x.GetEmployee(It.IsAny<Guid>(), It.IsAny<long>()))
                .Returns(employeeDto);
                        

            var response = new AddEmployeeResponse()
            {
                EmployeeId = Guid.NewGuid(),
                Success = true
            };

            _employeeService
                .Setup(x => x.Add(It.IsAny<AddEmployeeRequest>()))
                .Returns(response);

            // When
            ActionResult result = controller.Update(request);

            // Then
            _employeeService.Verify( x => x.CreateUser( It.Is<CreateEmployeeAsUserRequest>(
                    y =>    y.RoleId == userRoleId
                        &&  y.SiteId == userSiteId
                        &&  y.EmployeeId == employeeId
                )));             
        }

        [Test]
        public void Given_employee_and_new_user_When_Create_is_called_Then_user_is_created()
        {
            // Given
            var controller = CreateEmployeeController();

            Guid employeeId = Guid.NewGuid();
            Guid userRoleId = Guid.NewGuid();
            long userSiteId = 1234;


            var updateEmployeeRequest = EmployeeViewModelBuilder
                                    .Create()
                                    .WithEmployeeId(employeeId)
                                    .WithTitle("Mr")
                                    .WithForename("Fred")
                                    .WithSurname("Flintstone")
                                    .WithSex("Male")
                                    .WithUserRoleId(userRoleId)
                                    .WithUserSiteId(userSiteId)
                                    .Build();

            updateEmployeeRequest.CompanyId = 1;

            var employeeDto = new EmployeeDto
            {
                Id = employeeId,
                MainContactDetails = new EmployeeContactDetailDto
                {
                    Email = "testing@test.com",
                    Telephone2 = "333444555"
                }
            };

            _employeeService
                .Setup(x => x.GetEmployee(It.IsAny<Guid>(), It.IsAny<long>()))
                .Returns(employeeDto);

            _employeeService
                .Setup(x => x.Update(It.IsAny<UpdateEmployeeRequest>()))
                .Returns(() => new UpdateEmployeeResponse() {Success = true});
            
            CreateEmployeeAsUserRequest request = null;

            _employeeService
                .Setup(x => x.CreateUser(It.IsAny<CreateEmployeeAsUserRequest>()))
                .Callback<CreateEmployeeAsUserRequest>(parameter => request = parameter);
            
            // When
            var result = controller.Update(updateEmployeeRequest);

            // Then
            Assert.That(request.RoleId, Is.EqualTo(userRoleId));
            Assert.That(request.SiteId, Is.EqualTo(userSiteId));
            Assert.That(request.EmployeeId, Is.EqualTo(employeeId));
            Assert.That(request.CompanyId, Is.EqualTo(controller.CurrentUser.CompanyId));
        }

        [Test]
        public void Given_employee_and_updating_user_When_Create_is_called_Then_site_and_role_updated()
        {
            // Given
            var controller = CreateEmployeeController();

            Guid employeeId = Guid.NewGuid();
            Guid userRoleId = Guid.NewGuid();
            long userSiteId = 1234;
            var userToUpdate = Guid.NewGuid();
            
            var updateEmployeeRequest = EmployeeViewModelBuilder
                                    .Create()
                                    .WithEmployeeId(employeeId)
                                    .WithTitle("Mr")
                                    .WithForename("Fred")
                                    .WithSurname("Flintstone")
                                    .WithSex("Male")
                                    .WithUserRoleId(userRoleId)
                                    .WithUserSiteId(userSiteId)
                                    .Build();

            var employeeDto = new EmployeeDto
            {
                Id = employeeId,
                MainContactDetails = new EmployeeContactDetailDto
                {
                    Email = "testing@test.com",
                    Telephone2 = "333444555"
                },
                User = new UserDto() { Id = userToUpdate, Role = new RoleDto() { Id = userRoleId}, SiteStructureElement = new SiteDto() { Id = userSiteId}}
            };

            _employeeService
                .Setup(x => x.GetEmployee(It.IsAny<Guid>(), It.IsAny<long>()))
                .Returns(employeeDto);

            _employeeService
                .Setup(x => x.Update(It.IsAny<UpdateEmployeeRequest>()))
                .Returns(() => new UpdateEmployeeResponse() { Success = true });

            SetUserRoleAndSiteRequest request = null;

            _userService
                .Setup(x => x.SetRoleAndSite(It.IsAny<SetUserRoleAndSiteRequest>()))
                .Callback<SetUserRoleAndSiteRequest>(parameter => request = parameter);

            // When
            var result = controller.Update(updateEmployeeRequest);

            // Then
            _employeeService.Verify(x => x.CreateUser(It.IsAny<CreateEmployeeAsUserRequest>()), Times.Never());
            _userService.Verify(x => x.SetRoleAndSite(It.IsAny<SetUserRoleAndSiteRequest>()), Times.Once());

            Assert.That(request.RoleId, Is.EqualTo(userRoleId));
            Assert.That(request.UserToUpdateId, Is.EqualTo(userToUpdate));

            Assert.That(request.SiteId, Is.EqualTo(userSiteId));
           
           // Assert.That(request.ActioningUserId, Is.EqualTo(employeeId));
            Assert.That(request.CompanyId, Is.EqualTo(controller.CurrentUser.CompanyId));
        }
        
        [Test]
        public void Given_user_has_been_deleted_when_create_user_request_then_assert_reinstate_command_called()
        {
            // Given
            var controller = CreateEmployeeController();

            Guid employeeId = Guid.NewGuid();
            Guid userRoleId = Guid.NewGuid();
            long userSiteId = 1234;
            var userToUpdate = Guid.NewGuid();

            var updateEmployeeRequest = EmployeeViewModelBuilder
                                    .Create()
                                    .WithEmployeeId(employeeId)
                                    .WithTitle("Mr")
                                    .WithForename("Fred")
                                    .WithSurname("Flintstone")
                                    .WithSex("Male")
                                    .WithUserRoleId(userRoleId)
                                    .WithUserSiteId(userSiteId)
                                    .Build();

            var employeeDto = new EmployeeDto
            {
                Id = employeeId,
                MainContactDetails = new EmployeeContactDetailDto
                {
                    Email = "testing@test.com",
                    Telephone2 = "333444555"
                },
                User = new UserDto() { Id = userToUpdate, Role = new RoleDto() { Id = userRoleId }, SiteStructureElement = new SiteDto() { Id = userSiteId }, Deleted = true }
            };
            
            _userService
                    .Setup(x => x.GetByIdAndCompanyIdIncludeDeleted(It.IsAny<Guid>(), It.IsAny<long>()))
                    .Returns(new UserDto() { Employee = new EmployeeDto() { MainContactDetails = new EmployeeContactDetailDto() { Email = "Email" }}});

            _newRegistrationRequestService
                .Setup(x => x.HasEmailBeenRegistered(It.IsAny<string>()))
                .Returns(false);

            _employeeService
                .Setup(x => x.GetEmployee(It.IsAny<Guid>(), It.IsAny<long>()))
                .Returns(employeeDto);
            
            _employeeService
                .Setup(x => x.Update(It.IsAny<UpdateEmployeeRequest>()))
                .Returns(() => new UpdateEmployeeResponse() { Success = true });
          
            var result = controller.Update(updateEmployeeRequest);

            // Then
            _userService.Verify(x => x.ReinstateUser(It.IsAny<Guid>(), It.IsAny<long>(), It.IsAny<Guid>()), Times.Once());
        }

        [Test]
        public void Given_user_has_been_deleted_when_create_user_request_and_email_already_registered_then_assert_reinstate_command_not_called()
        {
            // Given
            var controller = CreateEmployeeController();

            Guid employeeId = Guid.NewGuid();
            Guid userRoleId = Guid.NewGuid();
            long userSiteId = 1234;
            var userToUpdate = Guid.NewGuid();

            var updateEmployeeRequest = EmployeeViewModelBuilder
                                    .Create()
                                    .WithEmployeeId(employeeId)
                                    .WithTitle("Mr")
                                    .WithForename("Fred")
                                    .WithSurname("Flintstone")
                                    .WithSex("Male")
                                    .WithUserRoleId(userRoleId)
                                    .WithUserSiteId(userSiteId)
                                    .Build();

            var employeeDto = new EmployeeDto
            {
                Id = employeeId,
                MainContactDetails = new EmployeeContactDetailDto
                {
                    Email = "testing@test.com",
                    Telephone2 = "333444555"
                },
                User = new UserDto() { Id = userToUpdate, Role = new RoleDto() { Id = userRoleId }, SiteStructureElement = new SiteDto() { Id = userSiteId }, Deleted = true }
            };

            _userService
                    .Setup(x => x.GetByIdAndCompanyIdIncludeDeleted(It.IsAny<Guid>(), It.IsAny<long>()))
                    .Returns(new UserDto() { Employee = new EmployeeDto() { MainContactDetails = new EmployeeContactDetailDto() { Email = "Email" } } });

            _newRegistrationRequestService
                .Setup(x => x.HasEmailBeenRegistered(It.IsAny<string>()))
                .Returns(true);

            _employeeService
                .Setup(x => x.GetEmployee(It.IsAny<Guid>(), It.IsAny<long>()))
                .Returns(employeeDto);

            _employeeService
                .Setup(x => x.Update(It.IsAny<UpdateEmployeeRequest>()))
                .Returns(() => new UpdateEmployeeResponse() { Success = true });

            var result = controller.Update(updateEmployeeRequest);

            // Then
            _userService.Verify(x => x.ReinstateUser(It.IsAny<Guid>(), It.IsAny<long>(), It.IsAny<Guid>()), Times.Never());
        }


        private EmployeeController CreateEmployeeController()
        {
            var result = new EmployeeController(_employeeService.Object, _employeeViewModelFactory.Object, _newRegistrationRequestService.Object, _userService.Object);
            return TestControllerHelpers.AddUserToController(result);
        }
    }
}
