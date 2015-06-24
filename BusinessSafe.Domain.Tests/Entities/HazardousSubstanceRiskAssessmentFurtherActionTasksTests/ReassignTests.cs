using System;

using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.Domain.Entities;

using NUnit.Framework;

using System.Linq;

namespace BusinessSafe.Domain.Tests.Entities.HazardousSubstanceRiskAssessmentFurtherActionTasksTests
{
    [TestFixture]
    [Category("Unit")]
    public class ReassignTests
    {
        [Test]
        public void When_reassign_Then_should_set_properties_correctly()
        {
            //Given
            var task = new HazardousSubstanceRiskAssessmentFurtherControlMeasureTask();
            var user = new UserForAuditing();
            var employeeReassigningTo = new Employee();

            //When
            task.ReassignTask(employeeReassigningTo, user);

            //Then
            Assert.That(task.TaskAssignedTo, Is.EqualTo(employeeReassigningTo));
            Assert.That(task.LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Today));
            Assert.That(task.LastModifiedBy, Is.EqualTo(user));

            // todo: doesn't archive yet
            //Assert.That(task.Archive.Count(x => x.ArchiveAction == FurtherActionTaskArchiveAction.Reassign.ToString()), Is.EqualTo(1));
        }

        [Test]
        public void When_reassign_task_set_as_completed_Then_should_throw_correct_exception()
        {
            //Given
            var task = new HazardousSubstanceRiskAssessmentFurtherControlMeasureTask { TaskStatus = TaskStatus.Completed };
            var user = new UserForAuditing();

            //When
            //Then
            Assert.Throws<AttemptingToReassignFurtherActionTaskThatIsCompletedException>(() => task.ReassignTask(null, user));
        }
    }
}