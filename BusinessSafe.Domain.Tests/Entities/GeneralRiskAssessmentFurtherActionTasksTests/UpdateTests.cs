using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.ParameterClasses;

using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.GeneralRiskAssessmentFurtherActionTasksTests
{
    [TestFixture]
    [Category("Unit")]
    public class UpdateTests
    {
        [TestCase(false, false, false, false)]
        [TestCase(true, false, false, false)]
        [TestCase(false, true, false, false)]
        [TestCase(false, false, true, false)]
        [TestCase(false, false, false, true)]
        [TestCase(true, true, true, true)]
        public void Given_notification_settings_When_Update_Then_should_set_properties_correctly(bool sendTaskCompletedNotification, bool sendTaskNotification, bool sendTaskOverdueNotification, bool sendTaskDueTomorrowNotification)
        {
            //Given
            var user = new UserForAuditing();
            var assignedTo = new Employee();
            const string reference = "ref";
            const string title = "title";
            const string description = "desc";
            const TaskStatus taskStatus = TaskStatus.Completed;
            var taskcompletionduedate = DateTime.Now;
            var furtherControlMeasureTaskCategory = new TaskCategory();

            var target = MultiHazardRiskAssessmentFurtherControlMeasureTask.Create("",
                "",
                "",
                DateTime.Now,
                TaskStatus.Outstanding,
                null,
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
            target.Update(reference, title, description, taskcompletionduedate, taskStatus, new List<CreateDocumentParameters>(), new List<long>(), 0, null, assignedTo, user, sendTaskCompletedNotification, sendTaskNotification, sendTaskOverdueNotification, sendTaskDueTomorrowNotification);

            //Then
            Assert.That(target.Reference, Is.EqualTo(reference));
            Assert.That(target.Title, Is.EqualTo(title));
            Assert.That(target.Description, Is.EqualTo(description));
            Assert.That(target.TaskAssignedTo, Is.EqualTo(assignedTo));
            Assert.That(target.TaskStatus, Is.EqualTo(taskStatus));
            Assert.That(target.TaskCompletionDueDate, Is.EqualTo(taskcompletionduedate));
            Assert.That(target.LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Now.Date));
            Assert.That(target.LastModifiedBy, Is.EqualTo(user));
            Assert.That(target.SendTaskCompletedNotification, Is.EqualTo(sendTaskCompletedNotification));
            Assert.That(target.SendTaskNotification, Is.EqualTo(sendTaskNotification));
            Assert.That(target.SendTaskOverdueNotification, Is.EqualTo(sendTaskOverdueNotification));
            Assert.That(target.SendTaskDueTomorrowNotification, Is.EqualTo(sendTaskDueTomorrowNotification));
        }

        [TestCase(false, false, false, false)]
        [TestCase(true, false, false, false)]
        [TestCase(false, true, false, false)]
        [TestCase(false, false, true, false)]
        [TestCase(false, false, false, true)]
        [TestCase(true, true, true, true)]
        public void Given_request_contains_documents_and_ids_of_documents_to_delete_and_notifications_When_Update_Then_should_set_properties_correctly(bool sendTaskCompletedNotification, bool sendTaskNotification, bool sendTaskOverdueNotification, bool sendTaskDueTomorrowNotification)
        {
            //Given
            var user = new UserForAuditing();
            var assignedTo = new Employee();
            const string reference = "ref";
            const string title = "title";
            const string description = "desc";
            const TaskStatus taskStatus = TaskStatus.Completed;
            var taskcompletionduedate = DateTime.Now;
            var furtherControlMeasureTaskCategory = new TaskCategory();

            var target = MultiHazardRiskAssessmentFurtherControlMeasureTask.Create("",
                "",
                "",
                DateTime.Now,
                TaskStatus.Outstanding,
                null,
                user,
                new List<CreateDocumentParameters>
                    {
                        new CreateDocumentParameters
                            {
                                DocumentLibraryId = 20L,
                                Description = "Test Document 1"
                            },
                        new CreateDocumentParameters
                            {
                                DocumentLibraryId = 21L,
                                Description = "Test Document 2"
                            }
                    },
                furtherControlMeasureTaskCategory,
                0,
                null,
                false,
                false,
                false,
                false,
                Guid.NewGuid());

            //When
            target.Update(
                reference,
                title,
                description,
                taskcompletionduedate,
                taskStatus,
                new List<CreateDocumentParameters>
                    {
                        new CreateDocumentParameters
                            {
                                DocumentLibraryId = 22L,
                                Description = "Test Document 3"
                            }
                    },
                new List<long> { 20 },
                0,
                null,
                assignedTo,
                user, sendTaskCompletedNotification, sendTaskNotification, sendTaskOverdueNotification, sendTaskDueTomorrowNotification);

            //Then
            var currentDocuments = target.Documents.Where(x => !x.Deleted).ToArray();
            Assert.That(target.Reference, Is.EqualTo(reference));
            Assert.That(target.Title, Is.EqualTo(title));
            Assert.That(target.Description, Is.EqualTo(description));
            Assert.That(target.TaskAssignedTo, Is.EqualTo(assignedTo));
            Assert.That(target.TaskStatus, Is.EqualTo(taskStatus));
            Assert.That(target.TaskCompletionDueDate, Is.EqualTo(taskcompletionduedate));
            Assert.That(target.LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Now.Date));
            Assert.That(target.LastModifiedBy, Is.EqualTo(user));
            Assert.That(currentDocuments.Count(), Is.EqualTo(2));
            Assert.That(currentDocuments[0].DocumentLibraryId, Is.EqualTo(21L));
            Assert.That(currentDocuments[0].Description, Is.EqualTo("Test Document 2"));
            Assert.That(currentDocuments[1].DocumentLibraryId, Is.EqualTo(22L));
            Assert.That(currentDocuments[1].Description, Is.EqualTo("Test Document 3"));
            Assert.That(target.SendTaskCompletedNotification, Is.EqualTo(sendTaskCompletedNotification));
            Assert.That(target.SendTaskNotification, Is.EqualTo(sendTaskNotification));
            Assert.That(target.SendTaskOverdueNotification, Is.EqualTo(sendTaskOverdueNotification));
            Assert.That(target.SendTaskDueTomorrowNotification, Is.EqualTo(sendTaskDueTomorrowNotification));
        }
    }
}