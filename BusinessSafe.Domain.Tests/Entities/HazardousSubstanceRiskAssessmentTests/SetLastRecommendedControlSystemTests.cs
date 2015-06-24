using System;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.HazardousSubstanceRiskAssessmentTests
{
    [TestFixture]
    [Category("Unit")]
    public class SetLastRecommendedControlSystemTests
    {
        [Test]
        public void When_SetLastRecommendedControlSystem_Then_set_correct_properties()
        {
            // Given
            var riskAssessment = new HazardousSubstanceRiskAssessment();
            var controlSystem = new ControlSystem();
            var user = new UserForAuditing();

            // When
            riskAssessment.SetLastRecommendedControlSystem(controlSystem, user);

            // Then
            Assert.That(riskAssessment.LastRecommendedControlSystem, Is.EqualTo(controlSystem));
            Assert.That(riskAssessment.LastModifiedBy, Is.EqualTo(user));
            Assert.That(riskAssessment.LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Now.Date));
        }
    }
}