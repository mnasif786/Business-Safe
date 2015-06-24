using System;
using BusinessSafe.Application.Implementations.Employees;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.ParameterClasses;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;
using StructureMap;

namespace BusinessSafe.Application.Tests.Implementations.EmployeeServicesTests
{
    [TestFixture]
    public class CreateUserTests
    {
        private Mock<IEmployeeRepository> _employeeRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        private Mock<IRegisterEmployeeAsUserParametersMapper> _registerEmployeeAsUserParametersMapper;
        private Mock<IPeninsulaLog> _log;

        [SetUp]
        public void SetUp()
        {
            _employeeRepository = new Mock<IEmployeeRepository>();
            _userRepository = new Mock<IUserForAuditingRepository>();
            _registerEmployeeAsUserParametersMapper = new Mock<IRegisterEmployeeAsUserParametersMapper>();
            _log = new Mock<IPeninsulaLog>();

            ObjectFactory.Inject<IPeninsulaLog>(new StubPeninsulaLog());
        }

        [Test]
        public void Given_valid_request_When_register_employee_as_user_called_Then_should_call_correct_methods()
        {
            // Given
            var request = new CreateEmployeeAsUserRequest
                              {
                                  CompanyId = 1,
                                  EmployeeId = Guid.NewGuid(),
                                  NewUserId = Guid.NewGuid(),
                                  RoleId = Guid.NewGuid(),
                                  SiteId = 66L,
                                  ActioningUserId = Guid.NewGuid()
                              };

            var target = CreateEmployeeService();

            _employeeRepository
                .Setup(x => x.GetByIdAndCompanyId(request.EmployeeId, request.CompanyId))
                .Returns(new Employee {Id = request.EmployeeId, CompanyId = request.CompanyId});

            _registerEmployeeAsUserParametersMapper
                .Setup(x => x.Map(request))
                .Returns(new RegisterEmployeeAsUserParameters
                             {
                                 NewUserId = request.NewUserId,
                                 Role = new Role {Id = request.RoleId},
                                 Site = new Site {Id = request.SiteId, ClientId = request.CompanyId},
                                 ActioningUser = new UserForAuditing {Id = request.ActioningUserId, CompanyId = request.CompanyId},
                                 CompanyId = request.CompanyId
                             });
            // When
            target.CreateUser(request);

            // Then
            _employeeRepository.Verify(x => x.SaveOrUpdate(It.Is<Employee>(
                y => y.Id == request.EmployeeId
                && y.CompanyId == request.CompanyId
                && y.LastModifiedBy.Id == request.ActioningUserId
                && y.LastModifiedOn.HasValue
                && y.User != null
                && y.User.Id == request.NewUserId
                && y.User.CompanyId == request.CompanyId
                && y.User.Site != null
                && y.User.Site.Id == request.SiteId
                && y.User.Role != null
                && y.User.Role.Id == request.RoleId
                && y.User.CreatedBy != null
                && y.User.CreatedBy.Id == request.ActioningUserId
                && y.User.CreatedOn.HasValue 
                )));
        }

        [Test]
        public void Given_valid_request_When_CreateUser_Then_User_IsRegistered_equals_false()
        {
            // Given
            var request = new CreateEmployeeAsUserRequest
                              {
                                  CompanyId = 1,
                                  EmployeeId = Guid.NewGuid(),
                                  NewUserId = Guid.NewGuid(),
                                  RoleId = Guid.NewGuid(),
                                  SiteId = 66L,
                                  ActioningUserId = Guid.NewGuid()
                              };

            var employee = new Employee {Id = request.EmployeeId, CompanyId = request.CompanyId};
            
            _employeeRepository
                .Setup(x => x.GetByIdAndCompanyId(request.EmployeeId, request.CompanyId))
                .Returns(() => employee);

            _registerEmployeeAsUserParametersMapper
                .Setup(x => x.Map(request))
                .Returns(new RegisterEmployeeAsUserParameters
                {
                    NewUserId = request.NewUserId,
                    Role = new Role { Id = request.RoleId },
                    Site = new Site { Id = request.SiteId, ClientId = request.CompanyId },
                    ActioningUser = new UserForAuditing { Id = request.ActioningUserId, CompanyId = request.CompanyId },
                    CompanyId = request.CompanyId
                });

            var target = CreateEmployeeService();

            // When
            target.CreateUser(request);

            Assert.IsFalse(employee.User.IsRegistered.Value);

        }

        [Test]
        [ExpectedException(typeof(AttemptingToCreateEmployeeAsUserWhenUserExistsException))]
        public void Given_a_user_exists_for_the_employee_When_CreateUser_Then_exception_is_thrown()
        {
            // Given
            var request = new CreateEmployeeAsUserRequest
            {
                CompanyId = 1,
                EmployeeId = Guid.NewGuid(),
                NewUserId = Guid.NewGuid(),
                RoleId = Guid.NewGuid(),
                SiteId = 66L,
                ActioningUserId = Guid.NewGuid()
            };

            var employee = new Employee { Id = request.EmployeeId, CompanyId = request.CompanyId };
            employee.User = new User();

            _employeeRepository
                .Setup(x => x.GetByIdAndCompanyId(request.EmployeeId, request.CompanyId))
                .Returns(() => employee);

            _registerEmployeeAsUserParametersMapper
                .Setup(x => x.Map(request))
                .Returns(new RegisterEmployeeAsUserParameters
                {
                    NewUserId = request.NewUserId,
                    Role = new Role { Id = request.RoleId },
                    Site = new Site { Id = request.SiteId, ClientId = request.CompanyId },
                    ActioningUser = new UserForAuditing { Id = request.ActioningUserId, CompanyId = request.CompanyId },
                    CompanyId = request.CompanyId
                });

            var target = CreateEmployeeService();

            // When
            target.CreateUser(request);

        }

        private EmployeeService CreateEmployeeService()
        {
            return new EmployeeService(_employeeRepository.Object, null, null, _userRepository.Object, _registerEmployeeAsUserParametersMapper.Object, null, _log.Object, null, null, null);
        }
    }
}