
using System;

using BusinessSafe.Domain.Entities;

using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.AuthorisationTokenTests
{
    [TestFixture]
    public class Create
    {
        User user;
        ApplicationToken applicationToken;
        [SetUp]
        public void Setup()
        {
            user = new User();
            applicationToken = new ApplicationToken();
        }

        [Test]
        public void Given_When_Create_Then_return_new_token()
        {
            // Given

            // When
            var result = AuthenticationToken.Create(user, applicationToken);

            // Then
            Assert.That(result, Is.InstanceOf<AuthenticationToken>());
        }

        [Test]
        public void Given_When_Create_Then_set_userId()
        {
            // Given

            // When
            var result = AuthenticationToken.Create(user, applicationToken);

            // Then
            Assert.That(result.User, Is.EqualTo(user));
        }

        [Test]
        public void Given_When_Create_Then_set_applicationToken()
        {
            // Given

            // When
            var result = AuthenticationToken.Create(user, applicationToken);

            // Then
            Assert.That(result.ApplicationToken, Is.EqualTo(applicationToken));
        }

        [Test]
        public void Given_When_Create_Then_set_createdon_date()
        {
            // Given

            // When
            var result = AuthenticationToken.Create(user, applicationToken);

            // Then
            Assert.That(result.CreatedOn.Value.Date, Is.EqualTo(DateTime.Today));
        }

        [Test]
        public void Given_When_Create_Then_set_isEnabled()
        {
            // Given

            // When
            var result = AuthenticationToken.Create(user, applicationToken);

            // Then
            Assert.IsTrue(result.IsEnabled);
        }

        [Test]
        public void Given_When_Create_Then_set_lastAccessDate()
        {
            // Given

            // When
            var result = AuthenticationToken.Create(user, applicationToken);

            // Then
            Assert.That(result.LastAccessDate.Date, Is.EqualTo(DateTime.Today));
        }

        [Test]
        public void Given_When_Create_Then_set_id()
        {
            // Given

            // When
            var result = AuthenticationToken.Create(user, applicationToken);

            // Then
            Assert.That(result.Id, Is.Not.Null);
        }
    }
}
