using System;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.HazardousSubstanceRiskAssessmentTests
{
    [TestFixture]
    [Category("Unit")]
    public class AddControlMeasureToRiskAssessmentTest
    {
        [Test]
        public void Given_controlmeasure_Then_should_add_correctly()
        {
            //Given
            var user = new UserForAuditing();
            var riskAssessment = Domain.Entities.HazardousSubstanceRiskAssessment.Create("title", "reference", 200, user, null);
            
            
            var controlMeasure = HazardousSubstanceRiskAssessmentControlMeasure.Create("", riskAssessment, user);

            
            //When
            riskAssessment.AddControlMeasure(controlMeasure, user);

            //Then
            Assert.That(riskAssessment.ControlMeasures.Count, Is.EqualTo(1));
            Assert.That(riskAssessment.LastModifiedBy, Is.EqualTo(user));
            Assert.That(riskAssessment.LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Now.Date));
        }
    }
}