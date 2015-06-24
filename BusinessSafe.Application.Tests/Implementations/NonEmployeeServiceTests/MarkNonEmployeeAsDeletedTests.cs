using System;
using BusinessSafe.Application.Implementations;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;
using StructureMap;

namespace BusinessSafe.Application.Tests.Implementations.NonEmployeeServiceTests
{
    [TestFixture]
    [Category("Unit")]
    public class MarkNonEmployeeAsDeletedTests
    {
        private Mock<INonEmployeeRepository> _nonEmployeeRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        private Mock<IPeninsulaLog> _log;
        private UserForAuditing _user;
        private const long NonEmployeeId = 21;
        private const long CompanyId = 200;

        [SetUp]
        public void SetUp()
        {
            _nonEmployeeRepository = new Mock<INonEmployeeRepository>();
            _userRepository = new Mock<IUserForAuditingRepository>();
            _user = new UserForAuditing();
            _log = new Mock<IPeninsulaLog>();
        }

        [Test]
        public void Given_that_request_is_valid_When_mark_non_employee_as_deleted_Then_should_call_correct_methods()
        {
            //Given
            var target = CreateNonEmployeeervice();

            var nonEmployee = new Mock<NonEmployee>();
            nonEmployee.Setup(x => x.MarkForDelete(_user));

            var request = new MarkNonEmployeeAsDeletedRequest(NonEmployeeId, CompanyId, Guid.NewGuid());
            _nonEmployeeRepository
                .Setup(x => x.GetByIdAndCompanyId(request.NonEmployeeId, request.CompanyId))
                .Returns(nonEmployee.Object);

            _userRepository
                .Setup(x => x.GetByIdAndCompanyId(request.UserId, request.CompanyId))
                .Returns(_user);

            //When
            target.MarkNonEmployeeAsDeleted(request);

            //Then
            nonEmployee.VerifyAll();
            _nonEmployeeRepository.Verify(x => x.SaveOrUpdate(It.IsAny<NonEmployee>()));
        }

      
        private NonEmployeeService CreateNonEmployeeervice()
        {
            var target = new NonEmployeeService(_nonEmployeeRepository.Object, _userRepository.Object, _log.Object);
            return target;
        }
    }
}