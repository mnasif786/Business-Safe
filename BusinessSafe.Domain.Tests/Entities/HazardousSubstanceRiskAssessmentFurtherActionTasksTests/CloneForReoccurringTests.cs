using System;
using System.Collections.Generic;

using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.ParameterClasses;

using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.HazardousSubstanceRiskAssessmentFurtherActionTasksTests
{
    [TestFixture]
    [Category("Unit")]
    public class CloneForReoccurringTests
    {
        [Test]
        public void When_CloneForReoccurring_Then_should_set_properties_correctly()
        {
            //Given
            var user = new UserForAuditing();
            var assignedTo = new Employee();
            const string reference = "ref";
            const string title = "title";
            const string description = "desc";
            const TaskStatus taskStatus = TaskStatus.Outstanding;
            var taskcompletionduedate = DateTime.Now;
            var furtherControlMeasureTaskCategory = new TaskCategory();


            var target = HazardousSubstanceRiskAssessmentFurtherControlMeasureTask.Create(
                reference,
                title,
                description,
                taskcompletionduedate,
                taskStatus,
                assignedTo,
                user,
                new List<CreateDocumentParameters>(),
                furtherControlMeasureTaskCategory,
                0,
                null,
                false,
                false,
                false,
                false,
                Guid.NewGuid());

            //When
            var expectedCompletionDueDate = DateTime.Today;
            var result = target.CloneForReoccurring(user, expectedCompletionDueDate);
            

            //Then
            Assert.That(result.Reference, Is.EqualTo(target.Reference));
            Assert.That(result.Title, Is.EqualTo(target.Title));
            Assert.That(result.Description, Is.EqualTo(target.Description));
            Assert.That(result.TaskAssignedTo, Is.EqualTo(target.TaskAssignedTo));
            Assert.That(result.TaskStatus, Is.EqualTo(TaskStatus.Outstanding));
            Assert.That(result.TaskCompletionDueDate, Is.EqualTo(expectedCompletionDueDate));
            Assert.That(result.CreatedOn.Value.Date, Is.EqualTo(DateTime.Now.Date));
            Assert.That(result.CreatedBy, Is.EqualTo(user));
            Assert.That(result.OriginalTask, Is.EqualTo(target.OriginalTask));
            Assert.That(result.SendTaskCompletedNotification, Is.EqualTo(target.SendTaskCompletedNotification));
            Assert.That(result.SendTaskNotification, Is.EqualTo(target.SendTaskNotification));
            Assert.That(result.SendTaskOverdueNotification, Is.EqualTo(target.SendTaskOverdueNotification));
            Assert.That(result.SendTaskDueTomorrowNotification, Is.EqualTo(target.SendTaskDueTomorrowNotification));
        }
    }
}