using System.Collections.Generic;

using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.Responsibilities
{
    [TestFixture]
    public class HasUndeletedTasks
    {

        [Test]
        public void Given_any_undeleted_tasks_When_HasUndeletedTasks_Then_Return_true()
        {
            // Given
            var responsibility = new Responsibility { ResponsibilityTasks = new List<ResponsibilityTask>()
                                                                            {
                                                                                new ResponsibilityTask() { Deleted = true },
                                                                                new ResponsibilityTask() { Deleted = false }
                                                                            } };

            // When
            var result = responsibility.HasUndeletedTasks();

            // Then
            Assert.IsTrue(result);
        }

        [Test]
        public void Given_all_tasks_deleted_When_HasUndeletedTasks_Then_Return_false()
        {
            // Given
            var responsibility = new Responsibility { ResponsibilityTasks = new List<ResponsibilityTask>()
                                                                            {
                                                                                new ResponsibilityTask() { Deleted = true },
                                                                                new ResponsibilityTask() { Deleted = true },
                                                                                new ResponsibilityTask() { Deleted = true }
                                                                            } };

            // When
            var result = responsibility.HasUndeletedTasks();

            // Then
            Assert.IsFalse(result);
        }

    }
}