using System;
using System.Collections.Generic;
using System.Linq;

using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.ParameterClasses;

using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.GeneralRiskAssessmentFurtherActionTasksTests
{
    [TestFixture]
    [Category("Unit")]
    public class MarkAsNoLongeerRequiredTests
    {
        [Test]
        public void When_mark_as_no_longer_required_completed_task_Then_should_throw_correct_exceptions()
        {
            //Given
            var task = new MultiHazardRiskAssessmentFurtherControlMeasureTask();
            var user = new UserForAuditing();

            task.Complete("", new List<CreateDocumentParameters>(), new List<long>(), user, null, DateTime.Now);

            //When
            //Then
            Assert.Throws<AttemptingToMarkAsNoLongerRequiredFurtherActionTaskThatIsCompletedException>(() => task.MarkAsNoLongerRequired(user));
        }

        [Test]
        public void When_mark_as_no_longer_required_Then_should_set_properties_correctly()
        {
            //Given
            var task = new MultiHazardRiskAssessmentFurtherControlMeasureTask();
            var user = new UserForAuditing();

            //When
            task.MarkAsNoLongerRequired(user);

            //Then
            Assert.That(task.LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Today));
            Assert.That(task.LastModifiedBy, Is.EqualTo(user));
            Assert.That(task.TaskStatus, Is.EqualTo(TaskStatus.NoLongerRequired));

            //todo: add this back in when archive is back.
            //Assert.That(task.Archive.Count(x => x.ArchiveAction == FurtherActionTaskArchiveAction.MarkAsNoLongerRequired.ToString()),Is.EqualTo(1));
        }
    }
}