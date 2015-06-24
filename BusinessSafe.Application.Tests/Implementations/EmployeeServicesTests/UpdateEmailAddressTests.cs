using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public class UpdateEmailAddressTests
    {
        private Mock<IEmployeeRepository> _employeeRepository;
        private Mock<IUserForAuditingRepository> _userForAuditingRepository;
        private Mock<IPeninsulaLog> _log;

        [SetUp]
        public void Setup()
        {
            _employeeRepository = new Mock<IEmployeeRepository>();
            _userForAuditingRepository = new Mock<IUserForAuditingRepository>();
            _log=new Mock<IPeninsulaLog>();
        }

        [Test]
        public void When_UpdateEmailAddress_Then_call_correct_methods()
        {
            // Given
            var request = new UpdateEmployeeEmailAddressRequest
                                                         {
                                                             EmployeeId= Guid.NewGuid(),
                                                             Email= "test@hotmail.com",
                                                             CompanyId=888L,
                                                             CurrentUserId = Guid.NewGuid()
                                                         };

            var employee = new Mock<Employee>();

            _employeeRepository
                .Setup(x => x.GetByIdAndCompanyId(request.EmployeeId, request.CompanyId))
                .Returns(employee.Object);

            var _userForAudiitng=new UserForAuditing();

            _userForAuditingRepository
                .Setup((x => x.GetByIdAndCompanyId(request.CurrentUserId, request.CompanyId)))
                .Returns(_userForAudiitng);

            var target = GetTarget();

            // When
            target.UpdateEmailAddress(request);

            // Then
            _employeeRepository.Verify(x => x.SaveOrUpdate(employee.Object));
            _userForAuditingRepository.VerifyAll();
            employee.Verify(x => x.SetEmail(request.Email, _userForAudiitng));
        }

        private EmployeeService GetTarget()
        {
            return new EmployeeService(
                _employeeRepository.Object,
                null,
                null,
                _userForAuditingRepository.Object,
                null,
                null,
                _log.Object
                , null, null, null
                );
        }
    }
}
