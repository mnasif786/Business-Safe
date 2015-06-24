using System;

using BusinessSafe.Domain.Entities;

using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.RiskAssessmentControlMeasureTests
{
    [TestFixture]
    [Category("Unit")]
    public class CreateTests
    {
        [Test]
        public void Given_all_required_felds_are_available_Then_create_RiskAssessmentControlMeasure_method_creates_an_object()
        {
            //Given
            const string controlMeasure = "CM1";
            var riskAssessmentHazard = new MultiHazardRiskAssessmentHazard(); 
            var assignedTo = new UserForAuditing();

            //When
            var result = MultiHazardRiskAssessmentControlMeasure.Create(controlMeasure, riskAssessmentHazard, assignedTo);

            //Then
            Assert.That(result.ControlMeasure,Is.EqualTo(controlMeasure));
            
            Assert.That(result.CreatedOn, Is.Not.Null);
            Assert.That(result.CreatedBy, Is.Not.Null);
            Assert.That(result.MultiHazardRiskAssessmentHazard, Is.Not.Null);
            
        }
    }
}
