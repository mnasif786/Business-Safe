using System;
using BusinessSafe.Application.Implementations.Employees;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.EmployeeServicesTests
{
    [TestFixture]
    public class ReinstateEmployeeAsNotDeletedTests
    {
        private Mock<IEmployeeRepository> _employeeRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        private Mock<IPeninsulaLog> _log;

        [SetUp]
        public void SetUp()
        {
            _employeeRepository = new Mock<IEmployeeRepository>();
            _userRepository = new Mock<IUserForAuditingRepository>();
            _log = new Mock<IPeninsulaLog>();

         
        }

        [Test]
        public void Given_valid_request_When_reinstate_employee_as_not_deleted_Then_should_call_correct_methods()
        {

            // Given
            var request = new ReinstateEmployeeAsNotDeleteRequest()
                              {
                                  CompanyId = 1,
                                  EmployeeId = Guid.NewGuid(),
                                  UserId = Guid.NewGuid()
                              };

            var target = CreateEmployeeService();
            var employee = new Mock<Employee>();

            _employeeRepository
                .Setup(x => x.GetByIdAndCompanyId(request.EmployeeId, request.CompanyId))
                .Returns(employee.Object);

            _userRepository.Setup(x => x.GetByIdAndCompanyId(request.UserId, request.CompanyId)).Returns(new UserForAuditing() { Id = request.UserId });

            // When
            target.ReinstateEmployeeAsNotDeleted(request);

            // Then
            _employeeRepository.Verify(x => x.SaveOrUpdate(It.IsAny<Employee>()));
            employee.Verify(x => x.ReinstateEmployeeAsNotDeleted(It.Is<UserForAuditing>(y => y.Id == request.UserId)));
        }

        private EmployeeService CreateEmployeeService()
        {
            return new EmployeeService(_employeeRepository.Object, null, null, _userRepository.Object, null, null, _log.Object, null, null, null);
        }
    }
}