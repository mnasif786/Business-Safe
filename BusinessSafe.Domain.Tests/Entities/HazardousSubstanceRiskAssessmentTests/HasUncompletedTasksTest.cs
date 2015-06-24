using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.HazardousSubstanceRiskAssessmentTests
{
    [TestFixture]
    [Category("Unit")]
    public class HasUncompletedTasksTest
    {
        [Test]
        public void Given_riskassessment_no_furthercontrolmeasuretasks_Then_should_return_false()
        {
            //Given
            var riskAssessment = HazardousSubstanceRiskAssessment.Create("", "", default(long), null, new HazardousSubstance());

            //When
            var result = riskAssessment.HasUncompletedTasks();

            //Then
            Assert.False(result);
        }


        [Test]
        public void Given_riskassessment_one_outstanding_further_control_measure_Then_should_return_true()
        {
            //Given
            var riskAssessment = HazardousSubstanceRiskAssessment.Create("", "", default(long), null, new HazardousSubstance());
            var task = new HazardousSubstanceRiskAssessmentFurtherControlMeasureTask();
            task.TaskStatus = TaskStatus.Outstanding;
            riskAssessment.FurtherControlMeasureTasks.Add(task);
            
            //When
            var result = riskAssessment.HasUncompletedTasks();

            //Then
            Assert.True(result);
        }

        [Test]
        public void Given_riskassessment_one_further_control_measure_which_is_deleted_Then_should_return_true()
        {
            //Given
            var riskAssessment = HazardousSubstanceRiskAssessment.Create("", "", default(long), null, new HazardousSubstance());
            var task = new HazardousSubstanceRiskAssessmentFurtherControlMeasureTask()
                           {
                               Deleted = true
                           };
            riskAssessment.FurtherControlMeasureTasks.Add(task);

            //When
            var result = riskAssessment.HasUncompletedTasks();

            //Then
            Assert.False(result);
        }


    }
}