using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.RiskAssessmentTests
{
    [TestFixture]
    [Category("Unit")]
    public class RemoveControlMeasureFromRiskAssessmentHazardTest
    {
        [Test]
        public void Given_controlmeasure_Then_should_amend_last_modified()
        {
            //Given
            var user = new UserForAuditing();
            var riskAssessment = GeneralRiskAssessment.Create("", "", default(long), null);
            var hazard = new Hazard();

            var riskAssessmentHazard = MultiHazardRiskAssessmentHazard.Create(riskAssessment, hazard, user);
            riskAssessmentHazard.Id = 1;

            var controlMeasure = MultiHazardRiskAssessmentControlMeasure.Create("", riskAssessmentHazard, user);
            controlMeasure.Id = 1;

            riskAssessmentHazard.ControlMeasures.Add(controlMeasure);
            riskAssessment.Hazards.Add(riskAssessmentHazard);

            //When
            riskAssessmentHazard.RemoveControlMeasure(1, user);

            //Then
            Assert.That(riskAssessmentHazard.LastModifiedBy, Is.EqualTo(user));
        }

        [Test]
        public void Given_controlmeasure_Then_should_not_actually_delete_control_measure()
        {
            //Given
            var user = new UserForAuditing();
            var riskAssessment = GeneralRiskAssessment.Create("", "", default(long), null);
            var hazard = new Hazard();

            var riskAssessmentHazard = MultiHazardRiskAssessmentHazard.Create(riskAssessment, hazard, user);
            riskAssessmentHazard.Id = 1;

            var controlMeasure = MultiHazardRiskAssessmentControlMeasure.Create("", riskAssessmentHazard, user);
            controlMeasure.Id = 1;

            riskAssessmentHazard.ControlMeasures.Add(controlMeasure);
            riskAssessment.Hazards.Add(riskAssessmentHazard);

            //When
            riskAssessmentHazard.RemoveControlMeasure(1, user);

            //Then
            Assert.That(riskAssessmentHazard.ControlMeasures.Count, Is.EqualTo(1));
        }

        [Test]
        public void Given_controlmeasure_Then_should_mark_control_measure_as_deleted()
        {
            //Given
            var user = new UserForAuditing();
            var riskAssessment = GeneralRiskAssessment.Create("", "", default(long), null);
            var hazard = new Hazard();

            var riskAssessmentHazard = MultiHazardRiskAssessmentHazard.Create(riskAssessment, hazard, user);
            riskAssessmentHazard.Id = 1;

            var controlMeasure = MultiHazardRiskAssessmentControlMeasure.Create("", riskAssessmentHazard, user);
            controlMeasure.Id = 1;

            riskAssessmentHazard.ControlMeasures.Add(controlMeasure);
            riskAssessment.Hazards.Add(riskAssessmentHazard);

            //When
            riskAssessmentHazard.RemoveControlMeasure(1, user);

            //Then
            Assert.That(riskAssessmentHazard.ControlMeasures[0].Deleted, Is.True);
        }

        [Test]
        public void Given_control_measure_doesnt_exist_When_removing_control_measure_Then_should_get_correct_exception()
        {
            //Given
            var user = new UserForAuditing();
            var riskAssessment = GeneralRiskAssessment.Create("", "", default(long), null);
            var hazard = new Hazard();

            var riskAssessmentHazard = MultiHazardRiskAssessmentHazard.Create(riskAssessment, hazard, user);
            riskAssessmentHazard.Id = 1;

            var controlMeasure = MultiHazardRiskAssessmentControlMeasure.Create("", riskAssessmentHazard, user);
            controlMeasure.Id = 1;

            riskAssessmentHazard.ControlMeasures.Add(controlMeasure);
            riskAssessment.Hazards.Add(riskAssessmentHazard);

            //When
            //Then
            Assert.Throws<ControlMethodDoesNotExistInHazard>(() => riskAssessmentHazard.RemoveControlMeasure(2, user));
        }     
    }
}