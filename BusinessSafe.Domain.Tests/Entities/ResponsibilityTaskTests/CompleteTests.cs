using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.ParameterClasses;

namespace BusinessSafe.Domain.Tests.Entities.ResponsibilityTaskTests
{
    [TestFixture]
    public class CompleteTests
    {
        [Test]
        public void Given_task_is_not_reocurring_When_Complete_called_Then_task_is_completed()
        {
            var employee = new Employee {Id = Guid.NewGuid()};
            var user = new User {Id = Guid.NewGuid(), Employee = employee};
            var userForAuditing = new UserForAuditing {Id = Guid.NewGuid()};

            var createDocumentParameterObjects1 = new List<CreateDocumentParameters>
                                                     {
                                                         new CreateDocumentParameters
                                                             {
                                                                 DocumentLibraryId = 726L
                                                             }
                                                     };

            var site = new Site {Id = 5232L};

            var task = ResponsibilityTask.Create(
                "Test Title 1",
                "Test Description 1",
                new DateTime(2050, 10, 01),
                TaskStatus.Outstanding,
                employee,
                userForAuditing,
                createDocumentParameterObjects1,
                new TaskCategory {Id = 4323L},
                (int) TaskReoccurringType.None,
                null,
                false,
                true,
                true,
                true,
                Guid.NewGuid(),
                site,
                new Responsibility());

            var createDocumentParameterObjects2 = new List<CreateDocumentParameters>
                                                     {
                                                         new CreateDocumentParameters
                                                             {
                                                                 DocumentLibraryId = 2344L
                                                             }
                                                     };

            task.Complete(
                "Completed Comments 1",
                createDocumentParameterObjects2,
                new List<long>(),
                userForAuditing,
                user
                );

            Assert.That(task.TaskStatus, Is.EqualTo(TaskStatus.Completed));
            Assert.That(task.TaskCompletedComments, Is.EqualTo("Completed Comments 1"));
            Assert.That(task.Documents.Count, Is.EqualTo(2));
            Assert.That(task.TaskCompletedBy, Is.EqualTo(userForAuditing));
            Assert.That(task.LastModifiedBy, Is.EqualTo(userForAuditing));
            Assert.That(task.LastModifiedOn.HasValue, Is.True);
            Assert.That(task.FollowingTask, Is.Null);
        }
        [Test]
        public void Given_task_is_reocurring_When_Complete_called_Then_task_is_completed()
        {
            var employee = new Employee { Id = Guid.NewGuid() };
            var user = new User { Id = Guid.NewGuid(), Employee = employee };
            var userForAuditing = new UserForAuditing { Id = Guid.NewGuid() };

            var createDocumentParameterObjects1 = new List<CreateDocumentParameters>
                                                     {
                                                         new CreateDocumentParameters
                                                             {
                                                                 DocumentLibraryId = 726L
                                                             }
                                                     };

            var site = new Site { Id = 5232L };

            var task = ResponsibilityTask.Create(
                "Test Title 1",
                "Test Description 1",
                new DateTime(2050, 10, 01),
                TaskStatus.Outstanding,
                employee,
                userForAuditing,
                createDocumentParameterObjects1,
                new TaskCategory { Id = 4323L },
                (int)TaskReoccurringType.Monthly,
                new DateTime(2051, 10, 01),
                false,
                true,
                true,
                true,
                Guid.NewGuid(),
                site,
                new Responsibility());

            var createDocumentParameterObjects2 = new List<CreateDocumentParameters>
                                                     {
                                                         new CreateDocumentParameters
                                                             {
                                                                 DocumentLibraryId = 2344L
                                                             }
                                                     };

            task.Complete(
                "Completed Comments 1",
                createDocumentParameterObjects2,
                new List<long>(),
                userForAuditing,
                user
                );

            Assert.That(task.TaskStatus, Is.EqualTo(TaskStatus.Completed));
            Assert.That(task.TaskCompletedComments, Is.EqualTo("Completed Comments 1"));
            Assert.That(task.Documents.Count, Is.EqualTo(2));
            Assert.That(task.TaskCompletedBy, Is.EqualTo(userForAuditing));
            Assert.That(task.LastModifiedBy, Is.EqualTo(userForAuditing));
            Assert.That(task.LastModifiedOn.HasValue, Is.True);
            Assert.That(task.FollowingTask, Is.Not.Null);
            Assert.That(task.FollowingTask.TaskCompletionDueDate, Is.EqualTo(new DateTime(2050, 11, 01)));
        }
    }
}
