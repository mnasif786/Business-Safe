using System;

using BusinessSafe.Application.Implementations.Responsibilities;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.ResponsibilitiesServiceTests
{
    [TestFixture]
    public class UndeleteTests
    {
        private Mock<IResponsibilityRepository> _responsibilityRepo;
        private Mock<IUserForAuditingRepository> _userForAuditingRepo;
        private Mock<IPeninsulaLog> _log;
        private Mock<Responsibility> _responsibility;

        private const long _responsibilityId = 1234L;
        private const long _companyId = 5678L;

        private Guid _actioningUserId;
        private UserForAuditing _actioningUser;

        private ResponsibilitiesService _target;

        [SetUp]
        public void Setup()
        {
            _actioningUserId = Guid.NewGuid();
            _actioningUser = new UserForAuditing() { Id = _actioningUserId };

            _userForAuditingRepo = new Mock<IUserForAuditingRepository>();
            _userForAuditingRepo
                .Setup(x => x.GetByIdAndCompanyId(_actioningUserId, _companyId))
                .Returns(_actioningUser);

            _responsibilityRepo = new Mock<IResponsibilityRepository>();
            _responsibility = new Mock<Responsibility>();

            _responsibility
                .Setup(x => x.ReinstateFromDelete(_actioningUser));

            _responsibilityRepo
                .Setup(x => x.GetByIdAndCompanyId(_responsibilityId, _companyId))
                .Returns(_responsibility.Object);

            _log = new Mock<IPeninsulaLog>();

        }

        [Test]
        public void When_Undelete_Then_get_responsibility_from_repo()
        {
            // Given
            _target = GetTarget();

            // When
            _target.Undelete(_responsibilityId, _companyId, _actioningUserId);

            // Then
            _responsibilityRepo.Verify(x => x.GetByIdAndCompanyId(_responsibilityId, _companyId));
        }

        [Test]
        public void Given_responsibility_found_When_Undelete_Then_tell_it_to_Undelete_itself()
        {
            // Given
            _target = GetTarget();

            // When
            _target.Undelete(_responsibilityId, _companyId, _actioningUserId);

            // Then
            _responsibility.Verify(x => x.ReinstateFromDelete(_actioningUser));
        }

        [Test]
        public void Given_responsibility_not_found_When_Undelete_Then_throw_exception()
        {
            // Given
            _responsibilityRepo
                .Setup(x => x.GetByIdAndCompanyId(_responsibilityId, _companyId));
            _target = GetTarget();

            // When

            // Then
            var e = Assert.Throws<ResponsibilityNotFoundException>(() => _target.Undelete(_responsibilityId, _companyId, _actioningUserId));
            _log.Verify(x => x.Add(e));
        }

        private ResponsibilitiesService GetTarget()
        {
            return new ResponsibilitiesService(_responsibilityRepo.Object, null, null, null, null, _userForAuditingRepo.Object, null, null, null, null, _log.Object);
        }
    }
}
