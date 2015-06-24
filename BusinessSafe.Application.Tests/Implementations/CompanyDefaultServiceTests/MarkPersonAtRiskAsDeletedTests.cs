using System;
using BusinessSafe.Application.Implementations.Company;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.CompanyDefaultServiceTests
{
    [TestFixture]
    [Category("Unit")]
    public class MarkPersonAtRiskAsDeletedTests
    {
        private Mock<IPeopleAtRiskRepository> _repo;
        private Mock<IUserForAuditingRepository> _userRepository;
        private Mock<IPeninsulaLog> _log;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IPeopleAtRiskRepository>();
            _userRepository = new Mock<IUserForAuditingRepository>();
           

            _log = new Mock<IPeninsulaLog>();
        }

        [Test]
        public void Given_that_request_is_valid_When_mark_person_at_risk_as_deleted_Then_should_call_correct_methods()
        {
            //Given
            var request = new MarkCompanyDefaultAsDeletedRequest(1, 2, Guid.NewGuid());

            var target = CreateNonEmployeeervice();

            var user = new UserForAuditing();
            _userRepository
                .Setup(x => x.GetByIdAndCompanyId(request.UserId, request.CompanyId))
                .Returns(user);

            var person = new Mock<PeopleAtRisk>();
            person.Setup(x => x.MarkForDelete(user));
            
            _repo
                .Setup(x => x.GetByIdAndCompanyId(request.CompanyDefaultId, request.CompanyId))
                .Returns(person.Object);

            //When
            target.MarkPersonAtRiskAsDeleted(request);

            //Then
            person.VerifyAll();
            _repo.Verify(x => x.SaveOrUpdate(It.IsAny<PeopleAtRisk>()));
        }

        private CompanyDefaultService CreateNonEmployeeervice()
        {
            var target = new CompanyDefaultService(null, _repo.Object, _userRepository.Object, null, null, null, null, null, null, null, _log.Object);
            return target;
        }
    }
}