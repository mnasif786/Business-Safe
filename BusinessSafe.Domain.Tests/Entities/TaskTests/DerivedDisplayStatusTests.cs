using System;

using BusinessSafe.Domain.Entities;

using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.TaskTests
{
    [TestFixture]
    public class DerivedDisplayStatusTests
    {
        [Test]
        public void Given_not_deleted_and_due_date_today_When_DerivedDisplayStatus_Then_return_outstanding()
        {
            // Given
            var task = new TestableTask() { Deleted = false, TaskCompletionDueDate = DateTime.Now };

            // When
            var result = task.DerivedDisplayStatus;

            // Then
            Assert.That(result, Is.EqualTo(DerivedTaskStatusForDisplay.Outstanding));
        }

        [Test]
        public void Given_not_deleted_and_due_date_after_today_When_DerivedDisplayStatus_Then_return_outstanding()
        {
            // Given
            var task = new TestableTask() { Deleted = false, TaskCompletionDueDate = DateTime.Now.AddDays(1) };

            // When
            var result = task.DerivedDisplayStatus;

            // Then
            Assert.That(result, Is.EqualTo(DerivedTaskStatusForDisplay.Outstanding));
        }

        [Test]
        public void Given_not_deleted_and_due_date_before_today_When_DerivedDisplayStatus_Then_return_overdue()
        {
            // Given
            var task = new TestableTask() { Deleted = false, TaskCompletionDueDate = DateTime.Now.AddDays(-1) };

            // When
            var result = task.DerivedDisplayStatus;

            // Then
            Assert.That(result, Is.EqualTo(DerivedTaskStatusForDisplay.Overdue));
        }

        [Test]
        public void Given_not_deleted_and_due_date_today_When_DerivedDisplayStatus_Then_outstanding_and_overdue_ignores_seconds()
        {
            // Given
            var task = new TestableTask() { Deleted = false, TaskCompletionDueDate = DateTime.Now.AddSeconds(-1) };

            // When
            var result = task.DerivedDisplayStatus;

            // Then
            Assert.That(result, Is.EqualTo(DerivedTaskStatusForDisplay.Outstanding));
        }

        [Test]
        public void Given_not_deleted_and_due_date_today_When_DerivedDisplayStatus_Then_outstanding_and_overdue_ignores_minutes()
        {
            // Given
            var task = new TestableTask() { Deleted = false, TaskCompletionDueDate = DateTime.Now.AddMinutes(-1) };

            // When
            var result = task.DerivedDisplayStatus;

            // Then
            Assert.That(result, Is.EqualTo(DerivedTaskStatusForDisplay.Outstanding));
        }

        [Test]
        public void Given_not_deleted_and_due_date_today_When_DerivedDisplayStatus_Then_outstanding_and_overdue_ignores_hours()
        {
            // Given
            var task = new TestableTask() { Deleted = false, TaskCompletionDueDate = DateTime.Now.AddHours(-1) };

            // When
            var result = task.DerivedDisplayStatus;

            // Then
            Assert.That(result, Is.EqualTo(DerivedTaskStatusForDisplay.Outstanding));
        }

        [Test]
        public void Given_not_deleted_and_no_longer_required_When_DerivedDisplayStatus_Then_return_no_longer_required()
        {
            // Given
            var task = new TestableTask() { Deleted = false, TaskStatus = TaskStatus.NoLongerRequired };

            // When
            var result = task.DerivedDisplayStatus;

            // Then
            Assert.That(result, Is.EqualTo(DerivedTaskStatusForDisplay.NoLongerRequired));
        }

        [Test]
        public void Given_not_deleted_and_completed_When_DerivedDisplayStatus_Then_return_completed()
        {
            // Given
            var task = new TestableTask() { Deleted = false, TaskStatus = TaskStatus.Completed };

            // When
            var result = task.DerivedDisplayStatus;

            // Then
            Assert.That(result, Is.EqualTo(DerivedTaskStatusForDisplay.Completed));
        }

        public class TestableTask : Task
        {
            public override void MarkAsNoLongerRequired(UserForAuditing user)
            {
                throw new NotImplementedException();
            }

            protected override Task GetBasisForClone()
            {
                throw new NotImplementedException();
            }

            public override RiskAssessment RiskAssessment
            {
                get { throw new NotImplementedException(); }
            }

            public override bool IsTaskCompletedNotificationRequired()
            {
                throw new NotImplementedException();
            }
        }
    }
}
