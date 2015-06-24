using System;

using BusinessSafe.Application.Implementations.Users;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;

using Moq;
using NServiceBus;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.Users.UserServiceTests
{
    [TestFixture]
    public class EnableUserTests
    {
        private Mock<IUserRepository> _userRepo;
        private Mock<IUserForAuditingRepository> _userForAuditingRepo;
        private Mock<IPeninsulaLog> _log;
        private Mock<IBus> _bus;

        [SetUp]
        public void SetUp()
        {
            _userRepo = new Mock<IUserRepository>();
            _userForAuditingRepo = new Mock<IUserForAuditingRepository>();
            _log = new Mock<IPeninsulaLog>();
            _bus = new Mock<IBus>();
        }

        [Test]
        public void Given_valid_request_to_enable_user_Then_should_call_appropiate_methods()
        {
            //Given
            var target = CreateUSerService();
            var userId = Guid.NewGuid();
            var currentUserId = Guid.NewGuid();
            const int companyId = 100;
            var currentUser = new UserForAuditing();
            var user = new Mock<User>();
            user.Setup(x => x.Deleted).Returns(true);

            _userForAuditingRepo
                .Setup(x => x.GetByIdAndCompanyId(currentUserId, companyId))
                .Returns(currentUser);


            _userForAuditingRepo
                .Setup(x => x.GetById(currentUserId))
                .Returns(currentUser);


            _userRepo
                .Setup(x => x.GetByIdAndCompanyIdIncludeDeleted(userId, companyId))
                .Returns(user.Object);

            //When
            target.ReinstateUser(userId, companyId, currentUserId);

            //Then
            user.Verify(x => x.ReinstateFromDelete(currentUser));
            _userRepo.Verify(x => x.SaveOrUpdate(user.Object));
        }

        private UserService CreateUSerService()
        {
            var target = new UserService(
                _userForAuditingRepo.Object,
                null,
                null,
                null,
                _userRepo.Object, _log.Object, null, _bus.Object);
            return target;
        }
    }
}