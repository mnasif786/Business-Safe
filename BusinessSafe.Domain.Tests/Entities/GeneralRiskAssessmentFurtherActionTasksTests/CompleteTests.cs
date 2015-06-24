using System;
using System.Collections.Generic;
using System.Linq;

using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.Domain.Entities;

using NUnit.Framework;

using BusinessSafe.Domain.ParameterClasses;

namespace BusinessSafe.Domain.Tests.Entities.GeneralRiskAssessmentFurtherActionTasksTests
{
    [TestFixture]
    [Category("Unit")]
    public class CompleteTests
    {
        [Test]
        public void When_complete_task_already_completed_Then_should_throw_correct_exception()
        {
            //Given
            var task = new MultiHazardRiskAssessmentFurtherControlMeasureTask();
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
            var task = new MultiHazardRiskAssessmentFurtherControlMeasureTask();
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
        }

        [Test]
        public void When_complete_Then_set_completed_by_to_full_user_entity_of_completing_user()
        {
            //Given
            var userId = Guid.NewGuid();
            var task = new MultiHazardRiskAssessmentFurtherControlMeasureTask();
            var completingUserForAuditing = new UserForAuditing { Id = userId };

            //When
            task.Complete("comments", new List<CreateDocumentParameters>(), new List<long>(), completingUserForAuditing, null, DateTime.Now);

            //Then
            Assert.That(task.TaskCompletedBy, Is.EqualTo(completingUserForAuditing));
        }

        [Test]
        public void Given_FCM_is_reocurring_weekly_Then_when_it_is_completed_Then_following_task_should_be_created_with_due_date_a_week_later()
        {
            var assignedTo = new Employee { Id = Guid.NewGuid() };
            var user1= new UserForAuditing { Id = Guid.NewGuid() };
            var user2= new UserForAuditing { Id = Guid.NewGuid() };

            var furtherControlMeasureTask = MultiHazardRiskAssessmentFurtherControlMeasureTask.Create(
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

            var furtherControlMeasureTask = MultiHazardRiskAssessmentFurtherControlMeasureTask.Create(
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

            var furtherControlMeasureTask = MultiHazardRiskAssessmentFurtherControlMeasureTask.Create(
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
        public void Given_task_is_reoccurring_and_ReoccuringType_is_TwentyFourMonthly_next_due_date_is_correct()
        {
            // given
            var assignedTo = new Employee { Id = Guid.NewGuid() };
            var user1 = new UserForAuditing { Id = Guid.NewGuid() };
            var user2 = new UserForAuditing { Id = Guid.NewGuid() };
            new UserForAuditing { Id = Guid.NewGuid() };

            var furtherControlMeasureTask = MultiHazardRiskAssessmentFurtherControlMeasureTask.Create(
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
                (int)TaskReoccurringType.TwentyFourMonthly,
                new DateTime(2020, 09, 20),
                false,
                false,
                false,
                false,
                Guid.NewGuid());

            //when
            furtherControlMeasureTask.Complete("Test Comments", new List<CreateDocumentParameters>(), new List<long>(), user2, null, DateTime.Now);

            //then
            var expected = furtherControlMeasureTask.TaskCompletionDueDate.Value.AddYears(2);
            var actual = furtherControlMeasureTask.FollowingTask.TaskCompletionDueDate;

            Assert.IsNotNull(furtherControlMeasureTask.FollowingTask);
            Assert.AreEqual(actual,expected);

        }

        [Test]
        public void Given_task_is_reoccurring_and_ReoccuringType_is_TwentySixMonthly_next_due_date_is_correct()
        {
            // given
            var assignedTo = new Employee { Id = Guid.NewGuid() };
            var user1 = new UserForAuditing { Id = Guid.NewGuid() };
            var user2 = new UserForAuditing { Id = Guid.NewGuid() };
            new UserForAuditing { Id = Guid.NewGuid() };

            var furtherControlMeasureTask = MultiHazardRiskAssessmentFurtherControlMeasureTask.Create(
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
                (int)TaskReoccurringType.TwentySixMonthly,
                new DateTime(2020, 09, 20),
                false,
                false,
                false,
                false,
                Guid.NewGuid());

            //when
            furtherControlMeasureTask.Complete("Test Comments", new List<CreateDocumentParameters>(), new List<long>(), user2, null, DateTime.Now);

            //then
            var expected = furtherControlMeasureTask.TaskCompletionDueDate.Value.AddYears(2).AddMonths(2);
            var actual = furtherControlMeasureTask.FollowingTask.TaskCompletionDueDate;

            Assert.IsNotNull(furtherControlMeasureTask.FollowingTask);
            Assert.AreEqual(actual, expected);

        }

        [Test]
        public void Given_task_is_reoccurring_and_ReoccuringType_is_ThreeYearlyy_next_due_date_is_correct()
        {
            // given
            var assignedTo = new Employee { Id = Guid.NewGuid() };
            var user1 = new UserForAuditing { Id = Guid.NewGuid() };
            var user2 = new UserForAuditing { Id = Guid.NewGuid() };
            new UserForAuditing { Id = Guid.NewGuid() };

            var furtherControlMeasureTask = MultiHazardRiskAssessmentFurtherControlMeasureTask.Create(
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
                (int)TaskReoccurringType.ThreeYearly,
                new DateTime(2020, 09, 20),
                false,
                false,
                false,
                false,
                Guid.NewGuid());

            //when
            furtherControlMeasureTask.Complete("Test Comments", new List<CreateDocumentParameters>(), new List<long>(), user2, null, DateTime.Now);

            //then

            var expected = furtherControlMeasureTask.TaskCompletionDueDate.Value.AddYears(3);
            var actual = furtherControlMeasureTask.FollowingTask.TaskCompletionDueDate;

            Assert.IsNotNull(furtherControlMeasureTask.FollowingTask);
            Assert.AreEqual(actual, expected);

        }

        [Test]
        public void Given_task_is_reoccurring_and_ReoccuringType_is_FiveYearly_next_due_date_is_correct()
        {
            // given
            var assignedTo = new Employee { Id = Guid.NewGuid() };
            var user1 = new UserForAuditing { Id = Guid.NewGuid() };
            var user2 = new UserForAuditing { Id = Guid.NewGuid() };
            new UserForAuditing { Id = Guid.NewGuid() };

            var furtherControlMeasureTask = MultiHazardRiskAssessmentFurtherControlMeasureTask.Create(
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
                (int)TaskReoccurringType.FiveYearly,
                new DateTime(2020, 09, 20),
                false,
                false,
                false,
                false,
                Guid.NewGuid());

            //when
            furtherControlMeasureTask.Complete("Test Comments", new List<CreateDocumentParameters>(), new List<long>(),
                                               user2, null, DateTime.Now);

            //then
            var expected = furtherControlMeasureTask.TaskCompletionDueDate.Value.AddYears(5);
            var actual = furtherControlMeasureTask.FollowingTask.TaskCompletionDueDate;

            Assert.IsNotNull(furtherControlMeasureTask.FollowingTask);
            Assert.AreEqual(actual, expected);


        }

        [Test]
        public void Given_task_when_Complete_then_completedDate_is_set_from_the_parameter()
        {
            //Given
            var task = new MultiHazardRiskAssessmentFurtherControlMeasureTask();
            var user = new UserForAuditing();
            var completedDate = DateTimeOffset.Now.AddDays(-52);

            //when
            task.Complete("comments", new List<CreateDocumentParameters>(), new List<long>(), user, null, completedDate);

            //then
            Assert.AreEqual(completedDate,task.TaskCompletedDate);
            
        }

    }
}