using System;
using BusinessSafe.WebSite.Helpers;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.AuthenticationService
{
    [TestFixture]
    public class ImpersonateGuardTests
    {
        [Test]
        public void Given_impersonation_config_setting_is_not_set_When_IsAllowed_Then_should_return_false()
        {
            // Given
            var target = new ImpersonateGuard(new ImpersonateGuardSettings()
            {
                IsImpersonateOn = null,
                Environment = "CI",
                AllowedUrlReferrerHost = string.Empty
            });

            // When
            var result = target.IsAllowed(new Uri("http://request.referrer.com"));

            // Then
            Assert.That(result, Is.False);
        }

        [Test]
        public void Given_impersonation_is_off_When_IsAllowed_Then_should_return_false()
        {
            // Given
            var target = new ImpersonateGuard(new ImpersonateGuardSettings()
            {
                IsImpersonateOn = "false",
                Environment = "CI",
                AllowedUrlReferrerHost = string.Empty
            });


            // When
            var result = target.IsAllowed(new Uri("http://request.referrer.com"));

            // Then
            Assert.That(result, Is.False);
        }

        [Test]
        public void Given_impersonation_is_on_When_IsAllowed_Then_should_return_true()
        {
            // Given
            var target = new ImpersonateGuard(new ImpersonateGuardSettings()
            {
                IsImpersonateOn = "true",
                Environment = "CI",
                AllowedUrlReferrerHost = string.Empty
            });


            // When
            var result = target.IsAllowed(new Uri("http://request.referrer.com"));

            // Then
            Assert.That(result, Is.True);
        }

        [Test]
        public void Given_impersonation_is_on_and_environment_is_not_live_When_IsAllowed_Then_should_return_true()
        {
            // Given
            var target = new ImpersonateGuard(new ImpersonateGuardSettings()
            {
                IsImpersonateOn = "true",
                Environment = "UAT",
                AllowedUrlReferrerHost = string.Empty
            });


            // When
            var result = target.IsAllowed(new Uri("http://request.referrer.com"));

            // Then
            Assert.That(result, Is.True);
        }

        [Test]
        public void Given_impersonation_is_on_and_environment_is_live_but_allowed_url_referrer_is_not_set_When_IsAllowed_Then_should_return_false()
        {
            // Given
            var target = new ImpersonateGuard(new ImpersonateGuardSettings()
            {
                IsImpersonateOn = "true",
                Environment = "LIVE",
                AllowedUrlReferrerHost = string.Empty
            });

            // When
            var result = target.IsAllowed(new Uri("http://request.referrer.com"));

            // Then
            Assert.That(result, Is.False);
        }

        [Test]
        public void Given_impersonation_is_on_and_environment_is_live_and_allowed_url_referrer_is_set_and_does_not_match_request_url_referrer_When_IsAllowed_Then_should_return_false()
        {
            // Given
            var target = new ImpersonateGuard(new ImpersonateGuardSettings()
            {
                IsImpersonateOn = "true",
                Environment = "LIVE",
                AllowedUrlReferrerHost = "allowedreferrer.com"
            });

            // When
            var result = target.IsAllowed(new Uri("http://hello_not_allowedreferrer.com"));

            // Then
            Assert.That(result, Is.False);
        }

        [Test]
        public void Given_impersonation_is_on_and_environment_is_live_and_allowed_url_referrer_is_set_but_request_url_referrer_not_set_When_IsAllowed_Then_should_return_false()
        {
            // Given
            var target = new ImpersonateGuard(new ImpersonateGuardSettings()
            {
                IsImpersonateOn = "true",
                Environment = "LIVE",
                AllowedUrlReferrerHost = "allowedreferrer.com"
            });

            // When
            var result = target.IsAllowed(null);

            // Then
            Assert.That(result, Is.False);
        }

        [Test]
        public void Given_impersonation_is_on_and_environment_is_live_and_allowed_url_referrer_is_set_and_does_match_request_url_referrer_When_IsAllowed_Then_should_return_true()
        {
            // Given
            var target = new ImpersonateGuard(new ImpersonateGuardSettings()
            {
                IsImpersonateOn = "true",
                Environment = "LIVE",
                AllowedUrlReferrerHost = "allowedreferrer.com"
            });

            // When
            var result = target.IsAllowed(new Uri("http://allowedreferrer.com"));

            // Then
            Assert.That(result, Is.True);
        }
    }
}