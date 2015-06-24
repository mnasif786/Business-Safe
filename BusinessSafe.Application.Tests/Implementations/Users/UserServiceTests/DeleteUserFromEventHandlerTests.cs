using System;
using System.Collections.Generic;
using System.Linq;

using BusinessSafe.Application.Implementations.Users;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.Users.UserServiceTests
{
    [TestFixture]
    public class DeleteUserFromEventHandlerTests
    {
        private Mock<IUserRepository> _userRepo;
        private Mock<IUserForAuditingRepository> _userForAuditingRepo;
        private Mock<IPeninsulaLog> _log;

        [SetUp]
        public void SetUp()
        {
            _userRepo = new Mock<IUserRepository>();
            _log = new Mock<IPeninsulaLog>();
            _userForAuditingRepo = new Mock<IUserForAuditingRepository>();
        }

        [Test]
        public void Given_valid_request_to_disable_user_Then_should_delete_user()
        {
            //Given
            var target = CreateUserService();

            var actioningUserId = Guid.NewGuid();
            var actioningUser = new UserForAuditing() { Id = actioningUserId };

            var userId = Guid.NewGuid();
            var userToDelete = new Mock<User>() { CallBase = true };
            userToDelete.Setup(x => x.Id).Returns(userId);
            userToDelete.Setup(x => x.AuthenticationTokens).Returns(new List<AuthenticationToken>());

            _userForAuditingRepo
                .Setup(x => x.GetById(actioningUserId))
                .Returns(actioningUser);

            _userRepo
                .Setup(x => x.GetById(userId))
                .Returns(userToDelete.Object);

            //When
            target.DeleteUser(userId, actioningUserId);

            //Then
            Assert.That(userToDelete.Object.Deleted, Is.True);
        }

        [Test]
        public void Given_valid_request_to_disable_user_Then_should_save_updated_user()
        {
            //Given
            var target = CreateUserService();
            var userId = Guid.NewGuid();
            var actioningUserId = Guid.NewGuid();
            var actioningUser = new UserForAuditing();
            var userToDelete = new Mock<User>();

            _userForAuditingRepo
                .Setup(x => x.GetById(actioningUserId))
                .Returns(actioningUser);

            _userRepo
                .Setup(x => x.GetById(userId))
                .Returns(userToDelete.Object);

            //When
            target.DeleteUser(userId, actioningUserId);

            //Then
            _userRepo.Verify(x => x.SaveOrUpdate(userToDelete.Object));
        }

        [Test]
        public void Given_valid_request_to_disable_user_Then_should_delete_associated_AuthenticationTokens()
        {
            //Given
            var target = CreateUserService();
            var userId = Guid.NewGuid();
            var actioningUserId = Guid.NewGuid();
            var actioningUser = new UserForAuditing();
            var userToDelete = new Mock<User>() { CallBase = true };
            userToDelete.Setup(x => x.Id).Returns(userId);
            userToDelete
                .Setup(x => x.AuthenticationTokens)
                .Returns(new List<AuthenticationToken>()
                         {
                             new AuthenticationToken() { IsEnabled = true },
                             new AuthenticationToken() { IsEnabled = true },
                             new AuthenticationToken() { IsEnabled = true }
                         });

            _userForAuditingRepo
                .Setup(x => x.GetById(actioningUserId))
                .Returns(actioningUser);

            _userRepo
                .Setup(x => x.GetById(userId))
                .Returns(userToDelete.Object);

            //When
            target.DeleteUser(userId, actioningUserId);

            //Then
            Assert.That(userToDelete.Object.AuthenticationTokens.Any(x => x.IsEnabled), Is.False);
        }

        private UserService CreateUserService()
        {
            var target = new UserService(
                _userForAuditingRepo.Object,
                null,
                null,
                null, _userRepo.Object, _log.Object, null, null);
            return target;
        }
    }
}