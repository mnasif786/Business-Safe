using System;
using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.RiskAssessmentReviewTaskTests
{
    [TestFixture]
    [Category("Unit")]
    public class MarkAsNoLongerRequiredTests
    {
        private RiskAssessmentReviewTask target = new RiskAssessmentReviewTask();
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void When_MarkAsNoLongerRequired_review_task_already_completed_Then_should_throw_correct_exception()
        {
            //Given
            var task = new RiskAssessmentReviewTask
                           {TaskCompletedDate = new DateTime(), TaskStatus = TaskStatus.NoLongerRequired};
            var user = new UserForAuditing();

            //When
            //Then
            Assert.Throws<AttemptingToMarkAsNoLongerRequiredRiskAssessmentReviewTaskThatIsNoLongerRequiredException>(() => task.MarkAsNoLongerRequired(user));
        }

        [Test]
        public void When_MarkAsNoLongerRequired_Then_should_set_properties_correctly()
        {
            //Given
            var user = new User() { Employee = new Employee()};
            var task = new RiskAssessmentReviewTask() { TaskAssignedTo = user.Employee };

            //When
            task.MarkAsNoLongerRequired(new UserForAuditing()
                                            {
                                                Id = user.Id
                                            });

            //Then
            Assert.That(task.LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Today));
            Assert.That(task.LastModifiedBy.Id, Is.EqualTo(user.Id));
            Assert.That(task.TaskStatus, Is.EqualTo(TaskStatus.NoLongerRequired));
        }
    }
}