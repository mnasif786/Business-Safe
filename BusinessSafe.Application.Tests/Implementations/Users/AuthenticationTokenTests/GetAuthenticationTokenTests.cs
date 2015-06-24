using System;
using BusinessSafe.Application.Implementations.Users;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.Users.AuthenticationTokenTests
{
    [TestFixture]
    public class GetAuthenticationTokenTests
    {
        private Mock<IAuthenticationTokenRepository> _authTokenRepository;
        private Mock<IApplicationTokenRepository> _appTokenRepository;
        private Mock<IUserRepository> _userRepository;
        private AuthenticationTokenService _target;

        private Guid authenticationTokenId;
        private User user;

        [SetUp]
        public void Setup()
        {
            user = new User() { Role = new Role() };
            
            authenticationTokenId = Guid.NewGuid();

            _appTokenRepository = new Mock<IApplicationTokenRepository>();

            _authTokenRepository = new Mock<IAuthenticationTokenRepository>();
            _authTokenRepository
                .Setup(x => x.GetById(authenticationTokenId))
                .Returns(new AuthenticationToken()
                         {
                             Id = authenticationTokenId,
                             IsEnabled = true,
                             ApplicationToken = new ApplicationToken(),
                             ReasonForDeauthorisation = string.Empty,
                             User = user
                         });

            _userRepository = new Mock<IUserRepository>();

            _target = GetTarget();
        }

        [Test]
        public void Given_When_GetAuthenticationToken_Then_returns_authenticationToken()
        {
            // Given

            // When
            var result = _target.GetAuthenticationToken(authenticationTokenId);

            // Then
            Assert.That(result.Id, Is.EqualTo(authenticationTokenId));
            Assert.That(result.User.Id, Is.EqualTo(user.Id));
        }

        private AuthenticationTokenService GetTarget()
        {
            return new AuthenticationTokenService(_authTokenRepository.Object, _userRepository.Object, _appTokenRepository.Object);
        }
    }
}
