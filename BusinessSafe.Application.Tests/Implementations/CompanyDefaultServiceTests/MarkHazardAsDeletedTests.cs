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
    public class MarkHazardAsDeletedTests
    {
        private Mock<IHazardRepository> _repo;
        private Mock<IUserForAuditingRepository> _userRepository;
        private Mock<UserForAuditing> _user;
        private Mock<IPeninsulaLog> _log;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IHazardRepository>();
            _user = new Mock<UserForAuditing>();
            _userRepository = new Mock<IUserForAuditingRepository>();

            _log = new Mock<IPeninsulaLog>();
        }

        [Test]
        public void Given_that_request_is_valid_When_mark_hazard_as_deleted_Then_should_call_correct_methods()
        {
            //Given
            var target = CreateNonEmployeeervice();

            var hazard = new Mock<Hazard>();
            hazard.Setup(x => x.MarkForDelete(_user.Object));

           

            var request = new MarkCompanyDefaultAsDeletedRequest(1, 2, Guid.NewGuid());

            _userRepository
              .Setup(x => x.GetByIdAndCompanyId(request.UserId, request.CompanyId))
              .Returns(_user.Object);

            _repo
                .Setup(x => x.GetByIdAndCompanyId(request.CompanyDefaultId, request.CompanyId))
                .Returns(hazard.Object);


            //When
            target.MarkHazardAsDeleted(request);

            //Then
            hazard.VerifyAll();
            _repo.Verify(x => x.SaveOrUpdate(It.IsAny<Hazard>()));
        }


        private CompanyDefaultService CreateNonEmployeeervice()
        {
            var target = new CompanyDefaultService(_repo.Object, null, _userRepository.Object, null, null, null, null, null, null, null, _log.Object);
            return target;
        }
    }
}