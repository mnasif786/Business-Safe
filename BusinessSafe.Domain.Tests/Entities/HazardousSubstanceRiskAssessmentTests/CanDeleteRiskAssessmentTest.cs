using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.HazardousSubstanceRiskAssessmentTests
{
    [TestFixture]
    [Category("Unit")]
    public class CanDeleteRiskAssessmentTest
    {
        [Test]
        public void Given_riskassessment_no_furthercontrolmeasuretasks_Then_should_be_able_to_delete()
        {
            //Given
            var riskAssessment = HazardousSubstanceRiskAssessment.Create("", "", default(long), null, new HazardousSubstance());
            
            //When
            var result = riskAssessment.HasUndeletedTasks();

            //Then
            Assert.False(result);
        }

        
        [Test]
        public void Given_riskassessment_one_further_control_measure_Then_should_not_be_able_to_delete()
        {
            //Given
            var riskAssessment = HazardousSubstanceRiskAssessment.Create("", "", default(long), null, new HazardousSubstance());
            var task = new HazardousSubstanceRiskAssessmentFurtherControlMeasureTask();
            riskAssessment.FurtherControlMeasureTasks.Add(task);
            

            //When
            var result = riskAssessment.HasUndeletedTasks();

            //Then
            Assert.True(result);
        }

        [Test]
        public void Given_riskassessment_one_further_control_measure_which_is_deleted_Then_should_be_able_to_delete()
        {
            //Given
            var riskAssessment = HazardousSubstanceRiskAssessment.Create("", "", default(long), null, new HazardousSubstance());
            var task = new HazardousSubstanceRiskAssessmentFurtherControlMeasureTask()
                           {
                               Deleted = true
                           };
            riskAssessment.FurtherControlMeasureTasks.Add(task);

            //When
            var result = riskAssessment.HasUndeletedTasks();

            //Then
            Assert.False(result);
        }
    }
}