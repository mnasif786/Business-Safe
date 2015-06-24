using System;
using System.Collections.Generic;

using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.ParameterClasses;

using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.GeneralRiskAssessmentFurtherActionTasksTests
{
    [TestFixture]
    [Category("Unit")]
    public class CreateTests
    {
        [Test]
        public void When_Create_Then_should_set_properties_correctly()
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

            //When
            var target = MultiHazardRiskAssessmentFurtherControlMeasureTask.Create(
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

            //Then
            Assert.That(target.Reference, Is.EqualTo(reference));
            Assert.That(target.Title, Is.EqualTo(title));
            Assert.That(target.Description, Is.EqualTo(description));
            Assert.That(target.TaskAssignedTo, Is.EqualTo(assignedTo));
            Assert.That(target.TaskStatus, Is.EqualTo(taskStatus));
            Assert.That(target.TaskCompletionDueDate, Is.EqualTo(taskcompletionduedate));
            Assert.That(target.CreatedOn.Value.Date, Is.EqualTo(DateTime.Now.Date));
            Assert.That(target.CreatedBy, Is.EqualTo(user));
            Assert.That(target.OriginalTask, Is.EqualTo(target));
            Assert.That(target.TaskGuid, Is.Not.EqualTo(default(Guid)));
        }

        [Test]
        public void Given_documents_are_included_in_request_When_Create_Then_should_set_properties_correctly()
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

            //When
            var target = MultiHazardRiskAssessmentFurtherControlMeasureTask.Create(
                reference,
                title,
                description,
                taskcompletionduedate,
                taskStatus,
                assignedTo,
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

            //Then
            Assert.That(target.Reference, Is.EqualTo(reference));
            Assert.That(target.Title, Is.EqualTo(title));
            Assert.That(target.Description, Is.EqualTo(description));
            Assert.That(target.TaskAssignedTo, Is.EqualTo(assignedTo));
            Assert.That(target.TaskStatus, Is.EqualTo(taskStatus));
            Assert.That(target.TaskCompletionDueDate, Is.EqualTo(taskcompletionduedate));
            Assert.That(target.CreatedOn.Value.Date, Is.EqualTo(DateTime.Now.Date));
            Assert.That(target.CreatedBy, Is.EqualTo(user));
            Assert.That(target.Documents.Count, Is.EqualTo(2));
            Assert.That(target.Documents[0].DocumentLibraryId, Is.EqualTo(20L));
            Assert.That(target.Documents[0].Description, Is.EqualTo("Test Document 1"));
            Assert.That(target.Documents[1].DocumentLibraryId, Is.EqualTo(21L));
            Assert.That(target.Documents[1].Description, Is.EqualTo("Test Document 2"));
        }
    }
}