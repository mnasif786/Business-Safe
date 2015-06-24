using System;
using System.Collections.Generic;
using BusinessSafe.Application.Implementations.Employees;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Application.Request;
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
    public class ValidateRegisterAsUserTests
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
        public void Given_valid_request_When_ValidateRegisterAsUser_called_Then_should_not_return_errors()
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
              .Returns(new Employee { Id = request.EmployeeId, CompanyId = request.CompanyId ,
                  ContactDetails = new List<EmployeeContactDetail>(){ new EmployeeContactDetail{Email="test@testing.com",Telephone1="01612381883"}}});

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

            //When
            var results = target.ValidateRegisterAsUser(request);

            //Then
            Assert.That(results.IsValid, Is.EqualTo(true));
        }

        [Test]
        public void Given_valid_request_When_ValidateRegisterAsUser_called_employee_dosent_have_phone_Then_should_not_return_errors()
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
              .Returns(new Employee
              {
                  Id = request.EmployeeId,
                  CompanyId = request.CompanyId
              });

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

            //When
            var results = target.ValidateRegisterAsUser(request);

            //Then
            Assert.That(results.IsValid, Is.EqualTo(false));
        }

        private EmployeeService CreateEmployeeService()
        {

            return new EmployeeService(_employeeRepository.Object, null, null, _userRepository.Object, _registerEmployeeAsUserParametersMapper.Object, null, _log.Object, null, null, null);
        }
    }
}
