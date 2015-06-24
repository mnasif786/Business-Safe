using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.RiskAssessmentHazardTests
{
    [TestFixture]
    [Category("Unit")]
    public class CanDeleteHazardTests
    {
        [Test]
        public void When_got_no_further_control_measures_Then_should_be_able_to_delete()
        {
            //Given
            var riskAssessmentHazard = new MultiHazardRiskAssessmentHazard();

            //When
            var result = riskAssessmentHazard.CanDeleteHazard();

            //Then
            Assert.True(result);
        }

        [Test]
        public void When_got_one_further_control_measures_Then_should_not_be_able_to_delete()
        {
            //Given
            var riskAssessmentHazard = new MultiHazardRiskAssessmentHazard();
            riskAssessmentHazard.AddFurtherActionTask(new MultiHazardRiskAssessmentFurtherControlMeasureTask(), new UserForAuditing());

            //When
            var result = riskAssessmentHazard.CanDeleteHazard();

            //Then
            Assert.False(result);
        }

        [Test]
        public void When_got_one_further_control_measures_but_is_deleted_Then_should_be_able_to_delete()
        {
            //Given
            var riskAssessmentHazard = new MultiHazardRiskAssessmentHazard();
            var user = new UserForAuditing();
            var riskAssessmentFurtherActionTask = new MultiHazardRiskAssessmentFurtherControlMeasureTask();
            riskAssessmentFurtherActionTask.MarkForDelete(user);
            riskAssessmentHazard.AddFurtherActionTask(riskAssessmentFurtherActionTask, user);

            //When
            var result = riskAssessmentHazard.CanDeleteHazard();

            //Then
            Assert.True(result);
        }

        [Test]
        public void When_got_two_further_control_measures_one_is_not_deleted_Then_should_not_be_able_to_delete()
        {
            //Given
            var riskAssessmentHazard = new MultiHazardRiskAssessmentHazard();
            var user = new UserForAuditing();
            var riskAssessmentFurtherActionTask = new MultiHazardRiskAssessmentFurtherControlMeasureTask();
            riskAssessmentFurtherActionTask.MarkForDelete(user);
            riskAssessmentHazard.AddFurtherActionTask(riskAssessmentFurtherActionTask, user);
            riskAssessmentHazard.AddFurtherActionTask(new MultiHazardRiskAssessmentFurtherControlMeasureTask(), user);

            //When
            var result = riskAssessmentHazard.CanDeleteHazard();

            //Then
            Assert.False(result);
        }
    }
}