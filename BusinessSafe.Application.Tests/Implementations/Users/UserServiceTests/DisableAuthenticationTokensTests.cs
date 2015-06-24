using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Implementations.Users;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.Users.UserServiceTests
{
    [TestFixture]
    public class DisableAuthenticationTokensTests
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

            var actioningUserId = Guid.NewGuid();
            var actioningUser = new UserForAuditing() { Id = actioningUserId };

            _userForAuditingRepo
                .Setup(x => x.GetById(actioningUserId))
                .Returns(actioningUser);
        }

        private UserService GetTarget()
        {
            var target = new UserService(
                _userForAuditingRepo.Object,
                null,
                null,
                null, _userRepo.Object, _log.Object,null, null);
            return target;
        }

        [Test]
        public void Given_user_exists_authentication_tokens_when_DisableAuthenticationTokens_Then_all_tokens_are_disabled()
        {
            //given
            var user = new User()
                           {
                               Id = Guid.NewGuid(),
                               AuthenticationTokens = new List<AuthenticationToken>() { new AuthenticationToken() { IsEnabled = true }, new AuthenticationToken() { IsEnabled = true } }
                           };

            _userRepo.Setup(x => x.GetById(user.Id)).Returns(() => user);

            var target = GetTarget();

            //when
            target.DisableAuthenticationTokens(user.Id, null);

            //then
            Assert.IsTrue(user.AuthenticationTokens.All(x=> x.IsEnabled == false));

        }

        [Test]
        public void Given_user_exists_authentication_tokens_when_DisableAuthenticationTokens_Then_user_saved()
        {
            //given
            var user = new User()
            {
                Id = Guid.NewGuid(),
                AuthenticationTokens = new List<AuthenticationToken>() { new AuthenticationToken() { IsEnabled = true }, new AuthenticationToken() { IsEnabled = true } }
            };

            _userRepo.Setup(x => x.GetById(user.Id)).Returns(() => user);

            var target = GetTarget();

            //when
            target.DisableAuthenticationTokens(user.Id, null);

            //then
            _userRepo.Verify(x=> x.Save(user),Times.Once());

        }

    }
}
