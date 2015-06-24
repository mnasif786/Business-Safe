using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using BusinessSafe.Domain.Entities;

using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.TaskTests
{
    [TestFixture]
    public class ReoccurringScheduleTests
    {
        [Test]
        public void ReoccuringScheduleReturnsAtMostFiveResults()
        {
            // Given
            var task = new MultiHazardRiskAssessmentFurtherControlMeasureTask
            {
                TaskStatus = TaskStatus.Outstanding,
                TaskReoccurringType = TaskReoccurringType.Weekly,
                TaskCompletionDueDate = new DateTime(2080, 6, 23),
                TaskReoccurringEndDate = new DateTime(2080, 12, 23)
            };

            // When
            var result = task.GetReoccurringSchedule();

            // Then
            Assert.That(result.ScheduledDates.Count(), Is.EqualTo(5));
        }

        [Test]
        public void ReoccuringScheduleOnlyCalculatesScheduleWithinBoundsOfTasksRecurringStartAndEndDates()
        {
            // Given
            var task = new MultiHazardRiskAssessmentFurtherControlMeasureTask
            {
                TaskStatus = TaskStatus.Outstanding,
                TaskReoccurringType = TaskReoccurringType.Weekly,
                TaskCompletionDueDate = new DateTime(2080, 6, 23),
                TaskReoccurringEndDate = new DateTime(2080, 7, 23)
            };

            // When
            var result = task.GetReoccurringSchedule();

            // Then
            Assert.That(result.ScheduledDates.Count(), Is.EqualTo(5));
        }
    }
}
