using System.Linq;

using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.UserTests
{
    [TestFixture]
    [Category("Unit")]
    public class DisableAuthenticationTokensTests
    {
       [Test]
        public void When_DisableAuthenticationTokens_Then_associated_tokens_are_set_to_disabled()
       {
           // Given
           var user = new User()
                      {
                          AuthenticationTokens = new []
                                                 {
                                                     new AuthenticationToken() { IsEnabled = true },
                                                     new AuthenticationToken() { IsEnabled = true },
                                                     new AuthenticationToken() { IsEnabled = true },
                                                     new AuthenticationToken() { IsEnabled = true }
                                                 }
                      };

           // When
           user.DisabledAuthenticationTokens();

           // Then
           Assert.That(user.AuthenticationTokens.Any(x => x.IsEnabled), Is.False);
       }
    }
}