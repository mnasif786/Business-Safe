using System;

using BusinessSafe.Application.Implementations.Users;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.Users.AuthenticationTokenTests
{
    [TestFixture]
    public class CreateAuthenticationTokenTests
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

        [Test]
        public void Given_When_CreateAuthorisationToken_Then_returns_new_authorisationToken()
        {
            // Given

            // When
            var result = _target.CreateAuthenticationToken(userId, applicationTokenId);

            // Then
            Assert.That(result.Id, Is.Not.Null);
            Assert.That(result.IsEnabled, Is.True);
            Assert.That(string.IsNullOrEmpty(result.ReasonForDeauthorisation));
        }

        [Test]
        public void Given_When_CreateAuthorisationToken_Then_save_to_repo()
        {
            // Given
            AuthenticationToken passedAuthenticationToken = null;
            _authTokenRepository
                .Setup(x => x.Save(It.IsAny<AuthenticationToken>()))
                .Callback<AuthenticationToken>(y => passedAuthenticationToken = y);

            // When
            var result = _target.CreateAuthenticationToken(userId, applicationTokenId);

            // Then
            Assert.That(passedAuthenticationToken.LastAccessDate.Date, Is.EqualTo(DateTime.Today));
            Assert.That(passedAuthenticationToken.User.Id, Is.EqualTo(userId));
            Assert.That(passedAuthenticationToken.ApplicationToken.Id, Is.EqualTo(applicationTokenId));
        }

        [Test]
        public void Given_When_CreateAuthorisationToken_Then_return_token_info()
        {
            // Given
            AuthenticationToken passedAuthenticationToken = null;
            _authTokenRepository
                .Setup(x => x.Save(It.IsAny<AuthenticationToken>()))
                .Callback<AuthenticationToken>(y => passedAuthenticationToken = y);

            // When
            var result = _target.CreateAuthenticationToken(userId, applicationTokenId);

            // Then
            Assert.That(result.Id, Is.EqualTo(passedAuthenticationToken.Id));
            Assert.That(result.IsEnabled, Is.EqualTo(passedAuthenticationToken.IsEnabled));
            Assert.That(result.ReasonForDeauthorisation, Is.EqualTo(passedAuthenticationToken.ReasonForDeauthorisation));
        }

        private AuthenticationTokenService GetTarget()
        {
            return new AuthenticationTokenService(_authTokenRepository.Object, _userRepository.Object, _appTokenRepository.Object);
        }
    }
}
