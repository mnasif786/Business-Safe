
using BusinessSafe.Domain.Entities;

using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.PersonalRiskAssessmentTests
{
    [TestFixture]
    public class PrefixTests
    {
        [Test]
        public void When_Prefix_Then_Return_PRA()
        {
            // Given
            var target = new PersonalRiskAssessment();

            // When
            var result = target.PreFix;

            // Then
            Assert.That(result, Is.EqualTo("PRA"));
        }
    }
}
