using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Implementations.Users;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.Users.AuthenticationTokenTests
{
    class DisableAuthenticationTokenTests
    {
        private Mock<IAuthenticationTokenRepository> _authTokenRepository;
        private Mock<IApplicationTokenRepository> _appTokenRepository;
        private Mock<IUserRepository> _userRepository;
        private AuthenticationTokenService _target;

        private Guid applicationTokenId;
        private Guid userId;

        [SetUp]
        public void Setup()
        {
            applicationTokenId = Guid.NewGuid();
            userId = Guid.NewGuid();

            _authTokenRepository = new Mock<IAuthenticationTokenRepository>();
            _authTokenRepository
                .Setup(x => x.Save(It.IsAny<AuthenticationToken>()));

            _appTokenRepository = new Mock<IApplicationTokenRepository>();
            _appTokenRepository
                .Setup(x => x.GetById(applicationTokenId))
                .Returns(new ApplicationToken() { Id = applicationTokenId });

            _userRepository = new Mock<IUserRepository>();
            _userRepository
                .Setup(x => x.GetById(userId))
                .Returns(new User() { Id = userId, Role = new Role() });

            _target = GetTarget();
        }

        private AuthenticationTokenService GetTarget()
        {
            return new AuthenticationTokenService(_authTokenRepository.Object, _userRepository.Object, _appTokenRepository.Object);
        }

        [Test]
        public void Given_token_doesnt_exist_when_DisableAuthenticationToken_then_object_not_saved()
        {
        //given
            _authTokenRepository.Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(() => null);

            var target = GetTarget();

            //then
            target.DisableAuthenticationToken(Guid.NewGuid());

            //when
            _authTokenRepository.Verify(x=> x.Save(It.IsAny<AuthenticationToken>()), Times.Never());
            _authTokenRepository.Verify(x => x.SaveOrUpdate(It.IsAny<AuthenticationToken>()), Times.Never());

        }

        [Test]
        public void Given_token_doesnt_exist_when_DisableAuthenticationToken_then_token_saved()
        {
            //given
            _authTokenRepository.Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(() => new AuthenticationToken());

            var target = GetTarget();

            //then
            target.DisableAuthenticationToken(Guid.NewGuid());

            //when
            _authTokenRepository.Verify(x => x.Save(It.IsAny<AuthenticationToken>()), Times.Once());


        }

        [Test]
        public void Given_token_doesnt_exist_when_DisableAuthenticationToken_then_token_disabled()
        {
            //given
            var savedToken = new AuthenticationToken(){IsEnabled =  true};
            _authTokenRepository.Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(() => savedToken);

            var target = GetTarget();

            //then
            target.DisableAuthenticationToken(Guid.NewGuid());

            //when
            Assert.False(savedToken.IsEnabled = false);


        }
    }
}
