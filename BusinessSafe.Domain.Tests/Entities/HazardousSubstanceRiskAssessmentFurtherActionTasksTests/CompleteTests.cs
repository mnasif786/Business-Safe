using System;
using System.Collections.Generic;
using System.Linq;

using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.Domain.Entities;
using Moq;
using NUnit.Framework;

using BusinessSafe.Domain.ParameterClasses;

namespace BusinessSafe.Domain.Tests.Entities.HazardousSubstanceRiskAssessmentFurtherActionTasksTests
{
    [TestFixture]
    [Category("Unit")]
    public class CompleteTests
    {
        [Test]
        public void When_complete_task_already_completed_Then_should_throw_correct_exception()
        {
            //Given
            var task = new HazardousSubstanceRiskAssessmentFurtherControlMeasureTask();
            var user = new UserForAuditing();

            task.Complete("comments", new List<CreateDocumentParameters>(), new List<long>(), user, null, DateTime.Now);

            //When
            //Then
            Assert.Throws<AttemptingToCompleteTaskThatIsCompletedException>(() => task.Complete("comments", new List<CreateDocumentParameters>(), new List<long>(), user, null, DateTime.Now));
        }

        [Test]
        public void When_complete_Then_should_set_properties_correctly()
        {
            //Given
            var task = new HazardousSubstanceRiskAssessmentFurtherControlMeasureTask();
            var user = new UserForAuditing();

            //When
            task.Complete("comments", new List<CreateDocumentParameters>(), new List<long>(), user, null, DateTime.Now);

            //Then
            Assert.That(task.LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Today));
            Assert.That(task.LastModifiedBy, Is.EqualTo(user));
            Assert.That(task.TaskStatus, Is.EqualTo(TaskStatus.Completed));
            Assert.That(task.TaskCompletedComments, Is.EqualTo("comments"));
            Assert.That(task.TaskCompletedDate.Value.Date, Is.EqualTo(DateTime.Today.Date));
            Assert.That(task.LastModifiedBy, Is.EqualTo(user));
            // todo: doesn't archive yet
            //Assert.That(task.Archive.Count(x => x.ArchiveAction == FurtherActionTaskArchiveAction.Complete.ToString()), Is.EqualTo(1));
        }

        [Test]
        public void Given_FCM_is_reocurring_weekly_Then_when_it_is_completed_Then_following_task_should_be_created_with_due_date_a_week_later()
        {
            var assignedTo = new Employee { Id = Guid.NewGuid() };
            var user1= new UserForAuditing { Id = Guid.NewGuid() };
            var user2= new UserForAuditing { Id = Guid.NewGuid() };

            var furtherControlMeasureTask = HazardousSubstanceRiskAssessmentFurtherControlMeasureTask.Create(
                "FCM",
                "Test FCM",
                "Description",
                new DateTime(2012, 09, 13),
                TaskStatus.Outstanding,
                assignedTo,
                user1,
                new System.Collections.Generic.List<CreateDocumentParameters>
                    {
                        new CreateDocumentParameters
                            {
                                DocumentLibraryId = 2000L,
                                Description = "Test File 1",
                                DocumentType = new DocumentType {Id = 5L},
                                Filename = "Test File 1.txt"
                            }
                    },
                new TaskCategory {Id = 9L},
                (int) TaskReoccurringType.Weekly,
                new DateTime(2013, 09, 13),
                false,
                false,
                false,
                false,
                Guid.NewGuid());

            furtherControlMeasureTask.Complete("Test Comments", new List<CreateDocumentParameters>(), new List<long>(), user2, null, DateTime.Now);

            Assert.IsNotNull(furtherControlMeasureTask.FollowingTask);
            Assert.AreEqual(furtherControlMeasureTask,
                            furtherControlMeasureTask.FollowingTask.
                                PrecedingTask);
            Assert.AreEqual(furtherControlMeasureTask.Reference,
                            furtherControlMeasureTask.FollowingTask.Reference);
            Assert.AreEqual(furtherControlMeasureTask.Title,
                            furtherControlMeasureTask.FollowingTask.Title);
            Assert.AreEqual(furtherControlMeasureTask.Description,
                            furtherControlMeasureTask.FollowingTask.Description);
            Assert.AreEqual(new DateTime(2012, 09, 20),
                                         furtherControlMeasureTask.FollowingTask.TaskCompletionDueDate);
            Assert.AreEqual(furtherControlMeasureTask.TaskAssignedTo.Id,
                            furtherControlMeasureTask.FollowingTask.TaskAssignedTo.Id);
            Assert.AreEqual(user2.Id,
                            furtherControlMeasureTask.FollowingTask.CreatedBy.Id);
            Assert.AreEqual(1, furtherControlMeasureTask.FollowingTask.Documents.Count);
            Assert.AreEqual(furtherControlMeasureTask.Documents[0].DocumentLibraryId, furtherControlMeasureTask.FollowingTask.Documents[0].DocumentLibraryId);
            Assert.AreEqual(furtherControlMeasureTask.Documents[0].Description, furtherControlMeasureTask.FollowingTask.Documents[0].Description);
            Assert.AreEqual(furtherControlMeasureTask.Documents[0].DocumentType.Id, furtherControlMeasureTask.FollowingTask.Documents[0].DocumentType.Id);
            Assert.AreEqual(furtherControlMeasureTask.Documents[0].Filename, furtherControlMeasureTask.FollowingTask.Documents[0].Filename);
            Assert.AreEqual(furtherControlMeasureTask.Category.Id, furtherControlMeasureTask.FollowingTask.Category.Id);
            Assert.AreEqual(furtherControlMeasureTask.TaskReoccurringType, furtherControlMeasureTask.FollowingTask.TaskReoccurringType);
            Assert.AreEqual(furtherControlMeasureTask.TaskReoccurringEndDate, furtherControlMeasureTask.FollowingTask.TaskReoccurringEndDate);
        }

        [Test]
        public void Given_task_is_reocurring_but_reocurring_end_date_is_before_next_due_date_When_it_is_completed_Then_following_task_should_not_be_created()
        {
            var assignedTo = new Employee { Id = Guid.NewGuid() };
            var user1 = new UserForAuditing { Id = Guid.NewGuid() };
            var user2 = new UserForAuditing { Id = Guid.NewGuid() };

            var furtherControlMeasureTask = HazardousSubstanceRiskAssessmentFurtherControlMeasureTask.Create(
                "FCM",
                "Test FCM",
                "Description",
                new DateTime(2012, 09, 13),
                TaskStatus.Outstanding,
                assignedTo,
                user1,
                new System.Collections.Generic.List<CreateDocumentParameters>
                    {
                        new CreateDocumentParameters
                            {
                                DocumentLibraryId = 2000L,
                                Description = "Test File 1",
                                DocumentType = new DocumentType {Id = 5L},
                                Filename = "Test File 1.txt"
                            }
                    },
                new TaskCategory { Id = 9L },
                (int)TaskReoccurringType.Weekly,
                new DateTime(2012, 09, 14),
                false,
                false,
                false,
                false,
                Guid.NewGuid());

            furtherControlMeasureTask.Complete("Test Comments", new List<CreateDocumentParameters>(), new List<long>(), user2, null, DateTime.Now);

            Assert.IsNull(furtherControlMeasureTask.FollowingTask);
        }

        [Test]
        public void Given_task_is_reocurring_and_reocurring_end_date_is_equal_to_next_due_date_When_it_is_completed_Then_following_task_should_be_created()
        {
            var assignedTo = new Employee { Id = Guid.NewGuid() };
            var user1 = new UserForAuditing { Id = Guid.NewGuid() };
            var user2 = new UserForAuditing { Id = Guid.NewGuid() };

            var furtherControlMeasureTask = HazardousSubstanceRiskAssessmentFurtherControlMeasureTask.Create(
                "FCM",
                "Test FCM",
                "Description",
                new DateTime(2012, 09, 13),
                TaskStatus.Outstanding,
                assignedTo,
                user1,
                new System.Collections.Generic.List<CreateDocumentParameters>
                    {
                        new CreateDocumentParameters
                            {
                                DocumentLibraryId = 2000L,
                                Description = "Test File 1",
                                DocumentType = new DocumentType {Id = 5L},
                                Filename = "Test File 1.txt"
                            }
                    },
                new TaskCategory { Id = 9L },
                (int)TaskReoccurringType.Weekly,
                new DateTime(2012, 09, 20),
                false,
                false,
                false,
                false,
                Guid.NewGuid());

            furtherControlMeasureTask.Complete("Test Comments", new List<CreateDocumentParameters>(), new List<long>(), user2, null, DateTime.Now);

            Assert.IsNotNull(furtherControlMeasureTask.FollowingTask);
            Assert.AreEqual(furtherControlMeasureTask,
                            furtherControlMeasureTask.FollowingTask.
                                PrecedingTask);
            Assert.AreEqual(furtherControlMeasureTask.Reference,
                            furtherControlMeasureTask.FollowingTask.Reference);
            Assert.AreEqual(furtherControlMeasureTask.Title,
                            furtherControlMeasureTask.FollowingTask.Title);
            Assert.AreEqual(furtherControlMeasureTask.Description,
                            furtherControlMeasureTask.FollowingTask.Description);
            Assert.AreEqual(new DateTime(2012, 09, 20),
                                         furtherControlMeasureTask.FollowingTask.TaskCompletionDueDate);
            Assert.AreEqual(furtherControlMeasureTask.TaskAssignedTo.Id,
                            furtherControlMeasureTask.FollowingTask.TaskAssignedTo.Id);
            Assert.AreEqual(user2.Id,
                            furtherControlMeasureTask.FollowingTask.CreatedBy.Id);
            Assert.AreEqual(1, furtherControlMeasureTask.FollowingTask.Documents.Count);
            Assert.AreEqual(furtherControlMeasureTask.Documents[0].DocumentLibraryId, furtherControlMeasureTask.FollowingTask.Documents[0].DocumentLibraryId);
            Assert.AreEqual(furtherControlMeasureTask.Documents[0].Description, furtherControlMeasureTask.FollowingTask.Documents[0].Description);
            Assert.AreEqual(furtherControlMeasureTask.Documents[0].DocumentType.Id, furtherControlMeasureTask.FollowingTask.Documents[0].DocumentType.Id);
            Assert.AreEqual(furtherControlMeasureTask.Documents[0].Filename, furtherControlMeasureTask.FollowingTask.Documents[0].Filename);
            Assert.AreEqual(furtherControlMeasureTask.Category.Id, furtherControlMeasureTask.FollowingTask.Category.Id);
            Assert.AreEqual(furtherControlMeasureTask.TaskReoccurringType, furtherControlMeasureTask.FollowingTask.TaskReoccurringType);
            Assert.AreEqual(furtherControlMeasureTask.TaskReoccurringEndDate, furtherControlMeasureTask.FollowingTask.TaskReoccurringEndDate);
        }

        [Test]
        public void Given_task_when_Complete_then_exception_is_thronw()
        {
            var user = new User();

            var target = new Mock<HazardousSubstanceRiskAssessmentFurtherControlMeasureTask>() {CallBase = true};
            target.Setup(x => x.CanUserComplete(user)).Returns(false);
            Assert.Throws<AttemptingToCompleteFurtherControlMeasureTaskThatTheUserDoesNotHavePermissionToAccess>(() => target.Object.Complete("test", null, null, null, user, DateTime.Now));

            target.Verify(x => x.CanUserComplete(user));

        }
    }
}