using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.Domain.Entities;
using Moq;
using NUnit.Framework;
using BusinessSafe.Domain.ParameterClasses;

namespace BusinessSafe.Domain.Tests.Entities.RiskAssessmentReviewTaskTests
{
    [TestFixture]
    [Category("Unit")]
    public class CompleteTests
    {
        private RiskAssessmentReviewTask target = new RiskAssessmentReviewTask();
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void When_complete_review_task_already_completed_Then_should_throw_correct_exception()
        {
            //Given
            var task = new RiskAssessmentReviewTask();
            task.TaskCompletedDate = new DateTime();
            task.TaskStatus = TaskStatus.Completed;
            var userForAuditing = new UserForAuditing();
            var user = new User();

            //When
            //Then
            Assert.Throws<AttemptingToCompleteTaskThatIsCompletedException>(() => task.Complete("comments", null, new List<long>(), userForAuditing, user));
        }

        [Test]
        public void When_complete_Then_should_set_properties_correctly()
        {
            //Given
            var userForAuditing = new UserForAuditing();
            var employee = new Employee();
            var user = new User{ Employee = employee };
            var task = new RiskAssessmentReviewTask() { TaskAssignedTo = employee };

            //When
            task.Complete("comments", new List<CreateDocumentParameters>(), new List<long>(), userForAuditing, user);

            //Then
            Assert.That(task.LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Today));
            Assert.That(task.LastModifiedBy, Is.EqualTo(userForAuditing));
            Assert.That(task.TaskCompletedComments, Is.EqualTo("comments"));
            Assert.That(task.TaskCompletedDate.Value.Date, Is.EqualTo(DateTime.Today.Date));
            Assert.That(task.LastModifiedBy, Is.EqualTo(userForAuditing));
            Assert.That(task.TaskStatus, Is.EqualTo(TaskStatus.Completed));
        }

        [Test]
        public void When_complete_Then_should_set_task_completed_by_to_completing_user()
        {
            //Given
            var userForAuditing = new UserForAuditing();
            var employee = new Employee();
            var user = new User{ Employee = employee };
            var task = new RiskAssessmentReviewTask() { TaskAssignedTo = employee };

            //When
            task.Complete("comments", new List<CreateDocumentParameters>(), new List<long>(), userForAuditing, user);

            //Then
            Assert.That(task.TaskCompletedBy, Is.EqualTo(userForAuditing));
        }

        [Test]
        public void Given_a_task_is_assigned_to_userA_When_it_is_completed_by_userB_Then_the_tasks_status_is_set_to_no_longer_required()
        {
            // Given
            var task = new RiskAssessmentReviewTask();
            var employee = new Employee() { Id = Guid.NewGuid() };
            var userA = new User() { Employee = employee };
            var userBForAuditing = new UserForAuditing() { Id = Guid.NewGuid() };
            var userB = new User() { Employee = new Employee() { Id = Guid.NewGuid() } };
            

            // When
            task.TaskAssignedTo = userA.Employee;
            task.Complete(It.IsAny<string>(), new List<CreateDocumentParameters>(), new List<long>(), userBForAuditing, userB);

            //
            Assert.That(task.TaskAssignedTo, Is.EqualTo(userA.Employee));
            Assert.That(task.TaskStatus, Is.EqualTo(TaskStatus.NoLongerRequired));
        }
    }
}