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
    public class DeleteUserInOwnCompanyTests
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
        public void Given_valid_request_to_disable_user_Then_should_call_appropiate_methods()
        {
            //Given
            var target = CreateUserService();
            var userId = Guid.NewGuid();
            var currentUserId = Guid.NewGuid();
            const int companyId = 100;
            var currentUser = new UserForAuditing();
            var user = new Mock<User>();

            _userForAuditingRepo
                .Setup(x => x.GetByIdAndCompanyId(currentUserId, companyId))
                .Returns(currentUser);

            _userRepo
                .Setup(x => x.GetByIdAndCompanyId(userId, companyId))
                .Returns(user.Object);

            //When
            target.DeleteUser(userId, companyId, currentUserId);

            //Then
            user.Verify(x => x.Delete(currentUser));
            _userRepo.Verify(x => x.SaveOrUpdate(user.Object));
        }

        [Test]
        public void Given_valid_request_to_disable_user_Then_should_delete_associated_AuthenticationTokens()
        {
            //Given
            const int companyId = 100;
            var target = CreateUserService();

            var actioningUserId = Guid.NewGuid();
            var actioningUser = new UserForAuditing();

            var userToDeleteId = Guid.NewGuid();
            var userToDelete = new Mock<User>() { CallBase = true };

            userToDelete.Setup(x => x.Id).Returns(userToDeleteId);
            userToDelete
                .Setup(x => x.AuthenticationTokens)
                .Returns(new List<AuthenticationToken>()
                         {
                             new AuthenticationToken() { IsEnabled = true },
                             new AuthenticationToken() { IsEnabled = true },
                             new AuthenticationToken() { IsEnabled = true }
                         });

            _userForAuditingRepo
                .Setup(x => x.GetByIdAndCompanyId(actioningUserId, companyId))
                .Returns(actioningUser);

            _userRepo
                .Setup(x => x.GetByIdAndCompanyId(userToDeleteId, companyId))
                .Returns(userToDelete.Object);

            //When
            target.DeleteUser(userToDeleteId, companyId, actioningUserId);

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