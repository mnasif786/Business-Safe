using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.RiskAssessmentTests
{
    [TestFixture]
    [Category("Unit")]
    public class CanDeleteRiskAssessmentTest
    {
        [Test]
        public void Given_riskassessment_no_hazards_Then_should_be_able_to_delete()
        {
            //Given
            var riskAssessment = GeneralRiskAssessment.Create("", "", default(long), null);
            
            //When
            var result = riskAssessment.HasUndeletedTasks();

            //Then
            Assert.False(result);
        }

        [Test]
        public void Given_riskassessment_one_hazard_no_further_control_measure_Then_should_be_able_to_delete()
        {
            //Given
            var hazard = new Hazard();
            var user = new UserForAuditing();
            
            var riskAssessment = GeneralRiskAssessment.Create("", "", default(long), null);
            var riskAssessmentHazard = MultiHazardRiskAssessmentHazard.Create(riskAssessment, hazard, user);
            riskAssessment.Hazards.Add(riskAssessmentHazard);

            //When
            var result = riskAssessment.HasUndeletedTasks();

            //Then
            Assert.False(result);

        }

        [Test]
        public void Given_riskassessment_one_hazard_one_further_control_measure_Then_should_not_be_able_to_delete()
        {
            //Given
            var hazard = new Hazard();
            var user = new UserForAuditing();

            var furtherControlMeasure = new MultiHazardRiskAssessmentFurtherControlMeasureTask();

            var riskAssessment = GeneralRiskAssessment.Create("", "", default(long), null);
            var riskAssessmentHazard = MultiHazardRiskAssessmentHazard.Create(riskAssessment, hazard, user);
            riskAssessmentHazard.FurtherControlMeasureTasks.Add(furtherControlMeasure);
            riskAssessment.Hazards.Add(riskAssessmentHazard);

            //When
            var result = riskAssessment.HasUndeletedTasks();

            //Then
            Assert.True(result);
        }

        [Test]
        public void Given_riskassessment_one_hazard_one_further_control_measure_which_is_deleted_Then_should_be_able_to_delete()
        {
            //Given
            var hazard = new Hazard();
            var user = new UserForAuditing();

            var furtherControlMeasure = new MultiHazardRiskAssessmentFurtherControlMeasureTask();
            furtherControlMeasure.MarkForDelete(user);

            var riskAssessment = GeneralRiskAssessment.Create("", "", default(long), null);
            var riskAssessmentHazard = MultiHazardRiskAssessmentHazard.Create(riskAssessment, hazard, user);
            riskAssessmentHazard.FurtherControlMeasureTasks.Add(furtherControlMeasure);
            riskAssessment.Hazards.Add(riskAssessmentHazard);

            //When
            var result = riskAssessment.HasUndeletedTasks();

            //Then
            Assert.False(result);
        }
    }
}