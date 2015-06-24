using System;

using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.Domain.Entities;

using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.GeneralRiskAssessmentFurtherActionTasksTests
{
    [TestFixture]
    [Category("Unit")]
    public class MarkForDeleteTests
    {
        [Test]
        public void When_mark_for_delete_Then_should_set_properties_correctly()
        {
            //Given
            var task = new MultiHazardRiskAssessmentFurtherControlMeasureTask();
            var user = new UserForAuditing();

            //When
            task.MarkForDelete(user);

            //Then
            Assert.That(task.Deleted, Is.True);
            Assert.That(task.LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Today));
            Assert.That(task.LastModifiedBy, Is.EqualTo(user));
        }

        [Test]
        public void When_mark_for_delete_for_task_set_as_completed_Then_should_throw_correct_exception()
        {
            //Given
            var task = new MultiHazardRiskAssessmentFurtherControlMeasureTask {TaskStatus = TaskStatus.Completed};
            var user = new UserForAuditing();

            //When
            //Then
            Assert.Throws<AttemptingToDeleteFurtherControlMeasureTaskThatIsCompletedException>(() => task.MarkForDelete(user));
        }
        [Test]
        public void When_mark_for_delete_Then_taskstatus_remains_the_same()
        {
            //Given
            var task = new MultiHazardRiskAssessmentFurtherControlMeasureTask {TaskStatus = TaskStatus.Outstanding};
            ;
            var user = new UserForAuditing();

            //When
            task.MarkForDelete(user);

            //Then
            Assert.That(task.TaskStatus, Is.EqualTo(TaskStatus.Outstanding));
        }
    }
}